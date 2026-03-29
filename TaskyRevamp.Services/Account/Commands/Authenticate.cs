using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskyRevamp.Domain.Models.Users;
using UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services;
using TaskyRevamp.Services.Exceptions;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Account;


namespace TaskyRevamp.Services.Account.Commands;

public record AuthenticateCommand(string Username, string Password) : IRequest<string?>;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, string?>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserDelegation> _delegateRepository;
    private readonly IOptions<AppSettings> _appSettingsOptions;
    IOptions<LdapSettings> _ldapPath;

    public AuthenticateCommandHandler(IRepository<User> userRepository, IRepository<UserDelegation> delegateRepository, IOptions<AppSettings> appSettingsOptions, IOptions<LdapSettings> ldapSettings)
    {
        _userRepository = userRepository;
        _delegateRepository = delegateRepository;
        _appSettingsOptions = appSettingsOptions;
        _ldapPath = ldapSettings;
    }

    private const int DefaultTokenExpiry = 24;

    public async Task<string?> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = null;
            if (_appSettingsOptions.Value.AuthenticationMode == (int)AuthenticationMode.ActiveDirectory)
            {
                var adUser = AuthenticateAndGetUser(_ldapPath.Value.Path, request.Username, request.Password);
                if (!adUser.IsAuthenticated)
                {
                    return null;
                }
                var userResponse = await _userRepository.FindBy(x => x.Username == request.Username);
                if (userResponse.IsFailure || userResponse.Value is null || userResponse.Value.Count == 0)
                {
                    user = new User
                    {
                        Username = adUser.Username,
                        NameArabic = adUser.DisplayName,
                        NameEnglish = adUser.DisplayName,
                        Email = adUser.Email,
                        DistinguishedName = adUser.DistinguishedName,
                        GivenName = adUser.GivenName,
                        //Surname = adUser.Surname,
                        //Title = adUser.Title,
                        Mobile = adUser.Phone,
                        IsActive = adUser.IsActive,
                        //Manager = adUser.ManagerUsername,
                    };
                    await _userRepository.Insert(user);
                    await _userRepository.SaveChangesAsync();
                }
                else
                {
                    user = userResponse.Value.FirstOrDefault()!;
                    if (user is null)
                    {
                        throw new NoDataException("User Not Found!");
                    }
                    user = await SyncUserWithActiveDirectory(user, adUser);
                }

            }
            else
            {
                var userResponse = await _userRepository.FindBy(x => x.Username == request.Username);
                user = userResponse.Value.FirstOrDefault()!;
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettingsOptions.Value.Secret);
            var tokenExpiry = GetTokenExpirySettingsQuery();
            var delegateUsersNames = new List<string>();
            var delegateUsersIds = new List<Guid>();
            var delegateUsers = await GetDelegatedUsersAsync(user);

            delegateUsersNames.AddRange(delegateUsers.Select(x => x.Username));
            delegateUsersIds.AddRange(delegateUsers.Select(x => x.Id));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new(ClaimTypes.Name, user.Id.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("Username", user.Username ?? ""),
                new("Email", user.Email ?? ""),
                new("NameEnglish", user.NameEnglish ?? ""),
                new("NameArabic", user.NameArabic ?? ""),
                new("Id", user.Id.ToString()),
                new("DelegatedUsersId", string.Join(",", delegateUsersIds)),
                }),
                Expires = DateTime.UtcNow.AddHours(tokenExpiry),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
	private async Task<User> SyncUserWithActiveDirectory(User existingUser, AdUser adUser)
	{
		bool hasChanges = false;

		if (existingUser.NameEnglish != adUser.DisplayName)
		{
			existingUser.NameEnglish = adUser.DisplayName;
			hasChanges = true;
		}
		if (existingUser.NameArabic != adUser.DisplayName)
		{
			existingUser.NameArabic = adUser.DisplayName;
			hasChanges = true;
		}
		if (existingUser.Email != adUser.Email)
		{
			existingUser.Email = adUser.Email;
			hasChanges = true;
		}
		if (existingUser.GivenName != adUser.GivenName)
		{
			existingUser.GivenName = adUser.GivenName;
			hasChanges = true;
		}
		if (existingUser.Mobile != adUser.Phone)
		{
			existingUser.Mobile = adUser.Phone;
			hasChanges = true;
		}
		if (existingUser.IsActive != adUser.IsActive)
		{
			existingUser.IsActive = adUser.IsActive;
			hasChanges = true;
		}
		if (existingUser.DistinguishedName != adUser.DistinguishedName)
		{
			existingUser.DistinguishedName = adUser.DistinguishedName;
			hasChanges = true;
		}

		if (hasChanges)
		{
			await _userRepository.Update(existingUser);
			await _userRepository.SaveChangesAsync();
		}

		return existingUser;
	}
    private AdUser AuthenticateAndGetUser(string ldapPath, string username, string password)
    {
		try
		{
			// First, authenticate with the user's credentials
			using (var authEntry = new DirectoryEntry(ldapPath, username, password))
			{
                try
                {
                    object nativeObject = authEntry.NativeObject;
                }
                catch(Exception)
				{
                    // Authentication failed
                    return new AdUser { IsAuthenticated = false };
				}
			}

			// Authentication successful, now get user details using service account
			using (var entry = new DirectoryEntry(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password))
			{
				using (var searcher = new DirectorySearcher(entry))
				{
					searcher.Filter = $"(sAMAccountName={username})";

					// Load all required properties
					searcher.PropertiesToLoad.Add("samaccountname");
					searcher.PropertiesToLoad.Add("displayName");
					searcher.PropertiesToLoad.Add("mail");
					searcher.PropertiesToLoad.Add("distinguishedName");
					searcher.PropertiesToLoad.Add("givenName");
					searcher.PropertiesToLoad.Add("sn");
					searcher.PropertiesToLoad.Add("title");
					searcher.PropertiesToLoad.Add("telephoneNumber");
					searcher.PropertiesToLoad.Add("userAccountControl");
					searcher.PropertiesToLoad.Add("manager");

					var result = searcher.FindOne();
					if (result == null)
					{
						return new AdUser { IsAuthenticated = false };
					}

					// Extract all properties
					string samAccountName = result.Properties.Contains("samAccountName")? result.Properties["samAccountName"][0].ToString(): string.Empty;
					string displayName = result.Properties.Contains("displayName")? result.Properties["displayName"][0].ToString(): string.Empty;
					string email = result.Properties.Contains("mail")? result.Properties["mail"][0].ToString(): string.Empty;
					string distinguishedName = result.Properties.Contains("distinguishedName")? result.Properties["distinguishedName"][0].ToString(): string.Empty;
					string givenName = result.Properties.Contains("givenName")? result.Properties["givenName"][0].ToString(): string.Empty;
					string surname = result.Properties.Contains("sn")? result.Properties["sn"][0].ToString(): string.Empty;
					string title = result.Properties.Contains("title")? result.Properties["title"][0].ToString(): string.Empty;
					string phone = result.Properties.Contains("telephoneNumber")? result.Properties["telephoneNumber"][0].ToString(): string.Empty;

					int userAccountControl = result.Properties.Contains("userAccountControl")? Convert.ToInt32(result.Properties["userAccountControl"][0]): 0;
					bool isActive = (userAccountControl & 0x0002) == 0;
					string managerUsername = string.Empty;
					if (result.Properties.Contains("manager"))
					{
						string managerDn = result.Properties["manager"][0].ToString();
						managerUsername = GetUserManager(ldapPath, managerDn);
					}

					return new AdUser
					{
						Username = samAccountName,
						DisplayName = displayName,
						Email = email,
						DistinguishedName = distinguishedName,
						GivenName = givenName,
						Surname = surname,
						Title = title,
						Phone = phone,
						IsActive = isActive,
						ManagerUsername = managerUsername,
						IsAuthenticated = true
					};
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
			throw new InvalidOperationException(ex.Message);
		}
	}
	private string GetUserManager(string ldapPath, string managerDn)
    {
        try
        {
            // Create a DirectoryEntry for the LDAP path
            using (var entry = new DirectoryEntry(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password))
            {
                // Create a DirectorySearcher to search for the manager by distinguished name (DN)
                using (var searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(distinguishedName={managerDn})";
                    searcher.PropertiesToLoad.Add("sAMAccountName"); // Manager's username

                    // Perform the search for the manager
                    var result = searcher.FindOne();

                    if (result != null && result.Properties.Contains("sAMAccountName"))
                    {
                        return result.Properties["sAMAccountName"][0].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions related to retrieving the manager
            throw new InvalidOperationException(ex.Message);
        }

        return null; // Return null if the manager's sAMAccountName is not found
    }
    private async Task<List<User>> GetDelegatedUsersAsync(User user)
    {
        var users = new List<User>();
        users.Add(user);

        var data = await _delegateRepository.FindBy(x => x.ToUserId == user.Id && x.FromDate <= DateTime.UtcNow && x.ToDate >= DateTime.UtcNow);
        if (data == null) { return new List<User>() { user }; }
        var toUsers = data.Value?.ToList();
        var guids = toUsers.Select(x => x.FromUserId).ToList();

        var data2 = await _userRepository.FindBy(x => guids.Contains(x.Id));
        if (data2 == null) { return new List<User>() { user }; }
        var delegateUsers = data2.Value?.ToList();
        users.AddRange(delegateUsers);

        return users;

    }
    private int GetTokenExpirySettingsQuery()
    {
        var data = _appSettingsOptions.Value.ExpiryHours;
        if (data is null)
        {
            return DefaultTokenExpiry;
        }

        return int.Parse(data.ToString());
    }
}