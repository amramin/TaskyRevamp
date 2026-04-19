# API Reference

This document provides a comprehensive reference for all REST API endpoints in TaskyRevamp. All endpoints are served by the ASP.NET Core Web API project (`TaskyRevamp.WebAPI`).

## Common Response Format

All API responses are wrapped in `CommonApiResponse<T>`:

| Property | Type | Description |
|----------|------|-------------|
| `version` | `string` | API version (e.g., "1.0") |
| `statusCode` | `int` | HTTP status code |
| `success` | `bool` | Whether the operation succeeded |
| `data` | `T` | Response payload (null on error) |
| `message` | `string` | Error message (null on success) |

## Authentication

All endpoints (except `/api/account/Authenticate`) require a valid JWT token in the `Authorization: Bearer {token}` header.

A `BlazorCulture` header is sent with every request to set the response language (`en-US` or `ar-EG`).

---

## Task Controller (`/api/task`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/CreateTask` | `CreateTaskDto` (body) | `string` (task ID) | Create a new task |
| GET | `/CompleteTask/{id}` | `Guid` (path) | `bool` | Mark task as 100% complete |
| POST | `/ReopenTask` | `TaskCommentDto` (body) | `bool` | Reopen completed task with reason |
| GET | `/RestoreTask/{taskId}/{restoreOption}` | `Guid, int` (path) | `bool` | Restore soft-deleted task |
| POST | `/RejectTask` | `TaskCommentDto` (body) | `bool` | Reject task assignment |
| POST | `/RequestChangeDueDate` | `ChangeEndDateRequestDto` (body) | `Guid` | Request due date extension |
| POST | `/UpdateTask` | `CreateTaskDto` (body) | `bool` | Update task properties |
| GET | `/UpdateTasksDepartment/{oldId}/{newId}` | `Guid, Guid` (path) | `bool` | Bulk update department |
| GET | `/ChangeTaskProgress/{taskId}/{progress}` | `Guid, int` (path) | `bool` | Update progress percentage |
| GET | `/UpdateTaskPriority/{taskId}/{PriorityId}` | `Guid, Guid` (path) | `bool` | Change task priority |
| GET | `/CheckOpenedTaskForUser/{userId}` | `Guid` (path) | `bool` | Check if user has open tasks |
| GET | `/CheckDelayedTasks` | (none) | `bool` | Trigger delayed task check |
| DELETE | `/SoftDeleteTask/{id}/{userId}` | `Guid, Guid` (path) | `bool` | Soft delete a task |
| GET | `/GetTask/{id}/{currentUserId}` | `Guid, Guid` (path) | `CreateTaskDto` | Get task with view tracking |
| GET | `/GetTasks` | Query params | `PagedResult<CreateTaskDto>` | Get paginated task list |
| GET | `/GetDeletedTasks` | Query params | `PagedResult<CreateTaskDto>` | Get soft-deleted tasks |
| GET | `/GetTasksByTaskIds` | Query params | `List<CreateTaskDto>` | Get parent/main tasks |

### GetTasks Query Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `pageNumber` | `int` | Page number (1-based) |
| `pageSize` | `int` | Items per page |
| `sortByColumnName` | `string` | Sort column name |
| `sortAscending` | `bool` | Sort direction |
| `searchFields` | `List<SearchFieldTask>` | Fields to search |
| `searchText` | `string` | Search keyword |
| `viewType` | `int` | View type (0=List, 1=Calendar, 2=Timeline, 3=Board) |
| `viewTypeId` | `Guid?` | View-specific filter |
| `isCompleted` | `bool` | Filter by completion |

---

## Task Assignees Controller (`/api/taskassignees`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `TaskAssigneesDto` (body) | `Guid` | Create assignee |
| POST | `/Update` | `TaskAssigneesDto` (body) | `bool` | Update assignee |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Remove assignee |
| GET | `/GetByTask/{taskId}` | `Guid` (path) | `List<TaskAssigneesDto>` | Get task assignees |

---

## Task Comment Controller (`/api/taskcomment`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `TaskCommentDto` (body) | `bool` | Create comment |
| POST | `/Update` | `TaskCommentDto` (body) | `bool` | Update comment |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete comment |
| GET | `/GetByTask/{taskId}` | `Guid` (path) | `List<TaskCommentDto>` | Get task comments |
| GET | `/{id}` | `Guid` (path) | `TaskCommentDto` | Get single comment |

---

## Task Checklist Controller (`/api/taskchecklist`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `TaskChecklistDto` (body) | `Guid` | Create checklist |
| POST | `/Update` | `TaskChecklistDto` (body) | `bool` | Update checklist |
| DELETE | `/{taskId}` | `Guid` (path) | `bool` | Delete checklist |
| GET | `/GetByTask/{taskId}` | `Guid` (path) | `List<TaskChecklistDto>` | Get task checklists |

---

## Checklist Item Controller (`/api/checklistitem`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `ChecklistItemDto` (body) | `Guid` | Create item |
| POST | `/Update` | `ChecklistItemDto` (body) | `bool` | Update item |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete item |
| GET | `/GetByChecklist/{checklistId}` | `Guid` (path) | `List<ChecklistItemDto>` | Get checklist items |

---

## Task Attachment Controller (`/api/taskattachment`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/AddAttachment/{taskId}` | Multipart form data | `bool` | Upload attachment |
| GET | `/GetByTask/{taskId}` | `Guid` (path) | `List<AttachmentWithNameDto>` | Get task attachments |
| GET | `/GetFileInfo/{fileId}` | `Guid` (path) | `byte[]` | Download file |
| DELETE | `/{attachmentId}/{fileId}` | `Guid, Guid` (path) | `bool` | Delete attachment |

---

## Task Escalation Controller (`/api/taskescalation`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `TaskEscalationDto` (body) | `Guid` | Create escalation |
| POST | `/Update` | `TaskEscalationDto` (body) | `bool` | Update escalation |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete escalation |
| GET | `/GetByTask/{taskId}` | `Guid?` (path) | `List<TaskEscalationDto>` | Get task escalations |

---

## Change End Date Request Controller (`/api/changeenddaterequest`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `ChangeEndDateRequestDto` (body) | `Guid` | Create request |
| POST | `/UpdateStatus/{requestId}/{status}` | Path params | `bool` | Approve/reject |
| GET | `/GetByTask/{taskId}` | `Guid` (path) | `List<ChangeEndDateRequestDto>` | Get requests by task |
| GET | `/GetAll` | Pagination params | `PagedResult<ChangeEndDateRequestDto>` | Get all requests |

---

## Task Dependency Controller (`/api/taskdependency`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| GET | `/GetDependOnTasks/{taskId}` | `Guid` + pagination | Tasks this depends on |
| GET | `/GetDependentTasks/{taskId}` | `Guid` + pagination | Tasks that depend on this |

---

## Pinned Tasks Controller (`/api/pinnedtasks`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `PinnedTasksDto` (body) | `Guid` | Pin task |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Unpin task |
| GET | `/GetAll` | (none) | `List<PinnedTasksDto>` | Get pinned tasks |

---

## Account Controller (`/api/account`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Authenticate` | `UserLoginDto` (body) | `string` (JWT) | Login and get token |
| POST | `/SyncUsers/{userId}` | `Guid` (path) | `int` | Trigger AD sync |
| GET | `/GetUsers` | Pagination params | `PagedResult<UserDto>` | Get users |

---

## User Controller (`/api/user`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| GET | `/GetUsersByDepartment/{DepartmentId}` | `Guid` (path) | `List<UserDto>` | Users by department |
| GET | `/GetAllUsersByDepartmentWithPaginationQuery/{DepartmentId}` | `Guid` + pagination | `PagedResult<UserDto>` | Paginated dept users |
| GET | `/GetUsersByPrivilegeWithPaginationQuery/{PrivilegeId}` | `Guid` + pagination | `PagedResult<UserDto>` | Users by privilege |
| GET | `/GetAllUsers` | (none) | `List<UserDto>` | Unassigned users |
| GET | `/GetUsersWithSearch` | Search params | `PagedResult<UserDto>` | Search users |
| GET | `/GetUsersStatistics` | (none) | `UserStatisticsDto` | Aggregate stats |
| GET | `/GetUserById/{id}` | `Guid` (path) | `UserDto` | Single user |
| POST | `/LinkUser` | `UserDto` + IDs (body) | `bool` | Link to dept + privilege |
| POST | `/LinkUserWithDepartmentAndPrivilege` | List (body) | `bool` | Batch link |
| POST | `/ActivateDeActivateUser` | `Guid, bool` (body) | `bool` | Toggle active |
| POST | `/SetUserAsManager` | `Guid, bool` (body) | `bool` | Toggle manager |
| DELETE | `/DeleteUser/{id}` | `Guid` (path) | `bool` | Delete user |

---

## Department Controller (`/api/department`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/CreateDepartment` | `DepartmentDto` (body) | `Guid` | Create department |
| POST | `/CreateDepartmentsBulk` | `List<DepartmentDto>` (body) | `bool` | Bulk create |
| POST | `/UpdateDepartment` | `DepartmentDto` (body) | `bool` | Update department |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete department |
| GET | `/GetDepartmentsForDDL` | (none) | `List<DdlDto>` | Dropdown list |
| GET | `/GetAllDepartments` | Pagination + search | `PagedResult<DepartmentDto>` | Paginated list |
| GET | `/GetDepartmentNames` | (none) | `List<string>` | Name list |
| GET | `/GetDepartmentsNoPagnation` | (none) | `List<DepartmentDto>` | All departments |
| GET | `/{id}` | `Guid` (path) | `DepartmentDto` | Single department |
| GET | `/CheckDeparmentHasUsers/{id}` | `Guid` (path) | `bool` | Pre-delete check |

---

## Privilege Controller (`/api/privilege`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/CreatePrivilege` | `PrivilegeDto` (body) | `bool` | Create privilege |
| POST | `/UpdatePrivilege` | `PrivilegeDto` (body) | `bool` | Update privilege |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete privilege |
| GET | `/GetPrivileges` | Pagination | `PagedResult<PrivilegeDto>` | Paginated list |
| GET | `/{id}` | `Guid` (path) | `PrivilegeDto` | Single privilege |
| GET | `/GetPrivilegeNames` | (none) | `List<string>` | Name list |
| GET | `/CheckPrivilegeIsLinkedWithUsers/{id}` | `Guid` (path) | `bool` | Pre-delete check |

---

## User Delegation Controller (`/api/userdelegation`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `UserDelegationDto` (body) | `bool` | Create delegation |
| POST | `/Update` | `UserDelegationDto` (body) | `bool` | Update delegation |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete delegation |
| GET | `/GetAll` | Pagination | `PagedResult<UserDelegationDto>` | All delegations |
| GET | `/{id}` | `Guid` (path) | `UserDelegationDto` | Single delegation |
| POST | `/ChangeActiveValue/{id}` | `Guid` (path) | `bool` | Toggle active |

---

## System Configuration Controllers

### Priority Settings (`/api/prioritysetting`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Create` | Create priority |
| POST | `/Update` | Update priority |
| DELETE | `/{id}` | Delete priority |
| POST | `/Retrive` | Restore soft-deleted |
| POST | `/RetriveDelete` | Undelete hard deleted |
| POST | `/UpdateOrder` | Reorder priorities |
| GET | `/GetAll` | Paginated list |
| GET | `/{id}` | Single priority |
| GET | `/CheckRelated/{id}` | Check related tasks |

### Status Settings (`/api/statussetting`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Update` | Update status |
| GET | `/GetAll` | All statuses |
| GET | `/{id}` | Single status |

### Source Settings (`/api/sourcesetting`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Create` | Create source |
| POST | `/Update` | Update source |
| DELETE | `/{id}` | Delete source |
| POST | `/Retrive` | Restore |
| GET | `/GetAll` | Paginated list |
| GET | `/{id}` | Single source |

### Type Settings (`/api/typesetting`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Create` | Create type |
| POST | `/Update` | Update type |
| DELETE | `/{id}` | Delete type |
| POST | `/Retrive` | Restore |
| GET | `/GetAll` | Paginated list |
| GET | `/{id}` | Single type |

### Other Settings Controllers

| Controller | Base Path | Operations |
|-----------|-----------|------------|
| AddTaskSettingController | `/api/addtasksetting` | Get, Update |
| RejectionSettingController | `/api/rejectionsetting` | Get, Update |
| RecycleBinSettingController | `/api/recyclebinsetting` | Get, Update |
| ViewTaskSettingController | `/api/viewtasksetting` | Get, GetActive, Update |
| DefaultColumnsSettingController | `/api/defaultcolumnssetting` | Get, Update |
| FilterFieldsSettingController | `/api/filterfieldssetting` | Get, Update |
| DefaultViewSettingsController | `/api/defaultviewsettings` | Get, Update, UpdateSubTaskLevel |
| WorkingDaySettingsController | `/api/workingdaysettings` | Get, Update |
| WeeklyReportSettingController | `/api/weeklyreportsetting` | Get, Update |
| SystemIdentityController | `/api/systemidentity` | Get, Update |

---

## Notification Template Controller (`/api/notificationtypetemplate`)

| Method | Endpoint | Input | Output | Description |
|--------|----------|-------|--------|-------------|
| POST | `/Create` | `NotificationTypeTemplateDto` | `bool` | Create template |
| POST | `/Update` | `NotificationTypeTemplateDto` | `bool` | Update template |
| POST | `/UpdateAll` | `List<NotificationTypeTemplateDto>` | `bool` | Batch update |
| DELETE | `/{id}` | `Guid` (path) | `bool` | Delete template |
| GET | `/GetAll` | (none) | `List<NotificationTypeTemplateDto>` | All templates |
| GET | `/{id}` | `Guid` (path) | `NotificationTypeTemplateDto` | Single template |

---

## Permission Module Controllers

### General Module (`/api/generalmodule`)

| Method | Endpoint | Output |
|--------|----------|--------|
| GET | `/GetAll` | `List<GeneralModuleDto>` |

### Report Module (`/api/reportmodule`)

| Method | Endpoint | Output |
|--------|----------|--------|
| GET | `/GetAll` | `List<ReportModuleDto>` |

---

## Send Email Controller (`/api/sendemail`)

| Method | Endpoint | Input | Description |
|--------|----------|-------|-------------|
| POST | `/SendEmail` | `EmailDto` (body) | Send email asynchronously |

---

## Error Handling

All endpoints use the `ExceptionHandlingMiddleware` which maps exceptions to HTTP status codes:

| Exception Type | HTTP Status |
|---------------|-------------|
| `BadRequestException` | 400 |
| `NotFoundException` | 404 |
| `ValidationException` | 422 |
| `NoDataException` | Custom |
| Default | 500 |

Validation errors from FluentValidation are returned grouped by property name in the response message.

## Middleware Pipeline

Every request passes through:

1. `RequestLocalization` — Sets culture from headers/cookies.
2. `ExceptionHandlingMiddleware` — Catches and formats exceptions.
3. `LocalizationExceptionMiddleware` — Localizes error messages.
4. `Authentication & Authorization` — Validates JWT token.
5. `ExtractCustomHeaderAttribute` — Reads `BlazorCulture` header and sets thread culture.
6. `ResponseWrapper` — Wraps all responses in `CommonApiResponse<T>`.
