# Departments and Users

This document provides a comprehensive description of the department and user management features in TaskyRevamp, covering the hierarchical department structure, user lifecycle, user-department-privilege linking, and user statistics.

## Departments

### Overview

Departments represent organizational units arranged in a parent-child hierarchy. Each department has a level calculated from the root. Users are assigned to departments, and task visibility is often scoped by department membership and hierarchy.

### Domain Model: Department

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | Department name in English |
| `NameArabic` | `string` | Department name in Arabic |
| `Level` | `int` | Hierarchy level (root = 1) |
| `ParentdepartmentId` | `Guid?` | Parent department reference (null for root) |
| `Parentdepartment` | `Department` | Navigation to parent department |
| `AssignedUser` | `ICollection<User>` | Users assigned to this department |

Implements `IHasCreationMetaData` and `IHasUpdateMetaData`.

### Domain Methods

- `Update(string nameEnglish, string nameArabic)` — Updates department names.
- `CopyToDto()` — Converts to `DepartmentDto`.

### DTO: DepartmentDto

Located in `TaskyRevamp.Dto/`.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Department ID |
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `Level` | `int` | Hierarchy level |
| `ParentdepartmentId` | `Guid` | Parent department ID |
| `ParentdepartmentArabic` | `string` | Parent name in Arabic |
| `ParentdepartmentEnglish` | `string` | Parent name in English |
| `Name` | `string` | Localized name (computed based on current culture) |
| `ParentdepartmentName` | `string` | Localized parent name (computed) |
| `AssignedUsers` | `List<UserDto>` | Users in this department |
| `UsersWithTasks` | `int` | Count of users with active tasks |
| `SubDepartmentUsers` | `int` | Count of users in sub-departments |
| `CreatedBy` | `Guid?` | Creator user ID |
| `CreateDate` | `DateTime?` | Creation timestamp |
| `UpdatedBy` | `Guid?` | Last updater |
| `UpdateDate` | `DateTime?` | Last update timestamp |

### MediatR Commands and Queries

| Operation | Type | Input | Output | Description |
|-----------|------|-------|--------|-------------|
| `CreateDepartmentCommand` | Command | `DepartmentDto` | `Guid` | Creates a department. Calculates the hierarchy level based on the parent. |
| `UpdateDepartmentCommand` | Command | `DepartmentDto` | `bool` | Updates department name(s). |
| `DeleteDepartmentCommand` | Command | `Guid` | `bool` | Deletes a department. |
| `CreateDepartmentsBulkCommand` | Command | `List<DepartmentDto>` | `bool` | Bulk creates multiple departments (for import). |
| `CheckDeparmentHasUsersCommand` | Command | `Guid` | `bool` | Validates whether a department has assigned users (used before deletion). |
| `GetDepartmentsQuery` | Query | Pagination, search params | `PagedResult<DepartmentDto>` | Paginated department list with search. |
| `GetDepartmentsForDDLQuery` | Query | (none) | `List<DdlDto>` | Departments formatted for dropdown lists. |
| `GetDepartmentNamesQuery` | Query | (none) | `List<string>` | All department names. |
| `GetDepartmentsNoPagnationQuery` | Query | (none) | `List<DepartmentDto>` | All departments without pagination. |
| `GetDepartmentQuery` | Query | `Guid` | `DepartmentDto` | Single department by ID. |

### API Endpoints

Base path: `/api/department`

| Method | Endpoint | Handler |
|--------|----------|---------|
| POST | `/CreateDepartment` | `CreateDepartmentCommand` |
| POST | `/CreateDepartmentsBulk` | `CreateDepartmentsBulkCommand` |
| POST | `/UpdateDepartment` | `UpdateDepartmentCommand` |
| DELETE | `/{id}` | `DeleteDepartmentCommand` |
| GET | `/GetDepartmentsForDDL` | `GetDepartmentsForDDLQuery` |
| GET | `/GetAllDepartments` | `GetDepartmentsQuery` (with pagination and search) |
| GET | `/GetDepartmentNames` | `GetDepartmentNamesQuery` |
| GET | `/GetDepartmentsNoPagnation` | `GetDepartmentsNoPagnationQuery` |
| GET | `/{id}` | `GetDepartmentQuery` |
| GET | `/CheckDeparmentHasUsers/{id}` | `CheckDeparmentHasUsersCommand` |

### Client Consumer: DepartmentConsumer

- `GetDepartmentsForDDL()` — Dropdown list.
- `GetDepartments(pagination, search)` — Paginated with search.
- `GetDepartmentsNoPagnation(search)` — All departments.
- `GetDepartmentNames()` — Name list.
- `GetDepartmentById(id)` — Single department.
- `AddDepartment(dto)` — Create.
- `UpdateDepartment(dto)` — Update.
- `DeleteDepartment(id)` — Delete.
- `CheckDeparmentHasUsers(id)` — Pre-delete validation.
- `CreateDeprtmentsBulk(dtos)` — Batch import.

### UI Page

`Departments.razor` in `TaskyRevamp.Client/Pages/` — Department hierarchy view with parent-child relationships, user count display, and CRUD operations.

### Hierarchy Rules

- Root departments have `ParentdepartmentId = null` and `Level = 1`.
- Child departments inherit `Level = parent.Level + 1`.
- Sub-departments are supported to arbitrary depth.
- Department hierarchy is used for permission scoping (see [Authentication and Authorization](authentication-and-authorization.md)).

---

## Users

### Overview

Users represent individuals in the system. Each user can be assigned to a department and a privilege (role). Users may be imported from Active Directory or created manually. The system tracks user activity, manager status, and links to tasks.

### Domain Model: User

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `Username` | `string` | Login username |
| `NameEnglish` | `string` | Full name in English |
| `NameArabic` | `string` | Full name in Arabic |
| `Email` | `string` | Email address |
| `DistinguishedName` | `string` | Active Directory distinguished name |
| `GivenName` | `string` | First/given name |
| `Mobile` | `string` | Mobile phone number |
| `DepartmentId` | `Guid?` | Assigned department reference |
| `Department` | `Department` | Navigation to department |
| `PrivilegeId` | `Guid?` | Assigned privilege/role reference |
| `Privilege` | `Privilege` | Navigation to privilege |
| `IsActive` | `bool` | Whether the user account is active |
| `IsManager` | `bool` | Whether the user is a manager |
| `IsDeleted` | `bool` | Soft delete flag |
| `TaskAssignees` | `ICollection<TaskAssignee>` | Tasks assigned to this user |

Implements `IHasUpdateMetaData`.

### Domain Methods

- `CopyToDto()` — Converts the user model to a `UserDto`.

### DTO: UserDto

Located in `TaskyRevamp.Dto/Account/`.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | User ID |
| `userNameAR` | `string` | Arabic name |
| `userNameEN` | `string` | English name |
| `UserName` | `string` | Login username |
| `Email` | `string` | Email |
| `Mobile` | `string` | Phone |
| `DepartmentId` | `Guid?` | Department reference |
| `PrivilegeId` | `Guid?` | Privilege reference |
| `PrivilegeName` | `string` | Privilege display name |
| `IsActive` | `bool` | Active status |
| `IsManager` | `bool` | Manager flag |
| `IsDeleted` | `bool` | Deleted flag |
| `UpdatedById` | `Guid?` | Last updater |
| `UpdateDate` | `DateTime?` | Last update |
| `CreateDate` | `DateTime?` | Creation date |
| `DisplayedName` | `string` | Computed name based on current culture (English or Arabic) |

### Other User DTOs

| DTO | Purpose |
|-----|---------|
| `UserLoginDto` | Username and password for authentication |
| `UserStatisticsDto` | TotalUsers, ActiveUsers, InActiveUsers, LinkedWithTasksUsers, LoggedInUsers, LinkedWithPrivilegesUsers |
| `AssignedUserDto` | User with department and privilege info |
| `AdUser` | Active Directory user data for sync |
| `SearchableBackendDto` | For search functionality |

### MediatR Commands and Queries

| Operation | Type | Input | Output | Description |
|-----------|------|-------|--------|-------------|
| `LinkUserCommand` | Command | `UserDto, Guid deptId, Guid privId` | `bool` | Links user to a department and privilege |
| `LinkUserWithDepartmentAndPrivilegeCommand` | Command | List of assignments | `bool` | Batch linking |
| `AssignedUserToDepartmentCommand` | Command | User + department | `bool` | Assigns user to department |
| `AssignUsersToPrivilegeCommand` | Command | Users + privilege | `bool` | Assigns users to a privilege role |
| `ActivateDeActivateUserCommand` | Command | `Guid userId, bool isActive` | `bool` | Toggles user active status |
| `DeleteUserCommand` | Command | `Guid` | `bool` | Deletes user |
| `SetUserAsMangerCommand` | Command | `Guid userId, bool isManager` | `bool` | Toggles manager status |
| `IfUserHasOpenTasksOnDepartmentCommand` | Command | `Guid userId, Guid deptId` | `bool` | Checks if user has open tasks in a department |
| `GetUsersQuery` | Query | (none) | `List<UserDto>` | All users |
| `GetUsersWithPaginationQuery` | Query | Pagination, search params | `PagedResult<UserDto>` | Paginated with search |
| `GetUserByIdQuery` | Query | `Guid` | `UserDto` | Single user |
| `GetUsersBySearchValueQuery` | Query | `string searchValue, int pageSize, int offset` | `PagedResult<UserDto>` | Search users by keyword |
| `GetUsersBySearchValueByRoleQuery` | Query | Search + role filter | `PagedResult<UserDto>` | Search users by role |
| `GetAllUsersByDepartmentQuery` | Query | `Guid deptId` | `List<UserDto>` | All users in a department |
| `GetAllUsersByDepartmentWithPaginationQuery` | Query | Dept + pagination | `PagedResult<UserDto>` | Paginated department users |
| `GetAllUsersByPrivilegeQuery` | Query | Privilege + pagination | `PagedResult<UserDto>` | Users by privilege |
| `GetUnAssignedUserstoPrivilegeQuery` | Query | `Guid privilegeId` | `List<UserDto>` | Users not assigned to a privilege |
| `GetUnassignedUsersToDepartmentQuery` | Query | (none) | `List<UserDto>` | Users not assigned to any department |
| `GetUsersStatisticsQuery` | Query | (none) | `UserStatisticsDto` | Aggregate user statistics |
| `GetSelectedUserDdlByIdQuery` | Query | `List<Guid>` | `List<DdlDto>` | Dropdown items for selected users |

### API Endpoints

Base path: `/api/user`

| Method | Endpoint | Handler |
|--------|----------|---------|
| GET | `/GetUsersByDepartment/{DepartmentId}` | `GetUsersByDepartmentQuery` |
| GET | `/GetAllUsersByDepartmentWithPaginationQuery/{DepartmentId}` | Paginated department users |
| GET | `/GetUsersByPrivilegeWithPaginationQuery/{PrivilegeId}` | Users by privilege |
| GET | `/GetAllUsers` | Unassigned users |
| GET | `/GetUsersWithSearch` | Search users |
| GET | `/GetUsersStatistics` | User statistics |
| GET | `/GetUserById/{id}` | Single user |
| POST | `/LinkUser` | Link user to dept + privilege |
| POST | `/LinkUserWithDepartmentAndPrivilege` | Batch link |
| POST | `/ActivateDeActivateUser` | Toggle active |
| POST | `/SetUserAsManager` | Toggle manager |
| DELETE | `/DeleteUser/{id}` | Delete user |

### Client Consumer: UserConsumer

- `GetUsers()` — All users.
- `GetUsersWithPagination(pagination, search)` — Paginated with search.
- `GetUsersByPrivilegeIdWithPagination(privilegeId, pagination)` — Filter by role.
- `GetUsersByDepartment(departmentId)` — Department users.
- `GetUsersStatistics()` — Aggregate statistics.
- `GetAllUsersByDepartmentWithPagination()` — Paginated department users.
- `GetAllUNassignedUsers()` — Users without department assignments.
- `GetUnAssignedUsersToPrivilege()` — Available for privilege assignment.
- `GetUsersBySearchValue(searchValue, pageSize, offset)` — Searchable dropdown.
- `GetUserById(id)` — Single user.
- `CreateAssignedUser(dto)` — Create new user.
- `LinkUser(user, departmentId, privilegeId)` — Assign role and department.
- `SetUserAsManger(userId, isManager)` — Toggle manager.
- `ActivateDeActivateUser(userId, isActive)` — Account status toggle.
- `LinkUserWithDepartmentAndPrivilege(list)` — Batch assignment.
- `UpdateUser(dto)` — Edit user.
- `DeleteUser(id)` — Remove user.
- `CheckUserHasOpenTask()` — Pre-delete validation.

### User Statistics

`UserStatisticsDto` provides aggregate counts:

| Metric | Description |
|--------|-------------|
| `TotalUsers` | Total number of users in the system |
| `ActiveUsers` | Users with `IsActive = true` |
| `InActiveUsers` | Users with `IsActive = false` |
| `LinkedWithTasksUsers` | Users assigned to at least one task |
| `LoggedInUsers` | Users who have logged in |
| `LinkedWithPrivilegesUsers` | Users assigned to a privilege/role |

### UI Pages

| Page | Location | Description |
|------|----------|-------------|
| `ViewUsers.razor` | `TaskyRevamp.Client/Pages/User/` | User list with pagination, active/inactive filter, department/privilege filters |
| `LinkUser.razor` | `TaskyRevamp.Client/Pages/User/` | Assign users to departments and privileges, bulk import via Excel |
| `Departments.razor` | `TaskyRevamp.Client/Pages/` | Department hierarchy management |

### EF Core Configurations

- `UserConfiguration` — User-Department relationship with NoAction delete behavior. One-to-many for AssignedUser collection.
- `DepartmentConfiguration` — Self-referencing parent-child relationship.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Department Model | `Department.cs` (`Domain/Models/`), `DepartmentDto.cs` (`Dto/`) |
| Department Logic | `TaskyRevamp.Services/Departments/`, `DepartmentController.cs`, `DepartmentConsumer.cs` |
| User Model | `User.cs` (`Domain/Models/`), `UserDto.cs` (`Dto/Account/`) |
| User Logic | `TaskyRevamp.Services/Account/` (users), `UserController.cs`, `UserConsumer.cs` |
| Configuration | `UserConfiguration.cs`, `DepartmentConfiguration.cs` (`Infrastructure/Configurations/`) |
| Search | `DepartmentSearchFieldMap.cs`, `UserSearchFieldMap.cs` (`Services/SearchMappings/`) |
