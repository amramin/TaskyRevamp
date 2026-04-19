using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Services;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;


namespace TaskyRevamp.Services.Jobs.ActiveDirectory.SyncFirstTime;

public record SyncAllUsersFt(Guid LogedInUser) : IRequest<int>;

public class SyncAllUsersFtHandler : IRequestHandler<SyncAllUsersFt, int>
{
    private readonly IRepository<User> _userRepository;
    private readonly IOptions<LdapSettings> _ldapPath;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IActiveDirectoryService _adService;

    public SyncAllUsersFtHandler(IOptions<LdapSettings> ldapSettings, IRepository<User> userRepository,
        IHttpContextAccessor httpContextAccessor, IActiveDirectoryService adService)
    {
        _userRepository = userRepository;
        _ldapPath = ldapSettings;
        _httpContextAccessor = httpContextAccessor;
        _adService = adService;
    }

    public async Task<int> Handle(SyncAllUsersFt request, CancellationToken cancellationToken)
    {
        try
        {
            var AddedUsers = await SyncAllAdUsers(request.LogedInUser);
            return AddedUsers;
        }
        catch (Exception ex) { Console.WriteLine(ex); return 0; }
    }

    private async Task<int> SyncAllAdUsers(Guid updatedby)
    {
        string ldapPath = _ldapPath.Value.Path;
        var adUsers = await GetAllActiveDirectoryUsers(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password);
        var AddedUsers = await SaveUsersToDatabaseBulk(adUsers, updatedby);

        Console.WriteLine("Active Directory users successfully saved to the database.");
        return AddedUsers;
    }

    public async Task<List<User>> GetAllActiveDirectoryUsers(string ldapPath, string userName, string password)
    {
        var users = new List<User>();

        using (var entry = new DirectoryEntry(ldapPath, userName, password))
        {
            using (var searcher = new DirectorySearcher(entry))
            {
                searcher.PageSize = 5000;
                searcher.ServerTimeLimit = TimeSpan.FromMinutes(5);
                searcher.Filter = "(&(objectClass=user)(!(sAMAccountName=*$))(|(userAccountControl=512)(userAccountControl=66048)))";
                _adService.ConfigureSearcherProperties(searcher);

                foreach (SearchResult result in searcher.FindAll())
                {
                    var adUser = _adService.ExtractAdUser(result);
                    users.Add(MapAdUserToUser(adUser));
                }
            }
        }

        return users;
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
            IsManager = false
        };
    }

    public async Task SaveUsersToDatabase(List<User> users)
    {
        foreach (var user in users)
        {
            var userData = await _userRepository.FindBy(x => x.Username == user.Username);
            var existingUser = userData?.Value?.FirstOrDefault();

            if (existingUser != null)
            {
                existingUser.NameEnglish = user.NameEnglish;
                existingUser.NameArabic = user.NameArabic;
                existingUser.Email = user.Email;
                existingUser.DistinguishedName = user.DistinguishedName;
                existingUser.GivenName = user.GivenName;
                existingUser.Mobile = user.Mobile;
                existingUser.IsActive = user.IsActive;
                existingUser.IsManager = user.IsManager;
            }
            else
            {
                await _userRepository.Insert(user);
            }
        }
        await _userRepository.SaveChangesAsync();
    }

    public async Task<int> SaveUsersToDatabaseBulk(List<User> users, Guid UpdatedBy)
    {
        int AddedUsers = 0;
        var dbUsersResult = await _userRepository.AllAsNoTracking();
        var dbUsers = (dbUsersResult.Value
              ?? Enumerable.Empty<User>());

        var dbUsernames = dbUsers
            .Select(u => u.Username)
            .ToHashSet();
        var ExistUsersAD = users.Where(u => dbUsernames.Contains(u.Username)).ToList();
        users.RemoveAll(u => dbUsernames.Contains(u.Username));
        if (users is not null && users.Count() > 0)
        {
            await _userRepository.InsertRange(users);
            AddedUsers = users.Count();
        }
        if (ExistUsersAD is not null && dbUsers is not null)
        {
            var UsersToUpdate = UpdateDbUsers(dbUsers.ToList(), ExistUsersAD, UpdatedBy);
            await _userRepository.UpdateRange(UsersToUpdate);
        }
        return AddedUsers;
    }

    private List<User> UpdateDbUsers(List<User> DbUsers, List<User> AdUsers, Guid UpdatedBy)
    {
        var adUserDict = AdUsers.ToDictionary(u => u.Username ?? "", u => u);

        foreach (var dbUser in DbUsers)
        {
            try
            {
                if (adUserDict.TryGetValue(dbUser.Username ?? "", out var adUser))
                {
                    dbUser.NameEnglish = adUser.NameEnglish;
                    dbUser.NameArabic = adUser.NameArabic;
                    dbUser.Email = adUser.Email;
                    dbUser.DistinguishedName = adUser.DistinguishedName;
                    dbUser.GivenName = adUser.GivenName;
                    dbUser.Mobile = adUser.Mobile;
                    dbUser.IsManager = adUser.IsManager;
                    dbUser.UpdatedById = UpdatedBy;
                }
            }
            catch (Exception ex)
            {
                var res = ex;
            }
        }

        return DbUsers;
    }

}
