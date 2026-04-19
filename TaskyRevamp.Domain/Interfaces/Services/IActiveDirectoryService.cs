using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Domain.Interfaces.Services;

/// <summary>
/// Abstracts Active Directory operations used across Authenticate, SyncADUsers, and SyncAllUsersFT.
/// Eliminates duplicated property extraction, searcher configuration, and manager lookup logic (HI-03).
/// </summary>
public interface IActiveDirectoryService
{
    /// <summary>
    /// Extracts standard AD user properties from a directory search result into an <see cref="AdUser"/> DTO.
    /// Replaces the ~10-line property extraction pattern that was duplicated in 4 locations.
    /// </summary>
    AdUser ExtractAdUser(object searchResult);

    /// <summary>
    /// Resolves a manager's sAMAccountName from their distinguished name.
    /// Replaces the GetUserManager method that was duplicated in 3 locations.
    /// </summary>
    string? ResolveManagerUsername(string managerDn);

    /// <summary>
    /// Authenticates a user against Active Directory and returns their details.
    /// Returns an <see cref="AdUser"/> with <c>IsAuthenticated = false</c> if authentication fails.
    /// </summary>
    AdUser AuthenticateAndGetUser(string username, string password);

    /// <summary>
    /// Loads the standard set of AD properties into a DirectorySearcher.
    /// Replaces the 10+ PropertiesToLoad.Add calls duplicated in 4 locations.
    /// </summary>
    void ConfigureSearcherProperties(object searcher);
}
