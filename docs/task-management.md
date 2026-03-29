# Task Management

This document provides a comprehensive description of the Task Management feature in TaskyRevamp. It covers the full task lifecycle, data model, operations, status transitions, and all related workflows.

## Overview

Tasks are the core entity in TaskyRevamp. Each task represents a unit of work that can be created, assigned, tracked, completed, rejected, reopened, escalated, and deleted. Tasks support subtask hierarchies, dependencies between tasks, progress tracking with planned and actual weights, and a full audit trail of changes.

## Domain Model: TaskItem

`TaskItem` is the primary aggregate root located in `TaskyRevamp.Domain/Models/`.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `Title` | `string` | Task title (required, max 100 characters) |
| `Description` | `string` | Detailed task description |
| `TaskTypeId` | `Guid?` | Reference to task type (Bug, Feature, etc.) |
| `TaskSourceId` | `Guid?` | Reference to task source (Ticket, Email, etc.) |
| `PriorityId` | `Guid` | Reference to priority level |
| `StatusId` | `Guid?` | Reference to current status |
| `StartDate` | `DateTime?` | Planned start date |
| `EndDate` | `DateTime?` | Planned end/due date |
| `ReminderDate` | `DateTime?` | Reminder date for notifications |
| `Progress` | `int` | Current progress percentage (0-100) |
| `PlannedWeight` | `Weight` | Planned effort weight (value object, 0-100) |
| `ActualWeight` | `Weight` | Actual effort weight (value object, 0-100) |
| `PlannedProgress` | `Progress` | Planned progress (value object, 0-100) |
| `ActualProgress` | `Progress` | Actual progress (value object, 0-100) |
| `FileId` | `Guid` | Associated file identifier |
| `AssignedDepartmentIds` | `List<Guid>` | Departments assigned to the task |

### Relationships

| Relationship | Type | Description |
|-------------|------|-------------|
| `TaskAssignees` | `List<TaskAssignee>` | Users assigned to this task |
| `Dependencies` | `List<TaskDependencies>` | Tasks this task depends on |
| `Parent` | `TaskItem` | Parent task (for subtask hierarchy) |
| `Subtasks` | `IReadOnlyCollection<TaskItem>` | Child tasks |
| `Checklist` | `TaskChecklist` | Associated checklist |
| `Comments` | `TaskComment` | Task comments |
| `Attachments` | `TaskAttachments` | File attachments |
| `History` | `IReadOnlyCollection<TaskHistoryEntry>` | Audit trail entries |
| `ChangeEndDateRequests` | `List<ChangeEndDateRequest>` | Requests to change the due date |
| `Escalations` | `IReadOnlyCollection<TaskEscalation>` | Escalation records |

### Metadata (Audit Fields)

TaskItem implements `IHasCreationMetaData` and `IHasUpdateMetaData`:

- `CreatedById` / `CreatedBy` — User who created the task.
- `CreateDate` — When the task was created.
- `UpdatedById` / `UpdatedBy` — User who last updated the task.
- `UpdateDate` — When the task was last updated.
- `DeletedById` — User who soft-deleted the task.
- `DeleteDate` — When the task was soft-deleted.

### Domain Methods

| Method | Signature | Description |
|--------|-----------|-------------|
| `UpdateTitle` | `(string title, User by)` | Updates the title and creates a history entry |
| `UpdateStatus` | `(StatusSettings status, User by)` | Updates the task status |
| `ChangeDates` | `(DateTime? start, DateTime end, User by)` | Updates start and end dates |
| `UpdatePlannedWeight` | `(int weight, User by)` | Updates planned effort weight |
| `UpdateActualWeight` | `(int weight, User by)` | Updates actual effort weight |
| `UpdateProgress` | `(int progress, User by)` | Updates progress percentage |
| `Complete` | `(User by)` | Sets progress to 100% and marks task as completed |
| `AddSubtask` | `(TaskItem subtask, User by)` | Adds a subtask relationship |
| `RequestEndDateChange` | `(DateTime newDate, string reason, User by)` | Creates a change request for the due date |
| `Escalate` | `(User to, string reason, User by, int level, int triggerAfter, int triggerStatus)` | Escalates the task to another user |
| `Reject` | `(User by, string reason, User assigner)` | Rejects the task assignment |
| `Restore` | `(bool reassignToCreator, User by)` | Restores a soft-deleted task |
| `SoftDelete` | `(Guid deletedById)` | Marks the task as deleted (soft delete) |
| `AddHistoryEntry` | `(User by, string action)` | Records an action in the audit trail |

## Data Transfer Object: CreateTaskDto

`CreateTaskDto` is the primary DTO used for task input and output, located in `TaskyRevamp.Dto/TaskDto/`.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Task identifier |
| `Title` | `string` | Task title |
| `Description` | `string` | Task description |
| `StartDate` | `DateTime?` | Start date |
| `EndDate` | `DateTime?` | End/due date |
| `ReminderDate` | `DateTime?` | Reminder date |
| `TypeId` | `Guid?` | Task type reference |
| `SourceId` | `Guid?` | Task source reference |
| `Priority` | `Guid` | Priority reference |
| `weight` | `int?` | Planned weight |
| `ActualProcess` | `int` | Actual progress percentage |
| `AssignedDepartmentIds` | `List<Guid>` | Assigned department IDs |
| `AssignedIds` | `List<Guid>` | Assigned user IDs |
| `CreatedBy` | `Guid?` | Creator user ID |
| `CreateDate` | `DateTime?` | Creation timestamp |
| `UpdateDate` | `DateTime?` | Last update timestamp |
| `TaskStatus` | `Guid?` | Status reference |
| `ChangeEndDateRequestCount` | `int` | Number of pending change requests |
| `TaskStatusName` | `string` | Localized status name (display) |
| `TaskStatusColor` | `string` | Status text color (display) |
| `TaskStatusBackgroundColor` | `string` | Status background color (display) |
| `PriorityName` | `string` | Localized priority name (display) |
| `PriorityColor` | `string` | Priority text color (display) |
| `PriorityBackgroundColor` | `string` | Priority background color (display) |
| `SourceName` | `string` | Source name (display) |
| `TypeName` | `string` | Type name (display) |
| `AssigneduserNames` | `string` | Comma-separated assignee names (display) |
| `ActualWeight` | `int` | Actual weight |
| `Plannedweight` | `int` | Planned weight |
| `PlannedProgress` | `int` | Planned progress |
| `Dependencies` | `List<Guid>` | Dependent task IDs |
| `DependencyNames` | `string` | Dependency names (display) |
| `ViewdByNames` | `List<TaskViewsDto>` | Users who have viewed this task |
| `Checklists` | `List<TaskChecklistDto>` | Associated checklists |
| `BrowserFiles` | `List<IBrowserFile>` | Files to upload (client-side only) |

## Task Lifecycle and Status Transitions

### Statuses

Task statuses are configurable via `StatusSettings` (seeded defaults):

- **Open** — Task created, not started.
- **In Progress** — Work has begun.
- **Completed** — Task finished (100% progress).
- **On Hold** — Task paused.
- **Rejected** — Task rejected by assignee.
- **Delayed** — Past due date, not completed.

### Automatic Status Transitions

The system automatically transitions task status based on dates and progress:

| Condition | Resulting Status |
|-----------|-----------------|
| Progress = 0%, start date in future | Not Started |
| Progress = 0%, start date today or past | In Progress |
| Past end date, not completed | Delayed |
| Progress = 100% | Completed |
| Reopened from Completed | In Progress (progress reset to 50%) |

The `CheckDelayedTasksCommand` runs as a daily background job via Hangfire to find all tasks past their end date and transition them to the Delayed status.

## MediatR Commands and Queries

All task operations are implemented as MediatR commands (write) and queries (read) in `TaskyRevamp.Services/Tasks/`.

### Commands

| Command | Input | Output | Description |
|---------|-------|--------|-------------|
| `CreateTaskCommand` | `CreateTaskDto` | `string` (task ID) | Creates a new task. Validates dependencies and departments. Sets status based on dates/progress. Creates initial comments and checklists if provided. |
| `UpdateTaskCommand` | `CreateTaskDto` | `bool` | Updates task properties. Manages status transitions. Updates assignees (add/remove). Updates checklists. |
| `DeleteTaskCommand` | `Guid` | `bool` | Permanently deletes a task. |
| `SoftDeleteTaskCommand` | `Guid taskId, Guid userId` | `bool` | Marks task as deleted (soft delete). Checks for task dependencies first. |
| `CompleteTaskCommand` | `Guid taskId` | `bool` | Sets progress to 100% and status to Completed. |
| `ChangeTaskProgressCommand` | `Guid taskId, int progress` | `bool` | Updates progress percentage and adjusts status accordingly. |
| `UpdateTaskPriorityCommand` | `Guid taskId, Guid priorityId` | `bool` | Changes the task priority. |
| `UpdateTaskStatusCommand` | `Guid taskId, TaskStatus status` | `Unit` | Directly updates the task status. |
| `ReopenTaskCommand` | `TaskCommentDto` | `bool` | Reopens a completed task. Resets progress to 50%. Adds a comment with the reason. |
| `RejectTaskCommand` | `TaskCommentDto` | `bool` | Rejects a task assignment. Validates rejection period from settings. If single assignee, reassigns to creator. If multiple, removes the rejecting assignee. Adds rejection comment. |
| `RestoreTaskCommand` | `Guid taskId, int restoreOption` | `bool` | Restores a soft-deleted task. Option to reassign to the original creator. |
| `UpdateTasksDepartmentCommand` | `Guid oldDeptId, Guid newDeptId` | `bool` | Bulk updates tasks when a department assignment changes. |
| `CheckDelayedTasksCommand` | (none) | `bool` | Background job: finds all overdue tasks and updates their status to Delayed. |
| `DeleteTasksCommand` | `List<Guid>` | `bool` | Bulk permanently deletes multiple tasks. |
| `CheckOpenedTaskForUserCommand` | `Guid userId` | `bool` | Checks whether a user has any open (non-completed) tasks. |

### Queries

| Query | Input | Output | Description |
|-------|-------|--------|-------------|
| `GetTasksQuery` | Pagination params, search fields, view type, filters | `PagedResult<CreateTaskDto>` | Retrieves paginated task list with filtering, sorting, and multiple view types (Timeline, Calendar, List, Board). Supports complex search. |
| `GetTaskQuery` | `Guid taskId, Guid currentUserId` | `CreateTaskDto` | Retrieves a single task with full details. Tracks task views (records that the user viewed this task). Enriches with localized names. |
| `GetTaskByIdQuery` | `Guid taskId` | `CreateTaskDto` | Retrieves a task DTO without view tracking. |
| `GetDeletedTasksQuery` | Pagination params | `PagedResult<CreateTaskDto>` | Retrieves soft-deleted tasks for the recycle bin. |
| `GetTasksForDDLQuery` | (none) | `List<DdlDto>` | Retrieves tasks formatted for dropdown lists. |
| `GetMainAndParentTasksQuery` | (none) | `List<CreateTaskDto>` | Retrieves only parent/main tasks (not subtasks). |

## Validation

`CreateTaskValidator` (FluentValidation) in `TaskyRevamp.Services/Tasks/`:

- `Title` is required with a maximum length of 100 characters.
- Error messages are localized using `IStringLocalizer`.

## Task Creation Workflow

1. User submits a `CreateTaskCommand` with a `CreateTaskDto`.
2. `CreateTaskValidator` validates the input (title required, max 100 chars).
3. The handler checks that referenced dependencies exist and are valid.
4. Referenced departments are validated.
5. The task is created with automatic status determination:
   - If start date is in the future → Not Started.
   - If start date is today/past and progress is 0 → In Progress.
6. Initial comments are created if provided in the DTO.
7. Checklists and checklist items are created if provided.
8. Assignees are created from the `AssignedIds` list.
9. The task ID is returned as a string.

## Task Rejection Workflow

1. Assignee initiates `RejectTaskCommand` with a `TaskCommentDto` containing the reason.
2. System validates that rejection is allowed (within the configurable rejection period from `RejectionSettings`).
3. If the task has a single assignee: the task is reassigned to the original creator.
4. If the task has multiple assignees: the rejecting assignee is removed from the list.
5. A comment of type `Rejection` is created with the reason.
6. Task history is updated to record the rejection.

## Task Soft Delete and Restoration

### Soft Delete
- `SoftDeleteTaskCommand` sets `IsDeleted = true` and records `DeletedById` and `DeleteDate`.
- The system checks for task dependencies first — tasks with active dependents cannot be deleted.
- Soft-deleted tasks appear in the Recycle Bin.

### Restoration
- `RestoreTaskCommand` restores a soft-deleted task.
- `restoreOption = 2` reassigns the task to the original creator.
- The task reappears in the main task list.

### Permanent Deletion
- `DeleteTaskCommand` and `DeleteTasksCommand` permanently remove tasks from the database.
- The `RecycleBinCleanupService` background job automatically deletes tasks that have been in the recycle bin longer than the configured retention period.

## Task Dependencies

- Tasks can declare dependencies on other tasks via `TaskDependencies`.
- A task cannot be completed if its dependent tasks are not completed first.
- Dependencies are validated during task creation and status updates.
- Queries: `GetDependentTasksQuery` (tasks this one depends on) and `GetDependentOnTasksQuery` (tasks that depend on this one).

## Task View Tracking

- When a user (other than the creator) views a task via `GetTaskQuery`, the system records a `TaskViews` entry.
- The view record includes the viewer's user ID and a timestamp.
- The list of viewers is returned in the `ViewdByNames` property of `CreateTaskDto`.

## Pinned Tasks

- Users can pin tasks for quick access using `PinnedTasks`.
- Commands: `CreatePinnedTasksCommand`, `UpdatePinnedTasksCommand`, `DeletePinnedTasksCommand`.
- Queries: `GetPinnedTasksQuery` (all pinned), `GetPinnedTaskQuery` (single).

## UI Pages

| Page | Location | Description |
|------|----------|-------------|
| `MainTasks.razor` | `TaskyRevamp.Client/Pages/Tasks/` | Main task list with pagination, filtering, sorting, and navigation to detail |
| `ViewTask.razor` | `TaskyRevamp.Client/Pages/Tasks/` | Single task detail view with comments, checklists, attachments, history, and action buttons |
| `AddTask.razor` | `TaskyRevamp.Client/Pages/Tasks/` | Task creation form with validation, file upload, department/assignee selection, and dropdowns |
| `EditTask.razor` | `TaskyRevamp.Client/Pages/Tasks/` | Task editing form |
| `CompletedTasks.razor` | `TaskyRevamp.Client/Pages/` | Completed tasks list with archive/restore functionality |
| `RecycleBin.razor` | `TaskyRevamp.Client/Pages/RecycleBin/` | Soft-deleted tasks with restore and permanent delete options |

## API Endpoints

Base path: `/api/task`

| Method | Endpoint | Handler |
|--------|----------|---------|
| POST | `/CreateTask` | `CreateTaskCommand` |
| GET | `/CompleteTask/{id}` | `CompleteTaskCommand` |
| POST | `/ReopenTask` | `ReopenTaskCommand` |
| GET | `/RestoreTask/{taskId}/{restoreOption}` | `RestoreTaskCommand` |
| POST | `/RejectTask` | `RejectTaskCommand` |
| POST | `/RequestChangeDueDate` | `CreateChangeEndDateRequestCommand` |
| POST | `/UpdateTask` | `UpdateTaskCommand` |
| GET | `/UpdateTasksDepartment/{oldId}/{newId}` | `UpdateTasksDepartmentCommand` |
| GET | `/ChangeTaskProgress/{taskId}/{progress}` | `ChangeTaskProgressCommand` |
| GET | `/UpdateTaskPriority/{taskId}/{PriorityId}` | `UpdateTaskPriorityCommand` |
| GET | `/CheckOpenedTaskForUser/{userId}` | `CheckOpenedTaskForUserCommand` |
| GET | `/CheckDelayedTasks` | `CheckDelayedTasksCommand` |
| DELETE | `/SoftDeleteTask/{id}/{userId}` | `SoftDeleteTaskCommand` |
| GET | `/GetTask/{id}/{currentUserId}` | `GetTaskQuery` |
| GET | `/GetTasks` | `GetTasksQuery` (with pagination and search params) |
| GET | `/GetDeletedTasks` | `GetDeletedTasksQuery` |
| GET | `/GetTasksByTaskIds` | `GetMainAndParentTasksQuery` |

## Related Files

| File | Location | Purpose |
|------|----------|---------|
| `TaskItem.cs` | `TaskyRevamp.Domain/Models/` | Domain model |
| `CreateTaskDto.cs` | `TaskyRevamp.Dto/TaskDto/` | Primary DTO |
| `TaskController.cs` | `TaskyRevamp.WebAPI/Controllers/` | API controller |
| `CreateTaskCommand.cs` | `TaskyRevamp.Services/Tasks/` | Create handler |
| `UpdateTaskCommand.cs` | `TaskyRevamp.Services/Tasks/` | Update handler |
| `GetTasksQuery.cs` | `TaskyRevamp.Services/Tasks/` | List query |
| `GetTaskQuery.cs` | `TaskyRevamp.Services/Tasks/` | Detail query |
| `CreateTaskValidator.cs` | `TaskyRevamp.Services/Tasks/` | Validation rules |
| `ITaskRepository.cs` | `TaskyRevamp.Domain/Interfaces/` | Repository interface |
| `TaskItemConfiguration.cs` | `TaskyRevamp.Infrastructure/Configurations/` | EF Core configuration |
| `TaskConsumer.cs` | `TaskyRevamp.Client/Consumer/` | Client-side API consumer |
