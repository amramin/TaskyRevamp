# Refactoring & Clean Code Audit

This document lists all identified clean code violations, code smells, and refactoring opportunities across the TaskyRevamp codebase. Items are organized by severity and layer. Each item is intended to be addressed in future pull requests.

> **Last audited**: March 2026
> **Scope**: All 8 projects, ~579 C# files, ~118 Razor components

---

## Table of Contents

- [Critical Priority](#critical-priority)
- [High Priority](#high-priority)
- [Medium Priority](#medium-priority)
- [Low Priority](#low-priority)

---

## Critical Priority

These items represent architectural issues, potential runtime failures, or security concerns that should be addressed first.

### ~~CR-01: Hardcoded Status GUIDs Throughout Service Layer~~ ✅ RESOLVED

**Status**: Fixed — `TaskStatusConstants` class created in `TaskyRevamp.Domain/Constants/` and all hardcoded GUIDs replaced across 20+ files.

---

### ~~CR-02: Sync-Over-Async Anti-Pattern (`.Result` Calls)~~ ✅ RESOLVED

**Status**: Fixed — All `.Result` calls replaced with proper `await` patterns. `FirstOrDefaultAsNoTracking` renamed to async `FirstOrDefaultAsNoTrackingAsync` in `IRepository<T>` and `EfRepository`. `GetDelegatedUsers` in `Authenticate.cs` converted to async `GetDelegatedUsersAsync`.

---

### CR-03: Fat Generic Repository (645 Lines, 42 Methods)

**Severity**: Critical | **SOLID Violation**: SRP, ISP, LSP | **Layer**: Infrastructure

`EfRepository.cs` is a god class with 42 public methods, unimplemented methods throwing `NotImplementedException`, and mixed sync/async patterns.

**File**: `TaskyRevamp.Infrastructure/EfRepository.cs`
- 645 lines with 42 public methods (violates SRP and ISP)
- Lines 369-392, 394-414, 416-421: Unimplemented methods throw `NotImplementedException` (violates LSP)
- Lines 444, 465-466: Duplicate `SaveChangesAsync()` calls
- String-based include properties (lines 55-61, 103-115) are fragile
- Line 632: Separate `.CountAsync()` in paginated queries (extra database roundtrip)

**Recommended fix**: Split into focused repository interfaces (`IReadRepository<T>`, `IWriteRepository<T>`, `IPagedRepository<T>`). Remove or implement unimplemented methods.

---

### CR-04: Security - Plaintext Credential Storage

**Severity**: Critical | **Layer**: Infrastructure

Network share credentials stored in plaintext configuration without encryption.

**Files affected**:
- `TaskyRevamp.Infrastructure/NetworkShareAccess.cs` (lines 17-35)
- `TaskyRevamp.Infrastructure/FileManagement.cs` (lines 29-31)

**Recommended fix**: Use Azure Key Vault, Windows DPAPI, or `IDataProtector` for credential storage.

---

### CR-05: DbContext Registered as Transient

**Severity**: Critical | **Layer**: WebAPI

`EfDbContext` is registered as `Transient` instead of `Scoped`, violating EF Core best practices and potentially causing tracking issues across a single request.

**File**: `TaskyRevamp.WebAPI/Program.cs` (line 74)
```csharp
builder.Services.AddTransient<DbContext, EfDbContext>();
```

**Recommended fix**: Change to `AddScoped<DbContext, EfDbContext>()`.

---

### CR-06: Duplicate Validation Rules

**Severity**: Critical | **Layer**: Services

The only FluentValidation validator has duplicated rules.

**File**: `TaskyRevamp.Services/Tasks/Commands/CreateTaskValidator.cs` (lines 14-16)
```csharp
RuleFor(x => x.CreateTaskDto.Title)
    .NotEmpty().WithMessage(localizer["TitleRequired"])
    .MaximumLength(100).WithMessage(localizer["TitleMaxLength"]);
RuleFor(x => x.CreateTaskDto.Title)  // DUPLICATE
    .NotEmpty().WithMessage(localizer["TitleRequired"])
    .MaximumLength(100).WithMessage(localizer["TitleMaxLength"]);
```

**Recommended fix**: Remove the duplicate rule block.

---

## High Priority

These items represent significant code quality issues, SOLID violations, or maintainability concerns.

### ~~HI-01: God Handlers / Oversized Command Handlers~~ ✅ MOSTLY RESOLVED

**Status**: Further fixed — `IActiveDirectoryService` extracted to centralise AD logic. `Authenticate.cs` reduced from 319→196 lines (AD auth + property extraction + manager lookup removed). `SyncADUsers.cs` reduced from 335→195 lines. `SyncAllUsersFT.cs` reduced from 246→185 lines. Remaining: `GetTasksQuery.cs` (442 lines) still oversized with timeline/filter/sort/mapping — tracked for future decomposition.

**Severity**: High | **SOLID Violation**: SRP | **Layer**: Services

Multiple handlers have too many responsibilities, making them hard to test and maintain.

| Handler | Lines | Responsibilities |
|---------|-------|-----------------|
| `GetTasksQuery.cs` | 442 | Timeline filtering (11 types), status/source/type filtering, search mapping, pagination, sorting, culture formatting, DTO mapping |
| ~~`Authenticate.cs`~~ | ~~319→196~~ | ~~AD authentication, user sync, token generation, delegation handling, manager lookup~~ |
| ~~`SyncADUsers.cs`~~ | ~~335→195~~ | ~~AD user extraction, bulk updates, property extraction, manager lookup~~ |
| ~~`SyncAllUsersFT.cs`~~ | ~~245→185~~ | ~~Full AD sync, user extraction, bulk operations~~ |
| ~~`UpdateTaskCommand.cs`~~ | ~~165~~ | ~~Task updates, assignee management, comment handling, checklist management, status logic~~ |
| ~~`CreateTaskCommand.cs`~~ | ~~118~~ | ~~Task creation, status determination, comment insertion, checklist creation, dependency validation~~ |

**Recommended fix**: Extract shared logic into domain services (e.g., `ITaskStatusService`, `IActiveDirectoryUserService`, `ITaskValidationService`).

---

### ~~HI-02: Duplicated Status Determination Logic~~ ✅ RESOLVED

**Status**: Fixed — All three handlers now delegate to `ITaskStatusDeterminer.DetermineStatus()` and `AreDependenciesCompleted()`. Duplicated if/else chains removed.

---

### ~~HI-03: Duplicated Active Directory User Extraction Logic~~ ✅ RESOLVED

**Status**: Fixed — `IActiveDirectoryService` created in `TaskyRevamp.Domain/Interfaces/Services/`, implemented in `TaskyRevamp.Services/ActiveDirectory/ActiveDirectoryService.cs`. Centralises `ExtractAdUser()`, `ConfigureSearcherProperties()`, `ResolveManagerUsername()`, and `AuthenticateAndGetUser()`. All three files refactored to use the service. ~170 lines of duplicated code eliminated.

---

### ~~HI-04: Business Logic in Service Layer (Should Be in Domain)~~ ✅ RESOLVED

**Status**: Fixed — Status determination extracted to `ITaskStatusDeterminer` (HI-02). Rejection period logic extracted to `ITaskRejectionService` in `TaskyRevamp.Domain/Interfaces/Services/`, implemented in `TaskyRevamp.Services/Tasks/Services/TaskRejectionService.cs`. `RejectTaskCommand` now delegates to `GetRejectionPeriodDays()` and `CanRejectTask()` instead of inline calculations.

---

### ~~HI-05: TaskItem God Class~~ ✅ PARTIALLY RESOLVED

**Status**: Partially fixed — `CopyToDto()` and `SetData()` extracted to `TaskItemMappingExtensions` in `TaskyRevamp.Services/Tasks/TaskItemMappingExtensions.cs` as `ToDto()` and `ApplyDto()` extension methods. DTO mapping is no longer a domain concern. All 12+ callers updated. TaskItem reduced from 430→385 lines. Remaining: weight/progress calculation, subtask hierarchy, escalation, and change request logic still in TaskItem — tracked for future extraction.

---

### HI-06: Missing Error Handling in All Blazor Consumers

**Severity**: High | **Layer**: Client

API consumer classes return `response.Data` without checking if the response was successful, risking `NullReferenceException` at runtime.

**Files affected**:
- `TaskyRevamp.Client/Consumer/TaskConsumer.cs` (lines 100, 106, 112)
- `TaskyRevamp.Client/Consumer/UserConsumer.cs` (lines 100-101, 107, 141-142)
- `TaskyRevamp.Client/Consumer/DepartmentConsumer.cs` (multiple methods)
- `TaskyRevamp.Client/Consumer/TaskAttachmentConsumer.cs` (line 20)
- `TaskyRevamp.Client/Consumer/AccountConsumer.cs` (multiple methods)

**Recommended fix**: Add response validation helper method and check `response.Success` before accessing `response.Data`.

---

### HI-07: Missing Error Handling in All Razor Page Components

**Severity**: High | **Layer**: Client (UI)

Most page components have zero try-catch blocks around async API calls, with no error feedback to users.

**Files affected**:
- `TaskListView.razor` (1,482 lines, lines 1278+)
- `ViewUsers.razor` (638 lines, zero try-catch)
- `Form.razor` (789 lines)
- `LinkUser.razor` (lines 113-134)
- `ChangeEndDateRequests.razor` (lines 150-151, 225-265)
- `TypeConfiguration.razor` (lines 149-151)
- `SourceConfiguration.razor` (lines 149-151)
- `NotificationTypeTemplate/Form.razor` (line 244)
- `MainLayout.razor` (lines 164-193)
- `LoginLayout.razor` (lines 54-64)
- `CultureSelector.razor` (lines 80, 88)

**Recommended fix**: Add standardized error handling pattern with user-facing error messages and toast notifications.

---

### HI-08: Oversized Razor God Components

**Severity**: High | **Layer**: Client (UI)

Multiple Razor components have too many responsibilities and should be split.

| Component | Lines | Should Split Into |
|-----------|-------|-------------------|
| `SVGCrud.razor` | 1,937 | Asset-based SVG loader (eliminate switch statement) |
| `TaskListView.razor` | 1,482 | TaskTable, TaskFilters, TaskActions, TaskColumnManager |
| `Form.razor` (Task) | 789 | FileUploadField, DatePickerField, SelectDropdown, per-field components |
| `ViewUsers.razor` | 638 | UserTable, UserFilters, UserActions |
| `MainTasks.razor` | 564 | TabNavigation, TaskViewSwitcher, TaskLoader |
| `NavMenu.razor` | 369 | MenuConfiguration, MenuRenderer, SettingsSubmenu |

---

### HI-09: Improper Exception Handling Patterns

**Severity**: High | **Layer**: Services, WebAPI

Inconsistent exception handling across the codebase, with some handlers silently swallowing errors.

**Examples**:
- `Authenticate.cs` (lines 44-126): 80+ lines in try block; catches generic `Exception` and re-throws as `InvalidOperationException`, losing original stack trace
- `SyncADUsers.cs` (lines 29-34): `catch (Exception ex) { Console.WriteLine(ex); }` — silently ignores errors
- `SyncAllUsersFT.cs` (lines 36-42): Same silent error swallowing
- Generic `throw new Exception("message")` throughout (e.g., `CreateTaskCommand.cs` line 46, `UpdateTaskCommand.cs` line 47)

**Recommended fix**: Define custom exception hierarchy. Use specific exception types. Never catch generic `Exception` without re-throwing. Replace `Console.WriteLine` with structured logging.

---

### HI-10: Two Conflicting Exception Handling Middlewares

**Severity**: High | **Layer**: WebAPI

Two separate exception handling middlewares are registered, creating confusion about which handles what.

**Files**:
- `TaskyRevamp.WebAPI/Middleware/ExceptionHandlingMiddleware.cs`
- `TaskyRevamp.WebAPI/Middleware/LocalizedExceptionMiddleware.cs`
- Both registered in `Program.cs` (lines 194 and 225)

**Recommended fix**: Consolidate into a single middleware that handles localization and error formatting.

---

### HI-11: Missing Validation Across DTOs

**Severity**: High | **Layer**: DTO

No DTOs have data annotation validation attributes.

**Files affected** (partial list):
- `TaskChecklistDto.cs` — missing `[Required]` on Title
- `CreateTaskDto.cs` — missing `[Required]` on EndDate
- `MailSettings.cs` — missing `[Required]` on Server, SenderEmail
- `UserDto.cs` — missing validation on all properties
- `DepartmentDto.cs` — missing validation
- `ChangeEndDateRequestDto.cs` — missing validation on NewEndDate

**Recommended fix**: Add `[Required]`, `[MaxLength]`, `[Range]` attributes. Or rely entirely on FluentValidation validators (but create them first).

---

### HI-12: Localization Logic Embedded in DTOs

**Severity**: High | **SOLID Violation**: SRP | **Layer**: DTO

13+ DTO classes contain `Thread.CurrentThread.CurrentCulture` checks for localized display properties. DTOs should be pure data carriers.

**Files affected**:
- `TaskyRevamp.Dto/SystemConfiguration/PriorityDto.cs` (`Name` property)
- `TaskyRevamp.Dto/SystemConfiguration/StatusSettingsDto.cs` (`Name` property)
- `TaskyRevamp.Dto/SystemConfiguration/SourceDto.cs` (`DisplayedName` property)
- `TaskyRevamp.Dto/SystemConfiguration/TypeDto.cs` (`DisplayedName` property)
- `TaskyRevamp.Dto/SystemConfiguration/DefaultColumnsSettingDto.cs` (`Name` property)
- `TaskyRevamp.Dto/SystemConfiguration/AddTaskSettingDto.cs` (`Name` property)
- `TaskyRevamp.Dto/SystemConfiguration/FilterFieldsSettingDto.cs` (`Name` property)
- `TaskyRevamp.Dto/GeneralDto/DdlDto.cs` (`Name` property)
- `TaskyRevamp.Dto/Permissions/GeneralModule/GeneralModuleDto.cs`
- `TaskyRevamp.Dto/Permissions/ReportModule/ReportModuleDto.cs`
- `TaskyRevamp.Dto/Permissions/PrivilegeDto.cs`
- `TaskyRevamp.Dto/UserDelegation/UserDelegationDto.cs`
- `TaskyRevamp.Dto/NotificationDtos/NotificationDto.cs`

**Recommended fix**: Move localization logic to a presentation/mapping layer. DTOs should expose `NameEnglish` and `NameArabic` separately.

---

### HI-13: CreateTaskDto Is Too Large (76 Properties)

**Severity**: High | **SOLID Violation**: SRP | **Layer**: DTO

`CreateTaskDto.cs` has 76 properties mixing creation fields, display fields, and UI-specific fields.

**File**: `TaskyRevamp.Dto/TaskDto/CreateTaskDto.cs`

**Recommended fix**: Split into `CreateTaskRequest`, `UpdateTaskRequest`, `TaskResponse`, and `TaskSummaryDto`.

---

### HI-14: Duplicate Repository in Single Handler

**Severity**: High | **Layer**: Services

`UpdateTaskCommand.cs` injects both `ITaskRepository` and `IRepository<TaskItem>` — two repositories for the same entity.

**File**: `TaskyRevamp.Services/Tasks/Commands/UpdateTaskCommand.cs` (lines 21, 24)
```csharp
private readonly ITaskRepository _taskRepository;
private readonly IRepository<TaskItem> _taskrepo;  // Duplicate
```

**Recommended fix**: Remove one; use only `ITaskRepository`.

---

### HI-15: Missing Unit of Work Pattern

**Severity**: High | **Layer**: Infrastructure

Each repository calls `SaveChangesAsync()` independently. There is no transaction management across multiple repository operations within a single use case.

**Impact**: Distributed operations (e.g., creating a task + assignees + checklists) lack atomicity.

**Recommended fix**: Implement a `IUnitOfWork` wrapper or use EF Core's built-in change tracking with a single `SaveChanges` call per request.

---

### HI-16: Incorrect HTTP Methods on State-Changing Endpoints

**Severity**: High | **Layer**: WebAPI

GET endpoints are used for operations that modify server state, violating REST conventions.

**File**: `TaskyRevamp.WebAPI/Controllers/TaskController.cs`
- Line 38: `[HttpGet("CompleteTask/{id}")]` — should be POST/PUT
- Line 70: `[HttpGet("UpdateTasksDepartment/{oldId}/{newId}")]` — should be PUT
- Line 77: `[HttpGet("ChangeTaskProgress/{taskId}/{progress}")]` — should be PUT
- Line 82: `[HttpGet("UpdateTaskPriority/{taskId}/{PriorityId}")]` — should be PUT
- Line 92: `[HttpGet("CheckDelayedTasks")]` — has side effects, should be POST

**Recommended fix**: Change to appropriate HTTP verbs (POST for actions, PUT for updates).

---

### HI-17: Background Job Coupled to Controller

**Severity**: High | **Layer**: WebAPI

A Hangfire recurring job directly calls a controller action method.

**File**: `TaskyRevamp.WebAPI/Program.cs` (line 226)
```csharp
RecurringJob.AddOrUpdate<TaskController>("Check-Delayed-Tasks", j => j.CheckDelayedTasks(), ...);
```

**Recommended fix**: Create a dedicated `IDelayedTaskCheckService` and use that from the background job.

---

## Medium Priority

These items affect code quality and developer experience but do not pose immediate runtime risks.

### ME-01: Anemic Domain Models

**Severity**: Medium | **Layer**: Domain

Most domain entities are pure data containers with only `SetData()` and `CopyToDto()` methods, no business behavior.

**Files affected**:
- `TaskyRevamp.Domain/Models/Task/Department.cs` — only `SetData()`, `CopyToDto()`, `Update()`
- `TaskyRevamp.Domain/Models/Users/User.cs` — only `CopyToDto()`, `Update()`
- `TaskyRevamp.Domain/Models/AuditLog/AuditLog.cs` — pure data container
- `TaskyRevamp.Domain/Models/Task/TaskViews.cs` — only `UpdateViewedTime()`
- `TaskyRevamp.Domain/Models/Permissions/ReportModulePermission.cs` — only `CopyToDto()`
- `TaskyRevamp.Domain/Models/Permissions/TaskModuleExternalDepartment.cs` — only `CopyToDto()`
- `TaskyRevamp.Domain/Models/Permissions/TaskModuleUserDepartment.cs` — only `CopyToDto()`
- `TaskyRevamp.Domain/Models/Notification/NotificationTypeTemplate.cs` — only `SetData()`, `CopyToDto()`
- All `SystemConfiguration/*` models — only `CopyToDto()`

**Recommended fix**: Identify behaviors that belong on domain entities and move them from handlers.

---

### ME-02: Missing Encapsulation on Domain Collections

**Severity**: Medium | **Layer**: Domain

Collection properties on domain entities have public setters, allowing external code to replace internal state.

**File**: `TaskyRevamp.Domain/Models/Task/TaskItem.cs`
- `public List<Guid> AssignedDepartmentIds { set; get; }` — should be read-only
- `public List<TaskDependencies>? Dependencies { get; set; }` — should be encapsulated
- `public List<ChangeEndDateRequest> ChangeEndDateRequests = new List<ChangeEndDateRequest>();` — field initialization mixed with property pattern

**Other files**:
- `ChecklistItem.cs` — all public get/set without encapsulation
- `TaskAssignee.cs` — all public get/set with inconsistent spacing (`get ;`)
- All SystemConfiguration classes — public properties without validation

**Recommended fix**: Use `private set` or `init` accessors. Expose collections as `IReadOnlyCollection<T>` with `Add`/`Remove` domain methods.

---

### ME-03: Missing Validation in Domain Constructors

**Severity**: Medium | **Layer**: Domain

Entity constructors accept parameters without validation, allowing invalid state.

**Files affected**:
- `TaskItem.cs` (line 154): Only checks `end < start`. Missing: title not null, priority valid, creator valid, weight range
- `TaskItem.cs` (line 224): `UpdateTitle()` accepts any string without null/empty check
- `TaskItem.cs` (line 280): `SetReminder()` does not validate date is in the future
- `TaskItem.cs` (line 306): `AddSubtask()` does not validate subtask is not null
- `ChecklistItem.cs` (line 25): Constructor does not validate `title`
- `Attachment.cs` (line 21): Constructor does not validate `fileName`, `fileId`, `size >= 0`
- `Department.cs` (line 23): Constructor does not validate `nameEn`, `nameAr`
- `User.cs` (line 35): Constructor does not validate `userName`
- `Privilege.cs` (lines 37-41): Constructor does not validate names
- `TaskEscalation.cs` (lines 24-36): Constructor does not validate `reason`, `level > 0`, `toUser`

**Recommended fix**: Add guard clauses in all constructors and mutation methods.

---

### ME-04: Missing Value Objects

**Severity**: Medium | **Layer**: Domain

Primitive types are used where value objects would better express intent and enforce invariants.

**Missing value objects**:
- **LocalizedString** — `NameEnglish`/`NameArabic` pairs appear in 15+ entities
- **Color** — `NameColor`/`BackgroundColor` string pairs in `PrioritySettings`, `StatusSettings`
- **EmailAddress** — `string Email` in `User`, `MailSettings`
- The existing `Weight`, `Progress`, `DateRange` value objects are not used consistently in `TaskItem`

**Recommended fix**: Create value objects for these repeated patterns; use them across entities.

---

### ME-05: Inconsistent Naming Conventions

**Severity**: Medium | **Layer**: Domain, DTO, Client

Naming inconsistencies throughout the codebase.

**Domain**:
- `Department.Parentdepartment` → should be `ParentDepartment`
- `TaskChecklist.items` → should be `Items`
- `TaskComment.taskItem` → should be `TaskItem`
- `ChecklistItem.taskChecklist` → should be `TaskChecklist`
- `TaskAssignee.taskId` parameter → inconsistent with `TaskItemId` property

**DTO**:
- `CreateTaskDto.weight` → should be `Weight`
- `CreateTaskDto.ActualProcess` → should be `ActualProgress`
- `CreateTaskDto.Plannedweight` → should be `PlannedWeight`
- `UserDto.userNameAR` / `userNameEN` → should be `UserNameAr` / `UserNameEn`
- `DepartmentDto.ParentdepartmentId` → should be `ParentDepartmentId`
- `TaskAssigneesDto.taskId` → should be `TaskId`
- `NotificationTypeTemplateDto.moduleType` → should be `ModuleType`
- `ChangeEndDateRequestDto.oldEndDate` → should be `OldEndDate`

**Client**:
- `DepartmentConsumer.CreateDeprtmentsBulk()` → should be `CreateDepartmentsBulk()`
- `DepartmentConsumer.CheckDeparmentHasUsers()` → should be `CheckDepartmentHasUsers()`
- `HttpClientExtenstions.cs` → filename should be `HttpClientExtensions.cs`

---

### ME-06: Spelling Errors in Code

**Severity**: Medium | **Layer**: DTO, Domain, Client

**Typos found**:
- `TaskViewsDto.ViewdAt` → should be `ViewedAt`
- `FileDto.Extention` → should be `Extension`
- `AssignedUserDto.UsrIds` → should be `UserIds`
- `UserStatisticsDto.LinkedWithPrivilagesUsers` → should be `LinkedWithPrivilegesUsers`
- `TaskEscalation.cs` (line 35): `TriggerAfter = TrgeStatus;` → **bug**: should be `TriggerAfter = triggerAfter;`

---

### ME-07: Dead Code and Placeholder Classes

**Severity**: Medium | **Layer**: Multiple

**Commented-out code** (should be removed — version control has history):
- `TaskyRevamp.Domain/Models/Task/Comment.cs` — entire file commented out
- `TaskyRevamp.Domain/Models/Task/TaskItem.cs` (lines 137-138, 181-188) — commented sections
- `TaskyRevamp.Domain/Models/Task/TaskAssignee.cs` (lines 20-21, 72-94) — commented implementation
- `TaskyRevamp.Services/SystemNotification/CreateActionRequestCommand.cs` (lines 28-63) — 30+ lines
- `TaskyRevamp.Services/Tasks/Commands/UpdateTaskCommand.cs` (lines 53, 105-110)
- `TaskyRevamp.Infrastructure/EFDbContext.cs` (lines 253-254) — commented soft delete

**Empty placeholder classes**:
- `TaskyRevamp.Domain/Class1.cs`
- `TaskyRevamp.Infrastructure/Class1.cs`
- `TaskyRevamp.Localization/Class1.cs`

**Dead middleware**:
- `TaskyRevamp.WebAPI/Pipeline/ExtractCustomHeaderMiddleware.cs` (lines 4-11) — does nothing

**Unused files**:
- `TaskyRevamp.WebAPI/WeatherForecast.cs` — template leftover

**Recommended fix**: Delete all dead code and placeholder files.

---

### ME-08: Audit Tracking Logic Mixed Into DbContext

**Severity**: Medium | **SOLID Violation**: SRP | **Layer**: Infrastructure

`EFDbContext.cs` contains audit field population logic (CreatedBy, UpdatedBy, timestamps) inside `SaveChangesAsync()`, mixing persistence concerns with audit behavior.

**File**: `TaskyRevamp.Infrastructure/EFDbContext.cs` (lines 66-98)

**Additional issue**: `IHttpContextAccessor` dependency in DbContext breaks in background jobs where no HTTP context exists (line 21, 50-54).

**Recommended fix**: Extract audit tracking to a `SaveChangesInterceptor` or a dedicated `AuditService`.

---

### ME-09: DateTime Inconsistencies

**Severity**: Medium | **Layer**: Multiple

Mixed usage of `DateTime.Now` and `DateTime.UtcNow` across the codebase.

**Examples**:
- `ChangeEndDateRequest.cs` (line 30): Uses `DateTime.Now`
- `TaskItem.cs` (line 96): Uses `DateTime.UtcNow`
- `TaskItem.cs` (line 175): Uses `DateTime.Now` for `AssigneeDate`
- Various handlers: Mix of both

**Recommended fix**: Standardize on `DateTime.UtcNow` everywhere. Consider an `IDateTimeProvider` for testability.

---

### ME-10: Missing `[ProducesResponseType]` on API Endpoints

**Severity**: Medium | **Layer**: WebAPI

No controller actions have `[ProducesResponseType]` attributes. This means:
- Swagger/OpenAPI documentation is incomplete
- Clients cannot discover response types automatically
- No compile-time contract enforcement

**All 34 controllers are affected.**

**Recommended fix**: Add `[ProducesResponseType(typeof(T), StatusCodes.Status200OK)]` to all endpoints.

---

### ME-11: Culture-Specific Logic Scattered in Query Handlers

**Severity**: Medium | **Layer**: Services

Culture checks (`currentCulture == "ar"`) are repeated throughout query handlers for name resolution.

**Files affected**:
- `TaskyRevamp.Services/Users/Queries/GetUsersWithPaginationQuery.cs` (lines 45-47, 77-100)
- `TaskyRevamp.Services/Departments/Queries/GetDepartmentsQuery.cs` (lines 23, 52-53, 99)
- `TaskyRevamp.Services/Tasks/Queries/GetTasksQuery.cs` (line 44)

**Recommended fix**: Create culture-aware extension methods or a localized name resolver service.

---

### ME-12: Eager Loading 14 Entities Unconditionally

**Severity**: Medium | **Layer**: Infrastructure

`TaskRepository.cs` always loads 14 related entities via `.Include()` regardless of whether they are needed.

**File**: `TaskyRevamp.Infrastructure/Repositories/TaskRepository.cs` (lines 47-60)

**Recommended fix**: Use `.AsSplitQuery()` for complex includes. Create different query methods for different use cases (summary vs. detail).

---

### ME-13: ValidationBehavior Loses Structured Errors

**Severity**: Medium | **Layer**: WebAPI

The validation pipeline converts structured validation errors to a plain string.

**File**: `TaskyRevamp.WebAPI/Pipeline/ValidationBehavior.cs` (line 37)
```csharp
throw new ValidationException(errorsDictionary.Values.ToString());
```

**Recommended fix**: Pass `errorsDictionary` to the exception so middleware can return structured error responses.

---

### ME-14: Duplicate Code in TypeConfiguration.razor and SourceConfiguration.razor

**Severity**: Medium | **Layer**: Client (UI)

These two pages share ~95% identical logic and markup for CRUD operations, differing only in the entity type.

**Files**:
- `TaskyRevamp.Client/Pages/SystemConfiguration/TypeSetting/TypeConfiguration.razor` (270 lines)
- `TaskyRevamp.Client/Pages/SystemConfiguration/SourceSetting/SourceConfiguration.razor` (272 lines)

**Recommended fix**: Create a generic `ConfigurationCrud<T>` base component.

---

### ME-15: TaskyService.cs Is Too Large (520 Lines)

**Severity**: Medium | **Layer**: Client

`TaskyService.cs` handles HTTP client management, token validation, query string building, file validation, MIME type detection, theme management, and culture management.

**File**: `TaskyRevamp.Client/TaskyService.cs`
- Duplicate methods: `CheckForToken()` and `CheckForToken2()` (lines 231-317)
- Public `httpClient` field (should be private)
- Magic strings for file extensions (lines 49-51)
- Unreachable code after return statements (lines 266-271, 312-315)

**Recommended fix**: Split into `TokenService`, `QueryBuilderService`, `FileValidationService`, `ThemeService`.

---

### ME-16: ResponseWrapper Middleware Buffers Entire Response

**Severity**: Medium | **Layer**: WebAPI

`ResponseWrapper.cs` reads the entire response body into memory before re-wrapping it. Problematic for large file downloads.

**File**: `TaskyRevamp.WebAPI/Middleware/ResponseWrapper.cs` (lines 38-39)
- Hardcoded path exclusion `"Table/save"` (line 31)
- Deserializes then re-serializes response body (lines 52-79)

**Recommended fix**: Use middleware only for specific content types. Skip file/binary responses.

---

### ME-17: Missing Service Abstractions

**Severity**: Medium | **Layer**: Services

The following abstractions are missing, leading to duplicated logic:

1. **`ITaskStatusService`** — Status determination appears in 3+ handlers
2. **`IActiveDirectoryUserService`** — User extraction duplicated across SyncADUsers, SyncAllUsersFT, Authenticate
3. **`ITaskValidationService`** — Validation logic scattered across handlers
4. **`IDepartmentHierarchyService`** — Recursive logic in UpdateDepartmentCommand (lines 73-113)
5. **`IGetUserManagerService`** — Duplicated in Authenticate and SyncADUsers

---

## Low Priority

These items improve code quality and consistency but have minimal impact on functionality.

### LO-01: Primitive Obsession in Domain Models

**Severity**: Low | **Layer**: Domain

Primitive types used where strongly typed alternatives would improve clarity.

**Examples**:
- `TaskItem.cs`: Uses `int` for progress instead of existing `Progress` value object
- `TaskItem.cs`: Uses `int?` for weight instead of existing `Weight` value object
- `TaskModuleExternalDepartment.cs`: `List<int>? PermissionId`, `List<Guid> Status`, `List<Guid> Source`
- `GeneralModulePermission.cs`: `List<int>?` and `List<Guid>?` for permissions

---

### LO-02: Magic Numbers in Domain Logic

**Severity**: Low | **Layer**: Domain

Unnamed constants in domain calculations.

- `TaskItem.cs` (lines 42, 54, 66, 79): `100` for max weight/progress
- `Weight.cs`, `Progress.cs` (lines 11-12): `0` and `100` without named constants
- `ChangeEndDateRequest.cs` (line 41): Magic string `"ar"` for culture
- `BaseEntity.cs` (lines 43-44): `"Castle.Proxies."` and `"Proxy"` for EF proxy detection

---

### LO-03: Accessibility Issues in Razor Components

**Severity**: Low | **Layer**: Client (UI)

Multiple components lack proper ARIA attributes and semantic HTML.

**Examples**:
- `TaskListView.razor`: No ARIA labels on dynamic elements, missing role on tables
- `NavMenu.razor`: Navigation items missing `aria-current`, active roles
- `MainLayout.razor`: Logo `<img>` missing `alt` text, user dropdown needs `aria-labels`
- `Pagination.razor`: Nav missing `aria-label`, page buttons need `aria-current`
- `SortableTable.razor`: Table needs `summary`, headers need `scope="col"`, missing `aria-sort`
- `PopupHost.razor`: Modal missing `role="alertdialog"`, buttons need `aria-labels`
- `RichTextEditorComponent.razor`: RTE has no `aria-label`

---

### LO-04: Hardcoded UI Strings Not Using Localization

**Severity**: Low | **Layer**: Client (UI)

Multiple components have hardcoded strings instead of using `Loc[]`.

**Examples**:
- `MainTasks.razor`: "List view", "Kanban view", "Gantt chart" (lines 39, 55, 69)
- `MainLayout.razor`: "All rights" copyright message (line 121)
- `MainLayout.razor`: "User name" search placeholder (line 40)
- `CultureSelector.razor`: Hardcoded Arabic/English text (lines 18, 62, 68)
- `RichTextEditorComponent.razor`: MaxLength default and ButtonText default
- `Pagination.razor`: "Items per page" (line 12)
- `SortableTable.razor`: "No data available" (line 60)

---

### LO-05: Service/Configuration Class in DTO Project

**Severity**: Low | **SOLID Violation**: SRP | **Layer**: DTO

`ColumnPreferenceService.cs` is a service class located in the DTO project (`TaskyRevamp.Dto/TaskViews/`), violating project boundaries.

**Recommended fix**: Move to the Client project's Services folder.

---

### LO-06: Duplicate JWT Parsing Logic

**Severity**: Low | **Layer**: Client

JWT token parsing is implemented in two places with identical logic.

**Files**:
- `TaskyRevamp.Client/Extensions/CustomAuthenticationStateProvider.cs` (lines 69-85)
- `TaskyRevamp.Client/Extensions/StaticMethods.cs` (lines 18-24)

**Recommended fix**: Consolidate into a single `JwtParser` utility.

---

### LO-07: Magic File Size Constant

**Severity**: Low | **Layer**: Client

Hardcoded file size limit without named constant.

**File**: `TaskyRevamp.Client/FileManagementService.cs` (line 86)
```csharp
if (file.Size > 26214400)  // 25MB
```

**Recommended fix**: Extract to a named constant or configuration value.

---

### LO-08: Inconsistent Value Object Constructors

**Severity**: Low | **Layer**: Domain

`Weight.cs`, `Progress.cs`, `DateRange.cs` are defined as `record` types but have parameterless constructors that do nothing, breaking immutability contracts.

**Recommended fix**: Remove parameterless constructors. Enforce validation only through parameterized constructors.

---

### LO-09: NotificationService Creates Unnecessary Lists

**Severity**: Low | **Layer**: Infrastructure

`NotificationService.cs` (lines 37-45) creates a single-item list and then loops over it — an unnecessary allocation.

**Recommended fix**: Process the single item directly without wrapping in a list.

---

### LO-10: Incomplete NotificationService Implementation

**Severity**: Low | **Layer**: Infrastructure

`NotificationService.cs` has an incomplete TODO comment: `//TODO GetUserMailData` (line 82).

**Recommended fix**: Implement or remove the TODO.

---

### LO-11: Redundant HttpContextAccessor Registrations

**Severity**: Low | **Layer**: WebAPI

`Program.cs` registers `HttpContextAccessor` twice (lines 73, 77).

**Recommended fix**: Remove duplicate registration.

---

### LO-12: Popup Parameter Classes in Service File

**Severity**: Low | **Layer**: Client

`PopupService.cs` contains multiple popup parameter classes and an enum alongside the service class (lines 89-125).

**Recommended fix**: Move popup parameter DTOs to separate files.

---

### LO-13: SVGCrud.razor Should Use Asset-Based Approach

**Severity**: Low | **Layer**: Client (UI)

`SVGCrud.razor` is 1,937 lines of raw SVG definitions in a massive switch statement (100+ cases). This is unmaintainable.

**Recommended fix**: Load SVGs from files/assets, or auto-generate from an SVG sprite sheet. Replace the switch statement with a dictionary lookup.

---

### LO-14: ExceptionHandler Logging Creates New LoggerFactory Per Exception

**Severity**: Low | **Layer**: WebAPI

`UnhandledExceptionBehaviour.cs` (lines 37-74) creates a new `LoggerFactory` instance for every exception, wasting resources.

**File**: `TaskyRevamp.WebAPI/Exeptions/UnhandledExceptionBehaviour.cs` (lines 46-47)

**Recommended fix**: Inject `ILogger<T>` via constructor.

---

## Summary Statistics

| Category | Critical | High | Medium | Low |
|----------|----------|------|--------|-----|
| Magic Strings/Numbers | 1 | — | — | 1 |
| Async Anti-patterns | 1 | — | — | — |
| God Classes/Components | — | 3 | — | 1 |
| Duplicate Code | — | 2 | 1 | 1 |
| Missing Validation | 1 | 1 | 1 | — |
| Exception Handling | — | 2 | — | — |
| Missing Error Handling (UI) | — | 2 | — | — |
| SOLID Violations | 1 | 5 | 3 | 1 |
| Naming/Spelling | — | — | 2 | — |
| Dead Code | — | — | 1 | — |
| Anemic Domain | — | — | 1 | — |
| Missing Encapsulation | — | — | 1 | — |
| Security | 1 | — | — | — |
| DI/Configuration | 1 | 1 | — | 1 |
| API Design | — | 1 | 1 | — |
| Accessibility | — | — | — | 1 |
| Localization | — | 1 | 1 | 1 |
| **Totals** | **6** | **18** | **12** | **7** |

**Grand total: 43 refactoring items**

---

## Suggested PR Sequence

The following sequence minimizes risk by addressing foundational issues first:

1. **PR 1 — Quick Wins**: Remove dead code, fix typos, delete placeholder classes (ME-07, ME-06)
2. **PR 2 — Status Constants**: Extract hardcoded GUIDs to constants (CR-01)
3. **PR 3 — Fix Async**: Replace all `.Result` calls with `await` (CR-02)
4. **PR 4 — Fix DbContext Registration**: Change Transient to Scoped (CR-05)
5. **PR 5 — Fix Duplicate Validation**: Remove duplicate rule in CreateTaskValidator (CR-06)
6. **PR 6 — HTTP Method Corrections**: Fix REST verb violations (HI-16)
7. **PR 7 — Error Handling (UI)**: Add try-catch to all Razor API calls (HI-07)
8. **PR 8 — Consumer Error Handling**: Validate response success before accessing Data (HI-06)
9. **PR 9 — Exception Middleware Consolidation**: Merge two exception middlewares (HI-10)
10. **PR 10 — Extract Status Service**: Create `ITaskStatusService` (HI-02, HI-04)
11. **PR 11 — Split God Handlers**: Decompose large handlers (HI-01)
12. **PR 12 — Repository Refactoring**: Split fat repository, add Unit of Work (CR-03, HI-15)
13. **PR 13 — Domain Validation**: Add guard clauses to constructors (ME-03)
14. **PR 14 — DTO Cleanup**: Split CreateTaskDto, fix naming, remove localization from DTOs (HI-12, HI-13, ME-05)
15. **PR 15 — Component Splitting**: Break up god Razor components (HI-08)
