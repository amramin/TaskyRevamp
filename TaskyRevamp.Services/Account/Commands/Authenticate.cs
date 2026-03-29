using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskyRevamp.Domain.Interfaces.Services;
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
    private readonly IActiveDirectoryService _adService;

    public AuthenticateCommandHandler(IRepository<User> userRepository, IRepository<UserDelegation> delegateRepository,
        IOptions<AppSettings> appSettingsOptions, IActiveDirectoryService adService)
    {
        _userRepository = userRepository;
        _delegateRepository = delegateRepository;
        _appSettingsOptions = appSettingsOptions;
        _adService = adService;
    }

    private const int DefaultTokenExpiry = 24;

    public async Task<string?> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = null;
            if (_appSettingsOptions.Value.AuthenticationMode == (int)AuthenticationMode.ActiveDirectory)
            {
                var adUser = _adService.AuthenticateAndGetUser(request.Username, request.Password);
                if (!adUser.IsAuthenticated)
                {
                    return null;
                }
                var userResponse = await _userRepository.FindBy(x => x.Username == request.Username);
                if (userResponse.IsFailure || userResponse.Value is null || userResponse.Value.Count == 0)
                {
                    user = MapAdUserToUser(adUser);
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
            var delegateUsersIds = new List<Guid>();
            var delegateUsers = await GetDelegatedUsersAsync(user);

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

    private static User MapAdUserToUser(AdUser adUser)
    {
        return new User
        {
            Username = adUser.Username,
            NameArabic = adUser.DisplayName,
            NameEnglish = adUser.DisplayName,
            Email = adUser.Email,
            DistinguishedName = adUser.DistinguishedName,
            GivenName = adUser.GivenName,
            Mobile = adUser.Phone,
            IsActive = adUser.IsActive,
        };
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