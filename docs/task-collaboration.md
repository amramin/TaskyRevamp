# Task Collaboration

This document provides a comprehensive description of all task collaboration features in TaskyRevamp, including assignees, comments, checklists, attachments, escalation, and change end date requests.

## Task Assignees

### Overview

Tasks can be assigned to one or more users. Each assignment tracks the user, the assignment date, and permissions such as whether the assignee is allowed to complete the task. Assignees can reject their assignment under configurable conditions.

### Domain Model: TaskAssignee

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Reference to the parent task |
| `TaskItem` | `TaskItem` | Navigation to parent task |
| `UserId` | `Guid` | Assigned user ID |
| `User` | `User` | Navigation to assigned user |
| `AllowComplete` | `bool` | Whether this assignee can mark the task as complete |
| `IsRejected` | `bool` | Whether this assignee has rejected the task |
| `RejectReason` | `string` | Reason for rejection |
| `AssigneeDate` | `DateTime` | When the assignment was made |

Implements `IHasCreationMetaData` (CreatedById, CreateDate).

### DTO: TaskAssigneesDto

Located in `TaskyRevamp.Dto/TaskAssignees/`.

| Property | Type | Description |
|----------|------|-------------|
| `taskId` | `Guid` | Task ID |
| `UserId` | `Guid` | User ID |
| `AllowComplete` | `bool` | Complete permission |
| `IsRejected` | `bool` | Rejection flag |
| `RejectReason` | `string` | Rejection reason |

### MediatR Commands and Queries

| Operation | Type | Input | Output | Description |
|-----------|------|-------|--------|-------------|
| `CreateTaskAssigneesCommand` | Command | `TaskAssigneesDto` | `Guid` | Creates a new task assignee record |
| `UpdateTaskAssigneesCommand` | Command | `TaskAssigneesDto` | `bool` | Updates assignee information |
| `DeleteTaskAssigneesCommand` | Command | `Guid` | `bool` | Removes an assignee from a task |
| `GetTaskAssigneesQuery` | Query | `Guid taskId` | `List<TaskAssigneesDto>` | Gets all assignees for a task |
| `GetTaskAssigneessQuery` | Query | (none) | `List<TaskAssigneeDataDto>` | Alternative retrieval |

### API Endpoints

Base path: `/api/taskassignees`

- CRUD operations for task assignee management.

### Client Consumer: TaskConsumer

Assignee operations are integrated into `TaskConsumer.cs`:
- `AddTask(dto, files, extensions)` — Creates assignees from `AssignedIds` list during task creation.
- `UpdateTask(dto, files)` — Updates assignee list (adds new, removes missing).

---

## Task Comments

### Overview

Users can add comments to tasks. Comments support three types: General comments, Rejection reasons, and Reopening reasons. Comments are timestamped and attributed to the authoring user.

### Domain Model: TaskComment

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Reference to the parent task |
| `taskItem` | `TaskItem` | Navigation to parent task |
| `Content` | `string` | Comment text |
| `Type` | `CommentType` | Comment type enum |

Implements `IHasCreationMetaData` and `IHasUpdateMetaData`.

### CommentType Enum

```
General     — Standard user comment
Rejection   — Comment created when a task is rejected
Reopening   — Comment created when a completed task is reopened
```

### DTO: TaskCommentDto

Located in `TaskyRevamp.Dto/TaskComment/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Task ID |
| `Content` | `string` | Comment content |
| `CreatedById` | `Guid` | Author user ID |
| `CreateDate` | `DateTime` | Creation timestamp |
| `Type` | `CommentType` | Comment type |

### MediatR Commands and Queries

| Operation | Type | Input | Output | Description |
|-----------|------|-------|--------|-------------|
| `CreateTaskCommentCommand` | Command | `TaskCommentDto` | `bool` | Creates a new comment on a task |
| `UpdateTaskCommentCommand` | Command | `TaskCommentDto` | `bool` | Updates an existing comment |
| `DeleteTaskCommentCommand` | Command | `Guid` | `bool` | Deletes a comment |
| `GetTaskCommentsQuery` | Query | `Guid taskId` | `List<TaskCommentDto>` | Gets all comments for a task |
| `GetTaskCommentQuery` | Query | `Guid commentId` | `TaskCommentDto` | Gets a single comment |

### API Endpoints

Base path: `/api/taskcomment`

### Client Consumer: TaskCommentConsumer

- `GetTaskCommentsById(taskId)` — Gets comments with user names.
- `AddTaskComment(dto)` — Creates a comment.
- `UpdateTaskComment(dto)` — Edits a comment.
- `DeleteTaskComment(id)` — Removes a comment.

---

## Task Checklists

### Overview

Each task can have one or more checklists, and each checklist contains multiple checklist items. Items can be assigned to individual users, have due dates, and be marked as done.

### Domain Model: TaskChecklist

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Reference to the parent task |
| `TaskItem` | `TaskItem` | Navigation to parent task |
| `Title` | `string` | Checklist title |
| `items` | `List<ChecklistItem>` | Checklist items |

### Domain Model: ChecklistItem

| Property | Type | Description |
|----------|------|-------------|
| `TaskChecklistId` | `Guid` | Reference to parent checklist |
| `taskChecklist` | `TaskChecklist` | Navigation to parent checklist |
| `Title` | `string` | Item title |
| `EndDate` | `DateTime?` | Item due date |
| `AssignedUserId` | `Guid?` | User assigned to this item |
| `AssignedUser` | `User?` | Navigation to assigned user |
| `IsDone` | `bool` | Completion status |

Implements `IHasCreationMetaData`.

### DTOs

**TaskChecklistDto** (`TaskyRevamp.Dto/TaskChecklist/`):

| Property | Type | Description |
|----------|------|-------------|
| `TaskId` | `Guid` | Task ID |
| `Title` | `string` | Checklist title |
| `Items` | `List<ChecklistItemDto>` | Checklist items |

**ChecklistItemDto** (`TaskyRevamp.Dto/TaskChecklist/`):

| Property | Type | Description |
|----------|------|-------------|
| `TaskChecklistId` | `Guid` | Parent checklist ID |
| `Title` | `string` | Item title |
| `EndDate` | `DateTime?` | Due date |
| `AssignedUserId` | `Guid?` | Assigned user |
| `IsDone` | `bool` | Done flag |

### MediatR Commands and Queries

**Checklists:**

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateTaskChecklistCommand` | Command | `TaskChecklistDto` | `Guid` |
| `UpdateTaskChecklistCommand` | Command | `TaskChecklistDto` | `bool` |
| `DeleteTaskChecklistCommand` | Command | `Guid` | `bool` |
| `GetTaskChecklistsQuery` | Query | `Guid taskId` | `List<TaskChecklistDto>` |
| `GetTaskChecklistQuery` | Query | `Guid checklistId` | `TaskChecklistDto` |

**Checklist Items:**

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateChecklistItemCommand` | Command | `ChecklistItemDto` | `Guid` |
| `UpdateChecklistItemCommand` | Command | `ChecklistItemDto` | `bool` |
| `DeleteChecklistItemCommand` | Command | `Guid` | `bool` |
| `GetChecklistItemsQuery` | Query | `Guid checklistId` | `List<ChecklistItemDto>` |

### API Endpoints

- `/api/taskchecklist` — Checklist CRUD
- `/api/checklistitem` — Checklist item CRUD

### Client Consumers

- `TaskChecklistConsumer` — CRUD for checklists.
- `ChecklistItemConsumer` — CRUD for checklist items.

---

## Task Attachments

### Overview

Tasks can have file attachments. Files are uploaded as multipart content, stored via the `IFileManagement` service, and tracked as `TaskAttachments` records. Each attachment links to a file entity with metadata.

### Domain Model: TaskAttachments

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Reference to parent task |
| `TaskItem` | `TaskItem` | Navigation to parent task |
| `Items` | `IReadOnlyCollection<Attachment>` | Attached file items |

### MediatR Commands and Queries

| Operation | Type | Input | Output | Description |
|-----------|------|-------|--------|-------------|
| `AddTaskAttachmentCommand` | Command | File data, task ID | `bool` | Uploads and attaches a file to a task |
| `DeleteAttachmentCommand` | Command | `Guid attachmentId` | `bool` | Removes an attachment |
| `GetTaskAttachmentsQuery` | Query | `Guid taskId` | `List<TaskAttachmentDto>` | Gets all attachments for a task |
| `GetFileInfoQuery` | Query | `Guid fileId` | `FileDto` | Gets file metadata |

### API Endpoints

Base path: `/api/taskattachment`

### Client Consumer: TaskAttachmentConsumer

- `AddTaskAttachment(multipartContent, taskId)` — Uploads file(s) to a task.
- `GetTaskAttachments(taskId)` — Gets attachment list with user names.
- `GetAttachmentInfo(fileId)` — Downloads file as byte array.
- `DeleteAttachment(attachmentId, fileId)` — Removes attachment and file.

### Supported File Types

Attachments: `.pdf`, `.docx`, `.doc`, `.ppt`, `.pptx`, `.jpeg`, `.jpg`, `.png`, `.txt`, `.csv`, `.json`, `.xml`

Task uploads: `.pdf`, `.doc`, `.docx`, `.xls`, `.xlsx`, `.ppt`, `.pptx`, `.txt`, `.csv`, `.jpg`, `.jpeg`, `.png`

Maximum file size: 26 MB (configurable).

---

## Task Escalation

### Overview

Tasks can be escalated to a specific user with a reason. Escalation tracks the level, trigger conditions, and resolution status. This feature is useful for alerting managers or other stakeholders when a task needs attention.

### Domain Model: TaskEscalation

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskId` | `Guid` | Reference to the task |
| `EscalatedToId` | `Guid` | User receiving the escalation |
| `Task` | `TaskItem` | Navigation to task |
| `EscalatedTo` | `User` | Navigation to target user |
| `Reason` | `string` | Escalation reason |
| `Level` | `int` | Escalation level |
| `TriggerAfter` | `int` | Days/units after which escalation triggers |
| `TriggerStatus` | `int` | Status that triggers escalation |
| `Status` | `EscalationStatus` | Active or Resolved |

Implements `IHasCreationMetaData`.

### EscalationStatus Enum

```
Active   — Escalation is active and unresolved
Resolved — Escalation has been addressed
```

### DTO: TaskEscalationDto

| Property | Type | Description |
|----------|------|-------------|
| `TaskId` | `Guid` | Task ID |
| `EscalatedToId` | `Guid` | Target user ID |
| `Reason` | `string` | Escalation reason |
| `Level` | `int` | Escalation level |
| `TriggerAfter` | `int` | Trigger delay |
| `TriggerStatus` | `int` | Trigger condition |

### MediatR Commands and Queries

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateTaskEscalationCommand` | Command | `TaskEscalationDto` | `Guid` |
| `UpdateTaskEscalationCommand` | Command | `TaskEscalationDto` | `bool` |
| `DeleteTaskEscalationCommand` | Command | `Guid` | `bool` |
| `GetTaskEscalationsQuery` | Query | `Guid? taskId` | `List<TaskEscalationDto>` |
| `GetTaskEscalationQuery` | Query | `Guid escalationId` | `TaskEscalationDto` |

### API Endpoints

Base path: `/api/taskescalation`

---

## Change End Date Requests

### Overview

When an assignee needs more time, they can request a change to the task's due date. The task creator can approve or reject the request. On approval, the task's end date is automatically updated.

### Domain Model: ChangeEndDateRequest

Located in `TaskyRevamp.Domain/Models/`.

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | Reference to the task |
| `RequesterId` | `Guid` | User making the request |
| `Task` | `TaskItem` | Navigation to task |
| `Requester` | `User` | Navigation to requester |
| `NewEndDate` | `DateTime` | Proposed new due date |
| `Reason` | `string` | Reason for the request |
| `Status` | `ChangeRequestStatus` | Pending, Approved, or Rejected |
| `IsAproved` | `bool` | Approval flag |
| `RequestedAt` | `DateTime` | When the request was made |

Implements `IHasCreationMetaData`.

### ChangeRequestStatus Enum

```
Pending  — Request awaiting decision
Approved — Request approved, end date updated
Rejected — Request denied
```

### Domain Methods

- `Approve(User by)` — Only the task creator can approve. Updates the task's end date to `NewEndDate` and sets status to Approved.
- `Reject(User by)` — Sets status to Rejected.

### DTO: ChangeEndDateRequestDto

| Property | Type | Description |
|----------|------|-------------|
| `TaskId` | `Guid` | Task ID |
| `NewEndDate` | `DateTime` | Proposed date |
| `Reason` | `string` | Request reason |
| `Status` | `ChangeRequestStatus` | Current status |
| `Requester` | `Guid` | Requester user ID |

### MediatR Commands and Queries

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateChangeEndDateRequestCommand` | Command | `ChangeEndDateRequestDto` | `Guid` |
| `UpdateChangeEndDateRequestCommand` | Command | `ChangeEndDateRequestDto` | `bool` |
| `DeleteChangeEndDateRequestCommand` | Command | `Guid` | `bool` |
| `UpdateRequestStatusCommand` | Command | `Guid requestId, ChangeRequestStatus status` | `bool` |
| `GetChangeEndDateRequestsQuery` | Query | `Guid? taskId` | `List<ChangeEndDateRequestDto>` |
| `GetChangeEndDateRequestQuery` | Query | `Guid requestId` | `ChangeEndDateRequestDto` |
| `GetChangeEndDateRequestsQueryByTaskId` | Query | `Guid taskId` | `List<ChangeEndDateRequestDto>` |

### API Endpoints

Base path: `/api/changeenddaterequest`

### UI Page

`ChangeEndDateRequests.razor` in `TaskyRevamp.Client/Pages/RequestNewDate/` — Lists pending requests with approve/reject actions and task reference links.

### Client Consumer: RequestChangeDueDateConsumer

- `GetDueDateRequestsByTaskId(taskId, pagination)` — Gets requests for a specific task.
- `GetDueDateRequests(pagination)` — Gets all requests.
- `UpdateRequestStatus(requestId, status)` — Approves or rejects a request.

---

## Task Dependencies

### Overview

Tasks can declare dependencies on other tasks. A dependent task cannot be completed until all its dependencies are completed. This prevents workflows from proceeding out of order.

### Domain Model: TaskDependencies

| Property | Type | Description |
|----------|------|-------------|
| `TaskItemId` | `Guid` | The task that has the dependency |
| `DependentId` | `Guid` | The task being depended on |
| `Task` | `TaskItem` | Navigation to parent task |

### Queries

- `GetDependentTasksQuery` — Gets tasks that this task depends on.
- `GetDependentOnTasksQuery` — Gets tasks that depend on this task.

### Client Consumer: TaskDependencyConsumer

- `GetDependOnTasks(taskId, pagination)` — Tasks this task depends on.
- `GetDependentTasks(taskId, pagination)` — Tasks that depend on this task.

---

## Task History

### Domain Model: TaskHistoryEntry

Every significant action on a task is recorded as a history entry.

| Property | Type | Description |
|----------|------|-------------|
| `Task` | `TaskItem` | Navigation to task |
| `By` | `User` | User who performed the action |
| `Action` | `string` | Description of the action |
| `Timestamp` | `DateTime` | When the action occurred |

History is automatically created by domain model methods such as `UpdateTitle`, `UpdateStatus`, `ChangeDates`, `Complete`, `Escalate`, `Reject`, etc.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Assignees | `TaskAssignee.cs`, `TaskAssigneesDto.cs`, `TaskAssigneesController.cs`, `TaskyRevamp.Services/TaskAssignees/` |
| Comments | `TaskComment.cs`, `TaskCommentDto.cs`, `TaskCommentController.cs`, `TaskyRevamp.Services/TaskComment/` |
| Checklists | `TaskChecklist.cs`, `ChecklistItem.cs`, `TaskChecklistDto.cs`, `ChecklistItemDto.cs`, `TaskChecklistController.cs`, `ChecklistItemController.cs` |
| Attachments | `TaskAttachments.cs`, `TaskAttachmentDto.cs`, `TaskAttachmentController.cs`, `TaskyRevamp.Services/TaskAttachments/` |
| Escalation | `TaskEscalation.cs`, `TaskEscalationDto.cs`, `TaskEscalationController.cs`, `TaskyRevamp.Services/TaskEscalation/` |
| Change Requests | `ChangeEndDateRequest.cs`, `ChangeEndDateRequestDto.cs`, `ChangeEndDateRequestController.cs` |
| Dependencies | `TaskDependencies.cs`, `TaskDependencyController.cs` |
| History | `TaskHistoryEntry.cs`, `TaskHistoryEntryConfiguration.cs` |
