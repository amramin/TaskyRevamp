# Background Jobs

This document provides a comprehensive description of all background job features in TaskyRevamp, covering the Hangfire integration, Active Directory synchronization, recycle bin cleanup, and delayed task status checks.

## Overview

TaskyRevamp uses **Hangfire** for background job processing. Hangfire provides persistent job storage in SQL Server, a web dashboard for monitoring, and support for recurring, delayed, and fire-and-forget jobs.

## Hangfire Configuration

### Setup (Program.cs)

Hangfire is configured in both the WebAPI and Server host `Program.cs`:

```
- Storage: SQL Server (same database as the application)
- Dashboard: Available at /HangFiredashboard
- Job scheduling: Recurring jobs registered on startup
```

### Registered Recurring Jobs

| Job | Schedule | Description |
|-----|----------|-------------|
| Recycle Bin Cleanup | `Cron.Daily` | Permanently deletes soft-deleted tasks past retention period |
| Delayed Task Check | `Cron.Daily` | Updates status of overdue tasks to Delayed |

## Active Directory Synchronization

### Location

`TaskyRevamp.Services/Jobs/ActiveDirectory/`

### SyncAdUsers

The primary AD sync job. Decorated with `[DisableConcurrentExecution(timeoutInSeconds: 3600)]` to prevent overlapping executions.

#### Methods

| Method | Signature | Description |
|--------|-----------|-------------|
| `Execute` | `()` | Main Hangfire job entry point |
| `CheckNewAddedUser` | `(string ldapPath, string userName, string password) -> List<User>` | Searches Active Directory for users created in the last 24 hours |
| `CheckUsersChanges` | `(string ldapPath, string userName, string password) -> List<User>` | Searches AD for users modified in the last 24 hours |
| `GetUserManager` | `(string ldapPath, string userName, string password) -> string` | Resolves the manager username from the manager's distinguished name |
| `SaveUsersToDatabaseBulk` | `(List<User>)` | Bulk inserts new users and bulk updates existing users |

#### AD Attributes Queried

The LDAP query fetches the following attributes for each user:

| AD Attribute | Local Field | Description |
|-------------|-------------|-------------|
| `samAccountName` | `Username` | Login name |
| `displayName` | `NameEnglish` / `NameArabic` | Display names |
| `mail` | `Email` | Email address |
| `distinguishedName` | `DistinguishedName` | Full AD path |
| `givenName` | `GivenName` | First name |
| `telephoneNumber` | `Mobile` | Phone number |
| `userAccountControl` | `IsActive` | Account status (derived from UAC flags) |
| `manager` | `IsManager` | Manager flag |

#### Sync Logic

1. **New user detection**: Queries AD with a filter for `whenCreated` within the last 24 hours.
2. **Modified user detection**: Queries AD with a filter for `whenChanged` within the last 24 hours.
3. **Manager resolution**: For each user with a manager DN, resolves the manager's `samAccountName`.
4. **Database sync**:
   - Uses a dictionary for fast lookups of existing users by username.
   - New users (not in dictionary) are bulk inserted.
   - Existing users (in dictionary) have their properties updated and are bulk updated.
   - Uses `EFCore.BulkExtensions` for efficient bulk operations.

### SyncAllUsersFT (First-Time Sync)

A one-time job that synchronizes all Active Directory users to the local database. Used during initial setup or when a full re-sync is needed.

## Recycle Bin Cleanup

### Location

`TaskyRevamp.Services/BackgroundJobs/` — `RecycleBinCleanupService`

### Method

`DeleteExpiredTasks()` — Called by Hangfire on a daily schedule.

### Logic

1. Reads the retention period from `RecycleBinSettings`:
   - **Never** (`PeriodType.Never` = -1): Keeps deleted tasks forever; no cleanup performed.
   - **Custom**: Uses a configurable number of days.
   - **Predefined periods**: 7, 14, 30 days, etc.
2. Calculates the expiration date: `DateTime.UtcNow - retentionDays`.
3. Queries the database for all tasks where:
   - `IsDeleted = true`
   - `DeleteDate <= expirationDate`
4. Permanently deletes all matching tasks from the database.

### Configuration Dependency

The cleanup behavior is controlled by the Recycle Bin Settings (see [System Configuration](system-configuration.md) — Recycle Bin Settings).

## Delayed Task Status Check

### Command

`CheckDelayedTasksCommand` — Registered as a Hangfire recurring job.

### Handler

`CheckDelayedTasksHandler` in `TaskyRevamp.Services/Tasks/`.

### Logic

1. Queries all tasks where:
   - `EndDate < DateTime.UtcNow` (past due)
   - Status is not Completed
   - Task is not deleted
2. For each matching task, updates the status to Delayed.
3. Returns `true` on completion.

### Purpose

Ensures that overdue tasks are automatically flagged as Delayed without manual intervention. This provides visibility into tasks that need attention.

## Hangfire Dashboard

- **URL**: `/HangFiredashboard`
- **Features**: View scheduled, processing, succeeded, and failed jobs.
- **Access**: Available in the application (no separate authentication configured).

## Job Resilience

- SQL Server storage ensures jobs survive application restarts.
- `DisableConcurrentExecution` prevents AD sync jobs from overlapping.
- Failed jobs are retried automatically by Hangfire's default retry policy.
- Database connection resilience (5 retries, 10-second delay) is configured for the EF Core DbContext.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| AD Sync | `SyncAdUsers.cs`, `SyncAllUsersFT.cs` (`Services/Jobs/ActiveDirectory/`) |
| Recycle Bin | `RecycleBinCleanupService.cs` (`Services/BackgroundJobs/`) |
| Delayed Tasks | `CheckDelayedTasksCommand.cs`, `CheckDelayedTasksHandler.cs` (`Services/Tasks/`) |
| Configuration | `Program.cs` (WebAPI and Server host) — Hangfire setup |
| Settings | `RecycleBinSettings.cs` (`Domain/Models/`), `RecycleBinSettingDto.cs` (`Dto/`) |
