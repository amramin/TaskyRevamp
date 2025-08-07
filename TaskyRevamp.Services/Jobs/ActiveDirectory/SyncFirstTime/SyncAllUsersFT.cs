using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;


namespace TaskyRevamp.Services.Jobs.ActiveDirectory.SyncFirstTime;

public record SyncAllUsersFT : IRequest<bool>;

public class SyncAllUsersFTHandler : IRequestHandler<SyncAllUsersFT, bool>
{
    private readonly IRepository<User> _userRepository;
    private readonly IOptions<LdapSettings> _ldapPath;

    public SyncAllUsersFTHandler(IOptions<LdapSettings> ldapSettings, IRepository<Domain.Models.Users.User> userRepository)
    {
        _userRepository = userRepository;
        _ldapPath = ldapSettings;
    }

    public async Task<bool> Handle(SyncAllUsersFT request, CancellationToken cancellationToken)
    {
        try
        {
            await SyncAllADUsers();
            return true;
        }
        catch (Exception ex) { Console.WriteLine(ex); return false; }

    }

    private async Task SyncAllADUsers()
    {
        string ldapPath = _ldapPath.Value.Path;
        List<User> adUsers = await GetAllActiveDirectoryUsers(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password);
        await SaveUsersToDatabaseBulk(adUsers);

        Console.WriteLine("Active Directory users successfully saved to the database.");

    }
    public async Task<List<User>> GetAllActiveDirectoryUsers(string ldapPath, string userName, string password)
    {
        List<User> users = new List<User>();

        using (DirectoryEntry entry = new DirectoryEntry(ldapPath, userName, password))
        {
            using (DirectorySearcher searcher = new DirectorySearcher(entry))
            {
                // Filter to get user objects
                searcher.PageSize = 5000;
                searcher.ServerTimeLimit = TimeSpan.FromMinutes(5);
                searcher.Filter = "(&(objectClass=user)(!(sAMAccountName=*$))(|(userAccountControl=512)(userAccountControl=66048)))";
                //searcher.Filter = "(&(objectClass=user)(!(sAMAccountName=*$))(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
                searcher.PropertiesToLoad.Add("samAccountName");    // Username
                searcher.PropertiesToLoad.Add("displayName");       // Display name
                searcher.PropertiesToLoad.Add("mail");              // Email
                searcher.PropertiesToLoad.Add("distinguishedName"); // Full DN
                searcher.PropertiesToLoad.Add("givenName");         // Given name
                searcher.PropertiesToLoad.Add("sn");                // Surname
                searcher.PropertiesToLoad.Add("title");             // Title
                searcher.PropertiesToLoad.Add("telephoneNumber");    // Phone number
                searcher.PropertiesToLoad.Add("userAccountControl");
                searcher.PropertiesToLoad.Add("manager"); // Manager DN
                // Add other attributes as needed

                foreach (SearchResult result in searcher.FindAll())
                {
                    string username = result.Properties.Contains("samAccountName") ? result.Properties["samAccountName"][0].ToString() : string.Empty;
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
                        managerUsername = await GetUserManager(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password, managerDn);

                    }
                    // Add the AD user to the list
                    users.Add(new User
                    {
                        Username = username,
                        NameArabic = displayName,
                        NameEnglish = displayName,
                        Email = email,
                        DistinguishedName = distinguishedName,
                        GivenName = givenName,
                        //Surname = surname,
                        //Title = title,
                        Mobile = phone,
                        IsActive = isActive,
                        IsManager = false
                        // Add other attributes as needed
                    });
                }
            }
        }

        return users;
    }
    public static async Task<string> GetUserManager(string ldapPath, string userName, string password, string managerDn)
    {
        try
        {
            // Create a DirectoryEntry for the LDAP path
            using (DirectoryEntry entry = new DirectoryEntry(ldapPath, userName, password))
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
            Console.WriteLine($"Error retrieving manager's username: {ex.Message}");
        }

        return null; // Return null if the manager's sAMAccountName is not found
    }

    public async Task SaveUsersToDatabase(List<User> users)
    {

        foreach (var user in users)
        {
            // Check if the user already exists
            var userData = await _userRepository.FindBy(x => x.Username == user.Username);
            var existingUser = userData?.Value?.FirstOrDefault();

            if (existingUser != null)
            {
                // Update existing user
                existingUser.NameEnglish = user.NameEnglish;
                existingUser.NameArabic = user.NameArabic;
                existingUser.Email = user.Email;
                existingUser.DistinguishedName = user.DistinguishedName;
                existingUser.GivenName = user.GivenName;
                //existingUser.Surname = user.Surname;
                //existingUser.Title = user.Title;
                existingUser.Mobile = user.Mobile;
                existingUser.IsActive = user.IsActive;
                existingUser.IsManager = user.IsManager;
            }
            else
            {
                // Add new user
                await _userRepository.Insert(user);
            }


        }
        await _userRepository.SaveChangesAsync();
    }
    public async Task SaveUsersToDatabaseBulk(List<User> users)
    {
        await _userRepository.InsertRange(users);
        await _userRepository.BulkInsertAsync(users);

    }


}