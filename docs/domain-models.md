# Domain Models

This document provides a comprehensive reference for all domain models in TaskyRevamp, located in `TaskyRevamp.Domain/Models/`. It describes every entity, its properties, relationships, and the interfaces it implements.

## Common Interfaces

### IEntityIdentifier

All entities implement this interface.

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |

### IHasCreationMetaData

Tracks who created the entity and when.

| Property | Type | Description |
|----------|------|-------------|
| `CreatedById` | `Guid` | Creator user ID |
| `CreateDate` | `DateTime` | Creation timestamp |
| `CreatedBy` | `User` | Navigation to creator |

### IHasUpdateMetaData

Tracks who last updated the entity and when.

| Property | Type | Description |
|----------|------|-------------|
| `UpdatedById` | `Guid?` | Last updater user ID |
| `UpdateDate` | `DateTime?` | Last update timestamp |
| `UpdatedBy` | `User?` | Navigation to updater |

---

## Task Management Models

### TaskItem

The primary aggregate root for the task management system.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `Title` | `string` | Task title |
| `Description` | `string` | Detailed description |
| `TaskTypeId` | `Guid?` | Task type reference |
| `TaskSourceId` | `Guid?` | Task source reference |
| `PriorityId` | `Guid` | Priority level reference |
| `StatusId` | `Guid?` | Current status reference |
| `StartDate` | `DateTime?` | Planned start date |
| `EndDate` | `DateTime?` | Planned end/due date |
| `ReminderDate` | `DateTime?` | Reminder notification date |
| `Progress` | `int` | Progress percentage (0-100) |
| `PlannedWeight` | `Weight` | Planned effort (value object) |
| `ActualWeight` | `Weight` | Actual effort (value object) |
| `PlannedProgress` | `Progress` | Planned progress (value object) |
| `ActualProgress` | `Progress` | Actual progress (value object) |
| `FileId` | `Guid` | Associated file ID |
| `AssignedDepartmentIds` | `List<Guid>` | Assigned departments |
| `TaskAssignees` | `List<TaskAssignee>` | Assigned users |
| `Dependencies` | `List<TaskDependencies>` | Task dependencies |
| `Parent` | `TaskItem` | Parent task (subtask hierarchy) |
| `Subtasks` | `IReadOnlyCollection<TaskItem>` | Child tasks |
| `Checklist` | `TaskChecklist` | Checklist |
| `Comments` | `TaskComment` | Comments |
| `Attachments` | `TaskAttachments` | File attachments |
| `History` | `IReadOnlyCollection<TaskHistoryEntry>` | Audit trail |
| `ChangeEndDateRequests` | `List<ChangeEndDateRequest>` | Due date change requests |
| `Escalations` | `IReadOnlyCollection<TaskEscalation>` | Escalation records |
| `DeletedById` | `Guid?` | Who soft-deleted |
| `DeleteDate` | `DateTime?` | When soft-deleted |

**EF Core Configuration**: `TaskItemConfiguration`
- Foreign keys to Source, Type, Status, Priority with Restrict delete behavior.
- CreatedBy and UpdatedBy with NoAction delete behavior.
- Comments with Cascade delete behavior.

### TaskAssignee

Links a user to a task as an assignee.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Parent task reference |
| `TaskItem` | `TaskItem` | Navigation to task |
| `UserId` | `Guid` | Assigned user reference |
| `User` | `User` | Navigation to user |
| `AllowComplete` | `bool` | Can this assignee complete the task |
| `IsRejected` | `bool` | Has this assignee rejected |
| `RejectReason` | `string` | Rejection reason |
| `AssigneeDate` | `DateTime` | Assignment date |

**EF Core Configuration**: `TaskAssigneesConfiguration`

### TaskComment

A comment on a task.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Parent task reference |
| `taskItem` | `TaskItem` | Navigation to task |
| `Content` | `string` | Comment text |
| `Type` | `CommentType` | General, Rejection, or Reopening |

**EF Core Configuration**: `TaskCommentConfiguration`

### TaskChecklist

A checklist attached to a task.

**Implements**: `IEntityIdentifier`

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Parent task reference |
| `TaskItem` | `TaskItem` | Navigation to task |
| `Title` | `string` | Checklist title |
| `items` | `List<ChecklistItem>` | Checklist items |

**EF Core Configuration**: `TaskChecklistConfiguration`

### ChecklistItem

An individual item within a checklist.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `TaskChecklistId` | `Guid` | Parent checklist reference |
| `taskChecklist` | `TaskChecklist` | Navigation to checklist |
| `Title` | `string` | Item title |
| `EndDate` | `DateTime?` | Item due date |
| `AssignedUserId` | `Guid?` | Assigned user |
| `AssignedUser` | `User?` | Navigation to user |
| `IsDone` | `bool` | Completion flag |

**EF Core Configuration**: `ChecklistItemConfiguration`

### TaskAttachments

File attachments for a task.

**Implements**: `IEntityIdentifier`

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Parent task reference |
| `TaskItem` | `TaskItem` | Navigation to task |
| `Items` | `IReadOnlyCollection<Attachment>` | Attached files |

### TaskEscalation

An escalation record for a task.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `TaskId` | `Guid` | Task reference |
| `EscalatedToId` | `Guid` | Target user |
| `Task` | `TaskItem` | Navigation to task |
| `EscalatedTo` | `User` | Navigation to user |
| `Reason` | `string` | Escalation reason |
| `Level` | `int` | Escalation level |
| `TriggerAfter` | `int` | Days/units before trigger |
| `TriggerStatus` | `int` | Status condition |
| `Status` | `EscalationStatus` | Active or Resolved |

**EF Core Configuration**: `TaskEscalationConfiguration`

### ChangeEndDateRequest

A request to change a task's due date.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Task reference |
| `RequesterId` | `Guid` | Requester user |
| `Task` | `TaskItem` | Navigation to task |
| `Requester` | `User` | Navigation to requester |
| `NewEndDate` | `DateTime` | Proposed new due date |
| `Reason` | `string` | Request reason |
| `Status` | `ChangeRequestStatus` | Pending, Approved, Rejected |
| `IsAproved` | `bool` | Approval flag |
| `RequestedAt` | `DateTime` | Request timestamp |

**EF Core Configuration**: `ChangeEndDateRequestConfiguration`

### TaskDependencies

Declares a dependency between two tasks.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | The task with the dependency |
| `DependentId` | `Guid` | The task being depended on |
| `Task` | `TaskItem` | Navigation to parent task |

### TaskHistoryEntry

Audit trail entry for task changes.

| Property | Type | Description |
|----------|------|-------------|
| `Task` | `TaskItem` | Navigation to task |
| `By` | `User` | User who performed the action |
| `Action` | `string` | Action description |
| `Timestamp` | `DateTime` | When the action occurred |

**EF Core Configuration**: `TaskHistoryEntryConfiguration`

### PinnedTasks

Allows users to pin tasks for quick access.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `TaskId` | `Guid` | Task reference |
| `Task` | `TaskItem` | Navigation to task |

**EF Core Configuration**: `PinnedTasksConfiguration`

---

## User Management Models

### User

Represents an application user.

**Implements**: `IEntityIdentifier`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `Username` | `string` | Login username |
| `NameEnglish` | `string` | Full name in English |
| `NameArabic` | `string` | Full name in Arabic |
| `Email` | `string` | Email address |
| `DistinguishedName` | `string` | Active Directory DN |
| `GivenName` | `string` | First name |
| `Mobile` | `string` | Phone number |
| `DepartmentId` | `Guid?` | Department reference |
| `Department` | `Department` | Navigation to department |
| `PrivilegeId` | `Guid?` | Privilege/role reference |
| `Privilege` | `Privilege` | Navigation to privilege |
| `IsActive` | `bool` | Account active status |
| `IsManager` | `bool` | Manager flag |
| `IsDeleted` | `bool` | Soft delete flag |
| `TaskAssignees` | `ICollection<TaskAssignee>` | Task assignments |

**EF Core Configuration**: `UserConfiguration`

### Department

Organizational unit in a parent-child hierarchy.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `Level` | `int` | Hierarchy level (root = 1) |
| `ParentdepartmentId` | `Guid?` | Parent reference |
| `Parentdepartment` | `Department` | Navigation to parent |
| `AssignedUser` | `ICollection<User>` | Users in department |

**EF Core Configuration**: `DepartmentConfiguration`

### UserDelegation

Delegation of responsibilities from one user to another.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`, `IHasUpdateMetaData`

Namespace: `UserDelegations`

| Property | Type | Description |
|----------|------|-------------|
| `FromUserId` | `Guid` | Delegator user |
| `ToUserId` | `Guid` | Delegate user |
| `FromUser` | `User` | Navigation to delegator |
| `Touser` | `User` | Navigation to delegate |
| `FromDate` | `DateTime` | Start date |
| `ToDate` | `DateTime` | End date |
| `DelegationOption` | `DelegationOptions` | Delegation type |

**EF Core Configuration**: `UserDelegationConfiguration`

---

## Permission Models

### Privilege

A named role containing permission sets.

**Implements**: `IEntityIdentifier`, `IHasCreationMetaData`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `GeneralModulePermissions` | `List<GeneralModulePermission>` | General CRUD permissions |
| `ReportModulePermissions` | `List<ReportModulePermission>` | Report access |
| `TaskModuleUserDepartmentPermissions` | `List<TaskModuleUserDepartment>` | Dept task permissions |
| `TaskModuleExternalDepartmentPermissions` | `List<TaskModuleExternalDepartment>` | External dept permissions |

### GeneralModule

Defines a system module with available permission types.

| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | Module name (English) |
| `NameArabic` | `string` | Module name (Arabic) |
| `HasView` | `bool` | View permission available |
| `HasEdit` | `bool` | Edit permission available |
| `HasAdd` | `bool` | Add permission available |
| `HasDelete` | `bool` | Delete permission available |

### GeneralModulePermission

Specific permissions for a module within a privilege.

| Property | Type | Description |
|----------|------|-------------|
| `GeneralModuleId` | `Guid` | Module reference |
| `IsView` | `bool` | Can view |
| `IsEdit` | `bool` | Can edit |
| `IsAdd` | `bool` | Can add |
| `IsDelete` | `bool` | Can delete |
| `DelegationFromUser` | `bool` | Delegation source permission |
| `DelegationToUser` | `bool` | Delegation target permission |
| `DelegationFromUserDepartments` | `bool` | Dept delegation source |
| `DelegationToUserDepartments` | `bool` | Dept delegation target |

### ReportModule

Defines a report type.

| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | Report name (English) |
| `NameArabic` | `string` | Report name (Arabic) |
| `HintEnglish` | `string` | Description (English) |
| `HintArabic` | `string` | Description (Arabic) |
| `ReportModulePermissions` | `List<ReportModulePermission>` | Permissions |

### ReportModulePermission

Access control for a report within a privilege.

| Property | Type | Description |
|----------|------|-------------|
| `ReportModuleId` | `Guid` | Report reference |
| `IsActive` | `bool` | Access granted |

### TaskModuleUserDepartment

Task access scoped to the user's own department hierarchy.

| Property | Type | Description |
|----------|------|-------------|
| `PermissionId` | `Guid` | Privilege reference |
| `SelectedOption` | varies | Access option |
| `IsActive` | `bool` | Permission active |
| `IsManagerTasks` | `bool` | See manager tasks |
| `IsEmployeeTasks` | `bool` | See employee tasks |
| `DirectionType` | `DirectionType` | Hierarchy direction |
| `DirectionLevel` | `int` | Levels in direction |

### TaskModuleExternalDepartment

Task access for departments outside the user's own.

| Property | Type | Description |
|----------|------|-------------|
| `DepartmentId` | `Guid` | Target department |
| `IsIncludeSubDepartment` | `bool` | Include sub-departments |
| `PermissionId` | `Guid` | Privilege reference |
| `IsManagerTasks` | `bool` | See manager tasks |
| `IsEmployeeTasks` | `bool` | See employee tasks |
| `Status` | `int` | Task status filter |
| `Source` | `int` | Task source filter |
| `DirectionType` | `DirectionType` | Hierarchy direction |
| `DirectionLevel` | `int` | Levels in direction |

---

## Configuration Models

### PrioritySettings

| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `NameColor` | `string` | Text color |
| `BackgroundColor` | `string` | Background color |
| `Order` | `int` | Sort order |
| `IsDeleted` | `bool` | Soft delete flag |

### StatusSettings

| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `NameColor` | `string` | Text color |
| `BackgroundColor` | `string` | Background color |

### Source

**Implements**: `IHasCreationMetaData`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `IsActive` | `bool` | Active flag |
| `IsDeleted` | `bool` | Soft delete flag |

### Type (TaskType)

**Implements**: `IHasCreationMetaData`, `IHasUpdateMetaData`

| Property | Type | Description |
|----------|------|-------------|
| `NameEnglish` | `string` | English name |
| `NameArabic` | `string` | Arabic name |
| `IsActive` | `bool` | Active flag |
| `IsDeleted` | `bool` | Soft delete flag |

### Other Configuration Models

| Model | Purpose |
|-------|---------|
| `AddTaskSettings` | Fields shown in add task form |
| `RejectionSettings` | Rejection period configuration |
| `RecycleBinSettings` | Retention days for deleted tasks |
| `ViewTaskSettings` | View type activation |
| `FilterFieldsSettings` | Available filter fields |
| `DefaultColumnsSettings` | Default visible columns |
| `DefaultViewSettings` | Default view selection |
| `WorkingDaysSettings` | Working days configuration |
| `WeeklyReportSettings` | Report generation settings |
| `SystemIdentity` | Application branding |
| `NotificationTypeTemplate` | Notification templates |

---

## Value Objects

| Value Object | Property | Constraint | Description |
|-------------|----------|------------|-------------|
| `Weight` (record) | `int Value` | 0-100 | Effort weighting |
| `Progress` (record) | `int Percentage` | 0-100 | Completion percentage |
| `Reminder` (record) | `DateTime Date` | Valid date | Reminder date |

---

## Enums

| Enum | Values | Description |
|------|--------|-------------|
| `CommentType` | General, Rejection, Reopening | Comment purpose |
| `TaskStatus` | NotStarted, InProgress, Delayed, Closed, Returned | Task state |
| `ChangeRequestStatus` | Pending, Approved, Rejected | Change request state |
| `EscalationStatus` | Active, Resolved | Escalation state |
| `DelegationOptions` | (various) | Delegation types |
| `PeriodType` | Never, Custom, Days7, Days14, Days30, etc. | Retention periods |
| `ViewTypes` | ListView, CalendarView, TimeLineView, BoardView, GanttView | UI view modes |
| `AuthenticationMode` | ActiveDirectory, LocalDatabase | Auth provider |
| `DirectionType` | (various) | Hierarchy access direction |
| `FileType` | (various) | File categorization |

---

## Repository Interfaces

### ITaskRepository

Located in `TaskyRevamp.Domain/Interfaces/`.

| Method | Signature | Description |
|--------|-----------|-------------|
| `GetTasks` | `(PagingParameterModel?, Expression<Func<TaskItem, object>>, string, Expression<Func<TaskItem, bool>>?) -> Task<List<TaskItem>>` | Paginated task query |
| `GetTaskById` | `(Guid) -> Task<TaskItem?>` | Single task by ID |
| `UpdateTask` | `(TaskItem) -> Task<TaskItem?>` | Update and return task |

### IFileManagement

See [File Management](file-management.md) for the complete interface description.

---

## EF Core Configurations

All located in `TaskyRevamp.Infrastructure/Configurations/`:

| Configuration Class | Entity | Key Behaviors |
|-------------------|--------|---------------|
| `TaskItemConfiguration` | TaskItem | FK to Source, Type, Status, Priority (Restrict); CreatedBy, UpdatedBy (NoAction); Comments (Cascade) |
| `UserConfiguration` | User | FK to Department (NoAction); One-to-many AssignedUser |
| `DepartmentConfiguration` | Department | Self-referencing parent-child |
| `TaskCommentConfiguration` | TaskComment | FK to TaskItem |
| `TaskAssigneesConfiguration` | TaskAssignee | Composite FK to Task + User |
| `TaskChecklistConfiguration` | TaskChecklist | FK to TaskItem |
| `ChecklistItemConfiguration` | ChecklistItem | FK to TaskChecklist + User |
| `UserDelegationConfiguration` | UserDelegation | FK to FromUser + ToUser |
| `TaskEscalationConfiguration` | TaskEscalation | FK to Task + User |
| `ChangeEndDateRequestConfiguration` | ChangeEndDateRequest | FK to Task + User |
| `PinnedTasksConfiguration` | PinnedTasks | FK to Task |
| `TaskHistoryEntryConfiguration` | TaskHistoryEntry | FK to Task + User |
