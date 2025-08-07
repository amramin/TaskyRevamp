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
using TaskyRevamp.Domain.Exceptions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Models.Users.UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services;


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

            var isAuthenticated = AuthenticateUser(_ldapPath.Value.Path, request.Username, request.Password);
            if (!isAuthenticated)
            {
                return null;
            }


            User user = new User();

            var userResponse = await _userRepository.FindBy(x => x.Username == request.Username);
            if (userResponse.IsFailure || userResponse.Value is null || userResponse.Value.Count == 0)
            {
                var newUser = AddNewUser(_ldapPath.Value.Path, request.Username);

                if (newUser == null)
                {
                    throw new NoDataException("User Not Found!");
                }
                else
                {
                    user = newUser;
                }

            }
            else
            {
                user = userResponse.Value.FirstOrDefault();
                if (user is null)
                {
                    throw new NoDataException("User Not Found!");
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettingsOptions.Value.Secret);
            var tokenExpiry = GetTokenExpirySettingsQuery();
            List<string> clusters = new List<string>();
            List<string> roles = new List<string>();
            List<string> clusterId = new List<string>();
            List<string> delegateUsersNames = new List<string>();
            List<Guid> delegateUsersIds = new List<Guid>();
            List<User> delegateUsers = GetDelegatedUsers(user);

            delegateUsersNames.AddRange(delegateUsers.Select(x => x.Username));
            delegateUsersIds.AddRange(delegateUsers.Select(x => x.Id));


            //foreach (var delegateUser in delegateUsersNames)
            //{
            //    GetUserClusters(_ldapPath.Value.Path, delegateUser, ref clusters, ref clusterId);
            //    roles.AddRange(GetUserRoles(user));
            //}

            clusters = clusters.Distinct().ToList();
            roles = roles.Distinct().ToList();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new(ClaimTypes.Name, user.Id.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role,  string.Join(",", roles)),
                new("Username", user.Username),
                new("Email", user.Email),
                new("NameEnglish", user.NameEnglish),
                new("NameArabic", user.NameArabic),
                new("Id", user.Id.ToString()),
                //new("Clusters", string.Join(",", clusters)),
                new("ClusterId",  string.Join(",", clusterId)),
                new("Roles", string.Join(",", roles)),
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

    private bool AuthenticateUser(string ldapPath, string username, string password)
    {
        try
        {
            using (DirectoryEntry entry = new DirectoryEntry(ldapPath, username, password))
            {
                // Bind to the directory and authenticate
                object nativeObject = entry.NativeObject;
                return true; // Successful login
            }

        }
        //catch (DirectoryServicesCOMException)
        //{
        //    // Invalid credentials
        //    return false;
        //}
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw new InvalidOperationException(ex.Message);
            return false;
        }
    }
    private User AddNewUser(string ldapPath, string username)
    {
        try
        {
            using (DirectoryEntry entry = new DirectoryEntry(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password))
            {
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    // Search for users created within the last 7 days
                    DateTime fromDate = DateTime.UtcNow.AddDays(-1);
                    string filter = searcher.Filter = $"(sAMAccountName={username})";

                    searcher.Filter = filter;
                    searcher.PropertiesToLoad.Add("samaccountname"); // Account name
                    searcher.PropertiesToLoad.Add("whenCreated");    // Creation time
                    searcher.PropertiesToLoad.Add("displayName");       // Display name
                    searcher.PropertiesToLoad.Add("mail");              // Email
                    searcher.PropertiesToLoad.Add("distinguishedName"); // Full DN
                    searcher.PropertiesToLoad.Add("givenName");         // Given name
                    searcher.PropertiesToLoad.Add("sn");                // Surname
                    searcher.PropertiesToLoad.Add("title");             // Title
                    searcher.PropertiesToLoad.Add("telephoneNumber");    // Phone number
                    searcher.PropertiesToLoad.Add("userAccountControl");
                    searcher.PropertiesToLoad.Add("manager"); // Manager DN

                    foreach (SearchResult result in searcher.FindAll())
                    {
                        string samAccountName = result.Properties.Contains("samAccountName") ? result.Properties["samAccountName"][0].ToString() : string.Empty;
                        string displayName = result.Properties.Contains("displayName") ? result.Properties["displayName"][0].ToString() : string.Empty;
                        string email = result.Properties.Contains("mail") ? result.Properties["mail"][0].ToString() : string.Empty;
                        string distinguishedName = result.Properties.Contains("distinguishedName") ? result.Properties["distinguishedName"][0].ToString() : string.Empty;
                        string givenName = result.Properties.Contains("givenName") ? result.Properties["givenName"][0].ToString() : string.Empty;
                        string surname = result.Properties.Contains("sn") ? result.Properties["sn"][0].ToString() : string.Empty;
                        string title = result.Properties.Contains("title") ? result.Properties["title"][0].ToString() : string.Empty;
                        string phone = result.Properties.Contains("telephoneNumber") ? result.Properties["telephoneNumber"][0].ToString() : string.Empty;
                        // Check if the user account is active by inspecting the userAccountControl flag
                        int userAccountControl = result.Properties.Contains("userAccountControl") ? Convert.ToInt32(result.Properties["userAccountControl"][0]) : 0;
                        bool isActive = (userAccountControl & 0x0002) == 0; // If the 2nd bit is not set, the account is active
                        string managerUsername = "";
                        // Check if the user has a manager
                        if (result.Properties.Contains("manager"))
                        {
                            // Get the manager's DN
                            string managerDn = result.Properties["manager"][0].ToString();
                            managerUsername = GetUserManager(ldapPath, managerDn);

                        }

                        // Add the AD user to the list
                        User newUser = new User
                        {
                            Username = samAccountName,
                            NameArabic = displayName,
                            NameEnglish = displayName,
                            Email = email,
                            DistinguishedName = distinguishedName,
                            GivenName = givenName,
                            //Surname = surname,
                            //Title = title,
                            Mobile = phone,
                            IsActive = isActive,
                            //Manager = managerUsername,
                            // Add other attributes as needed
                        };

                        _userRepository.Insert(newUser);
                        _userRepository.SaveChangesAsync();

                        return newUser;
                    }
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
            return null;
        }
    }
    private string GetUserManager(string ldapPath, string managerDn)
    {
        try
        {
            // Create a DirectoryEntry for the LDAP path
            using (DirectoryEntry entry = new DirectoryEntry(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password))
            {
                // Create a DirectorySearcher to search for the manager by distinguished name (DN)
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(distinguishedName={managerDn})";
                    searcher.PropertiesToLoad.Add("sAMAccountName"); // Manager's username

                    // Perform the search for the manager
                    SearchResult result = searcher.FindOne();

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


    private List<User> GetDelegatedUsers(User user)
    {
        List<User> users = new List<User>();
        users.Add(user);

        var data = _delegateRepository.FindBy(x => x.ToUserId == user.Id && x.FromDate <= DateTime.UtcNow && x.ToDate >= DateTime.UtcNow);
        if (data == null) { return new List<User>() { user }; }
        var toUsers = data.Result.Value?.ToList();
        List<Guid> guids = toUsers.Select(x => x.FromUserId).ToList();

        var data2 = _userRepository.FindBy(x => guids.Contains(x.Id));
        if (data2 == null) { return new List<User>() { user }; }
        var delegateUsers = data2.Result.Value?.ToList();
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