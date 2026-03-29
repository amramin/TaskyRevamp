# Authentication and Authorization

This document provides a comprehensive description of the authentication and authorization features in TaskyRevamp, covering JWT authentication, Active Directory/LDAP integration, the privilege and permission system, and user delegation.

## Authentication Overview

TaskyRevamp supports two authentication modes:

1. **Active Directory (LDAP)** — Authenticates users against an enterprise Active Directory server.
2. **Local Database** — Authenticates users against credentials stored in the local SQL Server database.

The mode is configured via `AuthenticationMode` in application settings.

### AuthenticationMode Enum

```
ActiveDirectory  — Authenticate via LDAP
LocalDatabase    — Authenticate via local database
```

## JWT Token Authentication

### Flow

1. User submits username and password on the login page (`Login.razor`).
2. `AccountConsumer.Authenticate(loginDto)` sends credentials to the API.
3. `AuthenticateCommand` handler validates credentials (via LDAP or local DB).
4. On success, a JWT token is generated with user claims.
5. The token is stored in the browser's `localStorage["bearerToken"]`.
6. All subsequent API requests include the token in the `Authorization: Bearer {token}` header.
7. The API middleware validates the token on every request.

### Token Generation

The JWT token is generated with the following claims:

| Claim | Value | Description |
|-------|-------|-------------|
| `ClaimTypes.Name` | User ID (Guid) | Primary identity claim |
| `ClaimTypes.NameIdentifier` | User ID (Guid) | Redundant identifier |
| `Username` | Username string | Login username |
| `Email` | User email | Email address |
| `NameEnglish` | English name | User's name in English |
| `NameArabic` | Arabic name | User's name in Arabic |
| `Id` | User ID (Guid) | Explicit ID claim |
| `DelegatedUsersId` | Comma-separated GUIDs | IDs of users who have delegated to this user |

**Token configuration:**
- Algorithm: HS256 (symmetric key)
- Default expiry: 24 hours (configurable)
- Clock skew: 0 seconds (strict validation)

### Token Validation (Client Side)

`TaskyService.CheckForToken()`:
1. Reads token from `localStorage["bearerToken"]`.
2. Parses JWT claims to check the `exp` (expiration) claim.
3. If expired: navigates to the login page with a return URL.
4. If valid: sets the `Authorization` header and `BlazorCulture` header for API calls.

### MediatR Command

| Command | Input | Output | Description |
|---------|-------|--------|-------------|
| `AuthenticateCommand` | `string username, string password` | `string?` (JWT token) | Authenticates user, syncs AD data if applicable, generates JWT with delegation info |

### API Endpoint

Base path: `/api/account`

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Authenticate` | Accepts `UserLoginDto` (Username, Password), returns JWT token string |

### Client Consumer: AccountConsumer

- `Authenticate(loginDto)` — Sends login request, receives JWT token.
- `SyncUsers(loggedInUserId)` — Triggers AD user synchronization, returns count of synced users.
- `GetUsers(pageNumber, pageSize)` — Gets paginated user list.

## Active Directory Integration

### Overview

TaskyRevamp integrates with enterprise Active Directory via LDAP for user authentication and data synchronization. Users can be automatically imported and kept up to date through background jobs.

### LDAP Authentication Flow

1. `AuthenticateCommand` handler creates an LDAP connection using configured `ldapPath`, `ldapUsername`, and `ldapPassword`.
2. Searches AD for the user by `samAccountName`.
3. Validates credentials by attempting an LDAP bind.
4. On success, syncs user attributes to the local database.
5. Generates JWT token with synced data.

### AD User Attributes Synced

| AD Attribute | Local Property | Description |
|-------------|----------------|-------------|
| `samAccountName` | `Username` | Login name |
| `displayName` | `NameEnglish` / `NameArabic` | Display names |
| `mail` | `Email` | Email address |
| `distinguishedName` | `DistinguishedName` | Full AD path |
| `givenName` | `GivenName` | First name |
| `telephoneNumber` | `Mobile` | Phone number |
| `userAccountControl` | `IsActive` | Account status (derived from UAC flags) |
| `manager` | `IsManager` | Manager flag (derived from manager attribute) |

### Background Sync Jobs

Located in `TaskyRevamp.Services/Jobs/ActiveDirectory/`:

**SyncAdUsers** (Hangfire recurring job):
- Decorated with `[DisableConcurrentExecution(timeoutInSeconds: 3600)]` to prevent overlapping runs.
- `CheckNewAddedUser(ldapPath, userName, password)` — Searches AD for users created in the last 24 hours, returns `List<User>`.
- `CheckUsersChanges(ldapPath, userName, password)` — Searches AD for users modified in the last 24 hours, returns `List<User>`.
- `GetUserManager(ldapPath, userName, password)` — Resolves the manager username from the manager DN.
- `SaveUsersToDatabaseBulk(List<User>)` — Bulk inserts new users and bulk updates existing users using dictionary lookups for performance.

**SyncAllUsersFT** — One-time initial sync of all AD users (first-time setup).

## Authorization: Privileges and Permissions

### Overview

TaskyRevamp uses a privilege-based authorization model. A **Privilege** is a named role that contains granular permissions across multiple modules. Users are assigned to one privilege, which determines their access rights.

### Domain Model: Privilege

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `GeneralModulePermissions` | `List<GeneralModulePermission>` | Permissions for general modules |
| `ReportModulePermissions` | `List<ReportModulePermission>` | Permissions for report modules |
| `TaskModuleUserDepartmentPermissions` | `List<TaskModuleUserDepartment>` | Task permissions scoped to user's department |
| `TaskModuleExternalDepartmentPermissions` | `List<TaskModuleExternalDepartment>` | Task permissions for external departments |

Methods:
- `SetData(PrivilegeDto)` — Updates privilege from DTO.
- `CopyToDto()` — Converts to DTO.

### Permission Modules

#### General Module Permissions

Controls CRUD access to system modules.

**GeneralModule** model:
| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | Module name in English |
| `NameArabic` | `string` | Module name in Arabic |
| `HasView` | `bool` | Module supports view permission |
| `HasEdit` | `bool` | Module supports edit permission |
| `HasAdd` | `bool` | Module supports add permission |
| `HasDelete` | `bool` | Module supports delete permission |

**GeneralModulePermission** (per privilege):
| Property | Type | Description |
|----------|------|-------------|
| `GeneralModuleId` | `Guid` | Module reference |
| `IsView` | `bool` | Can view |
| `IsEdit` | `bool` | Can edit |
| `IsAdd` | `bool` | Can add |
| `IsDelete` | `bool` | Can delete |
| `DelegationFromUser` | `bool` | Delegation from user permission |
| `DelegationToUser` | `bool` | Delegation to user permission |
| `DelegationFromUserDepartments` | `bool` | Delegation from user departments |
| `DelegationToUserDepartments` | `bool` | Delegation to user departments |

#### Report Module Permissions

Controls access to reports.

**ReportModule** model:
| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | Report name in English |
| `NameArabic` | `string` | Report name in Arabic |
| `HintEnglish` | `string` | Description in English |
| `HintArabic` | `string` | Description in Arabic |

**ReportModulePermission** (per privilege):
| Property | Type | Description |
|----------|------|-------------|
| `ReportModuleId` | `Guid` | Report module reference |
| `IsActive` | `bool` | Whether user has access to this report |

#### Task Module — User Department Permissions

Controls task access based on the user's own department hierarchy.

**TaskModuleUserDepartment**:
| Property | Type | Description |
|----------|------|-------------|
| `PermissionId` | `Guid` | Privilege reference |
| `SelectedOption` | varies | Selected access option |
| `IsActive` | `bool` | Whether permission is active |
| `IsManagerTasks` | `bool` | Can see manager tasks |
| `IsEmployeeTasks` | `bool` | Can see employee tasks |
| `DirectionType` | `DirectionType` | Hierarchical access direction |
| `DirectionLevel` | `int` | Number of levels in the direction |

#### Task Module — External Department Permissions

Controls task access for departments outside the user's own.

**TaskModuleExternalDepartment**:
| Property | Type | Description |
|----------|------|-------------|
| `DepartmentId` | `Guid` | Target department |
| `IsIncludeSubDepartment` | `bool` | Include sub-departments |
| `PermissionId` | `Guid` | Privilege reference |
| `IsManagerTasks` | `bool` | Can see manager tasks |
| `IsEmployeeTasks` | `bool` | Can see employee tasks |
| `Status` | `int` | Task status filter |
| `Source` | `int` | Task source filter |
| `DirectionType` | `DirectionType` | Hierarchical direction |
| `DirectionLevel` | `int` | Direction levels |

### MediatR Commands and Queries

**Privilege management:**

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreatePrivilegeCommand` | Command | `PrivilegeDto` | `bool` |
| `UpdatePrivilegeCommand` | Command | `PrivilegeDto` | `bool` |
| `DeletePrivilegeCommand` | Command | `Guid` | `bool` |
| `CheckPrivilegeIsLinkedWithUsersCommand` | Command | `Guid` | `bool` |
| `GetPrivilegesQuery` | Query | Pagination params | `PagedResult<PrivilegeDto>` |
| `GetPrivilegesWithoutPaginationQuery` | Query | (none) | `List<PrivilegeDto>` |
| `GetPrivilegeByIdQuery` | Query | `Guid` | `PrivilegeDto` |
| `GetPrivilegeNamesQuery` | Query | (none) | `List<string>` |
| `GetPrivilegeNameByIdQuery` | Query | `Guid` | `string` |

**Module queries:**

| Operation | Type | Output |
|-----------|------|--------|
| `GetGeneralModulesQuery` | Query | `List<GeneralModuleDto>` |
| `GetReportModulesQuery` | Query | `List<ReportModuleDto>` |

### DTO: PrivilegeDto

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Privilege ID |
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `GeneralModulePermissions` | `List<GeneralModulePermissionDto>` | General permissions |
| `ReportModulePermissions` | `List<ReportModulePermissionDto>` | Report permissions |
| `TaskModuleUserDepartmentPermissions` | `List<TaskModuleUserDepartmentPermissionDto>` | Task department permissions |
| `TaskModuleExternalDepartmentPermissions` | `List<TaskModuleExternalDepartmentDto>` | External department permissions |
| `CreatedById` | `Guid` | Creator |
| `CreateDate` | `DateTime` | Creation date |
| `UpdatedById` | `Guid?` | Last updater |
| `UpdateDate` | `DateTime?` | Last update |

### API Endpoints

- `/api/privilege` — Privilege CRUD
- `/api/generalmodule` — General module listing
- `/api/reportmodule` — Report module listing

### Client Consumers

- `PrivilegeConsumer` — Full CRUD and validation operations.
- `GeneralModuleConsumer` — Lists general permission modules.
- `ReportModuleConsumer` — Lists report permission modules.

## User Delegation

### Overview

User delegation allows a user to delegate their task responsibilities to another user for a specified date range. When delegation is active, the delegate can act on behalf of the original user. Delegated user IDs are included in the JWT token claims.

### Domain Model: UserDelegation

Located in `TaskyRevamp.Domain/Models/` (namespace: `UserDelegations`).

| Property | Type | Description |
|----------|------|-------------|
| `FromUserId` | `Guid` | User delegating (delegator) |
| `ToUserId` | `Guid` | User receiving delegation (delegate) |
| `FromUser` | `User` | Navigation to delegator |
| `Touser` | `User` | Navigation to delegate |
| `FromDate` | `DateTime` | Delegation start date |
| `ToDate` | `DateTime` | Delegation end date |
| `DelegationOption` | `DelegationOptions` | Type of delegation |

Implements `IHasCreationMetaData` and `IHasUpdateMetaData`.

### MediatR Commands and Queries

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateUserDelegationCommand` | Command | `UserDelegationDto` | `bool` |
| `UpdateUserDelegationCommand` | Command | `UserDelegationDto` | `bool` |
| `DeleteUserDelegationCommand` | Command | `Guid` | `bool` |
| `GetUserDelegationQuery` | Query | `Guid delegationId` | `UserDelegationDto` |
| `GetUserDelegationsQuery` | Query | (none) | `List<UserDelegationDto>` |
| `GetUsersDelegationsQuery` | Query | `Guid userId` | `List<UserDelegationDto>` |

### API Endpoints

Base path: `/api/userdelegation`

### Client Consumer: UserDelegationConsumer

- `GetAllUserDelegation(pagination)` — Paginated delegation list.
- `ChangeActiveValue(id)` — Toggle delegation active state.
- `CreateUserDelegation(dto)` — Create new delegation.
- `GetById(id)` — Get single delegation.
- `Delete(id)` — Remove delegation.
- `update(dto)` — Edit delegation.

### UI Page

`UserDelegations.razor` in `TaskyRevamp.Client/Pages/` — Lists delegations with create, edit, delete, and active/inactive toggle.

## UI Pages

| Page | Location | Description |
|------|----------|-------------|
| `Login.razor` | `TaskyRevamp.Client/Pages/Account/` | JWT login with username/password, remember me, return URL |
| `Privileges.razor` | `TaskyRevamp.Client/Pages/` | Role/privilege management with permission matrix |
| `UserDelegations.razor` | `TaskyRevamp.Client/Pages/` | Delegation setup and management |

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Authentication | `AuthenticateCommand.cs` (`Services/Account/`), `AccountController.cs`, `AccountConsumer.cs` |
| AD Integration | `SyncAdUsers.cs`, `SyncAllUsersFT.cs` (`Services/Jobs/ActiveDirectory/`) |
| Privileges | `Privilege.cs`, `GeneralModule.cs`, `ReportModule.cs` (`Domain/Models/`), `PrivilegeController.cs` |
| Permissions | `GeneralModulePermission.cs`, `ReportModulePermission.cs`, `TaskModuleUserDepartment.cs`, `TaskModuleExternalDepartment.cs` |
| Delegation | `UserDelegation.cs` (`Domain/Models/`), `UserDelegationController.cs`, `UserDelegationConsumer.cs` |
| JWT Config | `Program.cs` (WebAPI and Server host) |
