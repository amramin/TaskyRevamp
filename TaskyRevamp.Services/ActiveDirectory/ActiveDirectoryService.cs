using System.DirectoryServices;
using Microsoft.Extensions.Options;
using TaskyRevamp.Domain.Interfaces.Services;
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Services.ActiveDirectory;

/// <summary>
/// Centralised Active Directory operations.
/// Replaces duplicated property extraction in SyncADUsers, SyncAllUsersFT, and Authenticate (HI-03).
/// </summary>
public class ActiveDirectoryService : IActiveDirectoryService
{
    private readonly LdapSettings _ldapSettings;

    /// <summary>Standard AD property names loaded for every user query.</summary>
    private static readonly string[] StandardProperties =
    {
        "samAccountName",
        "displayName",
        "mail",
        "distinguishedName",
        "givenName",
        "sn",
        "title",
        "telephoneNumber",
        "userAccountControl",
        "manager"
    };

    public ActiveDirectoryService(IOptions<LdapSettings> ldapSettings)
    {
        _ldapSettings = ldapSettings.Value;
    }

    /// <inheritdoc />
    public void ConfigureSearcherProperties(object searcher)
    {
        if (searcher is not DirectorySearcher ds)
            throw new ArgumentException("Expected a DirectorySearcher instance.", nameof(searcher));

        foreach (var prop in StandardProperties)
            ds.PropertiesToLoad.Add(prop);
    }

    /// <inheritdoc />
    public AdUser ExtractAdUser(object searchResult)
    {
        if (searchResult is not SearchResult result)
            throw new ArgumentException("Expected a SearchResult instance.", nameof(searchResult));

        string samAccountName = GetStringProperty(result, "samAccountName");
        string displayName = GetStringProperty(result, "displayName");
        string email = GetStringProperty(result, "mail");
        string distinguishedName = GetStringProperty(result, "distinguishedName");
        string givenName = GetStringProperty(result, "givenName");
        string surname = GetStringProperty(result, "sn");
        string title = GetStringProperty(result, "title");
        string phone = GetStringProperty(result, "telephoneNumber");

        int userAccountControl = result.Properties.Contains("userAccountControl")
            ? Convert.ToInt32(result.Properties["userAccountControl"][0])
            : 0;
        bool isActive = (userAccountControl & 0x0002) == 0;

        string managerUsername = string.Empty;
        if (result.Properties.Contains("manager"))
        {
            string managerDn = result.Properties["manager"][0].ToString()!;
            managerUsername = ResolveManagerUsername(managerDn) ?? string.Empty;
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

    /// <inheritdoc />
    public string? ResolveManagerUsername(string managerDn)
    {
        try
        {
            using var entry = new DirectoryEntry(_ldapSettings.Path, _ldapSettings.Username, _ldapSettings.Password);
            using var searcher = new DirectorySearcher(entry)
            {
                Filter = $"(distinguishedName={managerDn})"
            };
            searcher.PropertiesToLoad.Add("sAMAccountName");

            var result = searcher.FindOne();
            if (result != null && result.Properties.Contains("sAMAccountName"))
            {
                return result.Properties["sAMAccountName"][0].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving manager's username: {ex.Message}");
        }

        return null;
    }

    /// <inheritdoc />
    public AdUser AuthenticateAndGetUser(string username, string password)
    {
        try
        {
            // Step 1: Authenticate with user's credentials
            using (var authEntry = new DirectoryEntry(_ldapSettings.Path, username, password))
            {
                try
                {
                    _ = authEntry.NativeObject;
                }
                catch
                {
                    return new AdUser { IsAuthenticated = false };
                }
            }

            // Step 2: Retrieve user details using service account
            using var entry = new DirectoryEntry(_ldapSettings.Path, _ldapSettings.Username, _ldapSettings.Password);
            using var searcher = new DirectorySearcher(entry)
            {
                Filter = $"(sAMAccountName={username})"
            };
            ConfigureSearcherProperties(searcher);

            var result = searcher.FindOne();
            if (result == null)
            {
                return new AdUser { IsAuthenticated = false };
            }

            return ExtractAdUser(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during AD authentication: {ex.Message}");
            throw new InvalidOperationException(ex.Message);
        }
    }

    private static string GetStringProperty(SearchResult result, string propertyName)
    {
        return result.Properties.Contains(propertyName)
            ? result.Properties[propertyName][0].ToString() ?? string.Empty
            : string.Empty;
    }
}
