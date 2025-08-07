using Hangfire;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Jobs.ActiveDirectory;

public class SyncADUsers
{

    private readonly IRepository<User> _userRepository;
    IOptions<LdapSettings> _ldapPath;

    public SyncADUsers(IOptions<LdapSettings> ldapSettings, IRepository<User> userRepository)
    {
        _userRepository = userRepository;
        _ldapPath = ldapSettings;
    }

    [DisableConcurrentExecution(timeoutInSeconds: 3600)]
    public async Task Execute()
    {
        try
        {
            await SyncAllADUsers();
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }

    private async Task SyncAllADUsers()
    {
        // Define the directory entry for the root of the domain
        string ldapPath = _ldapPath.Value.Path;

        // Step 1: Query AD for all new users
        List<User> adUsers = await CheckNewAddedUser(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password);

        // Step 1: Query AD for all modified users
        List<User> adUsersModified = await CheckUsersChanges(ldapPath, _ldapPath.Value.Username, _ldapPath.Value.Password);

        var allAdUsers = adUsers.Concat(adUsersModified)
                                  .GroupBy(u => u.Username)
                                  .Select(g => g.First())
                                  .ToList();

        await SaveUsersToDatabaseBulk(allAdUsers);


        Console.WriteLine("Active Directory users successfully saved to the database.");

    }
    public async Task<List<User>> CheckNewAddedUser(string ldapPath, string userName, string password)
    {
        List<User> users = new List<User>();

        using (DirectoryEntry entry = new DirectoryEntry(ldapPath, userName, password))
        {
            using (DirectorySearcher searcher = new DirectorySearcher(entry))
            {
                // Search for users created within the last 7 days
                DateTime fromDate = DateTime.UtcNow.AddDays(-1);
                string filter = $"(&(objectClass=user)(!(sAMAccountName=*$))(|(userAccountControl=512)(userAccountControl=66048))(whenCreated>={fromDate.ToString("yyyyMMddHHmmss.0Z")}))";

                searcher.PageSize = 5000;
                searcher.ServerTimeLimit = TimeSpan.FromMinutes(5);
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
                        managerUsername = await GetUserManager(ldapPath, userName, password, managerDn);

                    }

                    // Add the AD user to the list
                    users.Add(new User
                    {
                        Username = samAccountName,
                        NameEnglish = displayName,
                        NameArabic = displayName,
                        Email = email,
                        DistinguishedName = distinguishedName,
                        GivenName = givenName,
                        //Surname = surname,
                        //Title = title,
                        Mobile = phone,
                        IsActive = isActive,
                        IsManager = false,
                        // Add other attributes as needed
                    });
                }
            }
        }

        return users;
    }
    public async Task<List<User>> CheckUsersChanges(string ldapPath, string userName, string password)
    {
        List<User> users = new List<User>();

        using (DirectoryEntry entry = new DirectoryEntry(ldapPath, userName, password))
        {
            using (DirectorySearcher searcher = new DirectorySearcher(entry))
            {
                // Calculate the date 1 days ago
                DateTime fromDate = DateTime.UtcNow.AddDays(-1);
                //string fromDateString = fromDate.ToString("yyyyMMddHHmmss.0Z");

                searcher.PageSize = 5000;
                searcher.ServerTimeLimit = TimeSpan.FromMinutes(5);
                // Search for users whose whenChanged attribute is within the last 7 days
                searcher.Filter = $"(&(objectClass=user)(!(sAMAccountName=*$))(|(userAccountControl=512)(userAccountControl=66048))(whenChanged>={fromDate.ToString("yyyyMMddHHmmss.0Z")}))";
                searcher.PropertiesToLoad.Add("samAccountName");        // Load the user account name
                searcher.PropertiesToLoad.Add("userAccountControl");    // Load the userAccountControl attribute
                searcher.PropertiesToLoad.Add("whenChanged");           // Load the whenChanged attribute
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
                    string samAccountName = result.Properties["samAccountName"][0].ToString();
                    int userAccountControl = (int)result.Properties["userAccountControl"][0];
                    DateTime whenChanged = (DateTime)result.Properties["whenChanged"][0];
                    string displayName = result.Properties.Contains("displayName") ? result.Properties["displayName"][0].ToString() : string.Empty;
                    string email = result.Properties.Contains("mail") ? result.Properties["mail"][0].ToString() : string.Empty;
                    string distinguishedName = result.Properties.Contains("distinguishedName") ? result.Properties["distinguishedName"][0].ToString() : string.Empty;
                    string givenName = result.Properties.Contains("givenName") ? result.Properties["givenName"][0].ToString() : string.Empty;
                    string surname = result.Properties.Contains("sn") ? result.Properties["sn"][0].ToString() : string.Empty;
                    string title = result.Properties.Contains("title") ? result.Properties["title"][0].ToString() : string.Empty;
                    string phone = result.Properties.Contains("telephoneNumber") ? result.Properties["telephoneNumber"][0].ToString() : string.Empty;
                    // Check if the user account is active by inspecting the userAccountControl flag
                    bool isActive = (userAccountControl & 0x0002) == 0; // If the 2nd bit is not set, the account is active
                    string managerUsername = "";
                    // Check if the user has a manager
                    if (result.Properties.Contains("manager"))
                    {
                        // Get the manager's DN
                        string managerDn = result.Properties["manager"][0].ToString();
                        managerUsername = await GetUserManager(ldapPath, userName, password, managerDn);

                    }

                    // Add the AD user to the list
                    users.Add(new User
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
                        IsManager = false,
                    });

                    Console.WriteLine($"User: {samAccountName}");
                    Console.WriteLine($"Status: {(isActive ? "Active" : "Inactive")}");
                    Console.WriteLine($"Last Modified: {whenChanged}");
                    Console.WriteLine("-------------------------");
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
                // Update other attributes as needed
            }
            //else
            //{
            //    // Add new user
            //    await _userRepository.Insert(user);
            //}


        }

        // Save changes to the database
        await _userRepository.SaveChangesAsync();
    }

    private async Task SaveUsersToDatabaseBulk(List<User> users)
    {
        // Retrieve all existing users from the database
        var existingUsers = (await _userRepository.All()).Value;

        // Use a dictionary for faster lookups by username (case-insensitive)
        var existingUserDictionary = existingUsers
            .ToDictionary(u => u.Username.ToLower(), u => u, StringComparer.OrdinalIgnoreCase);

        var newUsers = new List<User>();
        var usersToUpdate = new List<User>();

        foreach (var user in users)
        {
            if (existingUserDictionary.TryGetValue(user.Username.ToLower(), out var existingUser))
            {
                // User exists, update its properties
                existingUser.Username = user.Username;
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

                usersToUpdate.Add(existingUser);
            }
            else
            {
                // New user, assign a new ID
                user.Id = Guid.NewGuid();
                newUsers.Add(user);
            }
        }

        // Perform bulk insert for new users
        if (newUsers.Any())
        {
            await _userRepository.BulkInsertAsync(newUsers);
        }

        // Perform bulk update for existing users
        if (usersToUpdate.Any())
        {
            await _userRepository.BulkUpdateAsync(usersToUpdate);
        }

        // Save changes to the database
        await _userRepository.SaveChangesAsync();
    }

}

