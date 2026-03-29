using Hangfire;
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

namespace TaskyRevamp.Services.Jobs.ActiveDirectory;

public class SyncAdUsers
{

private readonly IRepository<User> _userRepository;
private readonly IActiveDirectoryService _adService;
IOptions<LdapSettings> _ldapPath;

public SyncAdUsers(IOptions<LdapSettings> ldapSettings, IRepository<User> userRepository, IActiveDirectoryService adService)
{
_userRepository = userRepository;
_ldapPath = ldapSettings;
_adService = adService;
}

[DisableConcurrentExecution(timeoutInSeconds: 3600)]
public async Task Execute()
{
try
{
await SyncAllAdUsers();
}
catch (Exception ex) { Console.WriteLine(ex); }
}

private async Task SyncAllAdUsers()
{
string ldapPath = _ldapPath.Value.Path;

var adUsers = await CheckNewAddedUser(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password);
var adUsersModified = await CheckUsersChanges(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password);

var allAdUsers = adUsers.Concat(adUsersModified)
  .GroupBy(u => u.Username)
  .Select(g => g.First())
  .ToList();

await SaveUsersToDatabaseBulk(allAdUsers);

Console.WriteLine("Active Directory users successfully saved to the database.");
}

public async Task<List<User>> CheckNewAddedUser(string ldapPath, string userName, string password)
{
var users = new List<User>();

using (var entry = new DirectoryEntry(ldapPath, userName, password))
{
using (var searcher = new DirectorySearcher(entry))
{
var fromDate = DateTime.UtcNow.AddDays(-1);
string filter = $"(&(objectClass=user)(!(sAMAccountName=*$))(|(userAccountControl=512)(userAccountControl=66048))(whenCreated>={fromDate.ToString("yyyyMMddHHmmss.0Z")}))";

searcher.PageSize = 5000;
searcher.ServerTimeLimit = TimeSpan.FromMinutes(5);
searcher.Filter = filter;
_adService.ConfigureSearcherProperties(searcher);
searcher.PropertiesToLoad.Add("whenCreated");

foreach (SearchResult result in searcher.FindAll())
{
var adUser = _adService.ExtractAdUser(result);
users.Add(MapAdUserToUser(adUser));
}
}
}

return users;
}

public async Task<List<User>> CheckUsersChanges(string ldapPath, string userName, string password)
{
var users = new List<User>();

using (var entry = new DirectoryEntry(ldapPath, userName, password))
{
using (var searcher = new DirectorySearcher(entry))
{
var fromDate = DateTime.UtcNow.AddDays(-1);

searcher.PageSize = 5000;
searcher.ServerTimeLimit = TimeSpan.FromMinutes(5);
searcher.Filter = $"(&(objectClass=user)(!(sAMAccountName=*$))(|(userAccountControl=512)(userAccountControl=66048))(whenChanged>={fromDate.ToString("yyyyMMddHHmmss.0Z")}))";
_adService.ConfigureSearcherProperties(searcher);
searcher.PropertiesToLoad.Add("whenChanged");

foreach (SearchResult result in searcher.FindAll())
{
var adUser = _adService.ExtractAdUser(result);
users.Add(MapAdUserToUser(adUser));
}
}
}

return users;
}

private static User MapAdUserToUser(Dto.Account.AdUser adUser)
{
return new User
{
Username = adUser.Username,
NameEnglish = adUser.DisplayName,
NameArabic = adUser.DisplayName,
Email = adUser.Email,
DistinguishedName = adUser.DistinguishedName,
GivenName = adUser.GivenName,
Mobile = adUser.Phone,
IsActive = adUser.IsActive,
IsManager = false,
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
}
}

await _userRepository.SaveChangesAsync();
}

private async Task SaveUsersToDatabaseBulk(List<User> users)
{
var existingUsers = (await _userRepository.All()).Value;

var existingUserDictionary = existingUsers
.ToDictionary(u => u.Username.ToLower(), u => u, StringComparer.OrdinalIgnoreCase);

var newUsers = new List<User>();
var usersToUpdate = new List<User>();

foreach (var user in users)
{
if (existingUserDictionary.TryGetValue(user.Username.ToLower(), out var existingUser))
{
existingUser.Username = user.Username;
existingUser.NameEnglish = user.NameEnglish;
existingUser.NameArabic = user.NameArabic;
existingUser.Email = user.Email;
existingUser.DistinguishedName = user.DistinguishedName;
existingUser.GivenName = user.GivenName;
existingUser.Mobile = user.Mobile;
existingUser.IsActive = user.IsActive;
existingUser.IsManager = user.IsManager;

usersToUpdate.Add(existingUser);
}
else
{
user.Id = Guid.NewGuid();
newUsers.Add(user);
}
}

if (newUsers.Any())
{
await _userRepository.BulkInsertAsync(newUsers);
}

if (usersToUpdate.Any())
{
await _userRepository.BulkUpdateAsync(usersToUpdate);
}

await _userRepository.SaveChangesAsync();
}

}
