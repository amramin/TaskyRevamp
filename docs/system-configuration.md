# System Configuration

This document provides a comprehensive description of all system configuration features in TaskyRevamp. These settings control the behavior, appearance, and rules of the task management system.

## Overview

TaskyRevamp provides extensive configuration options through a settings subsystem. Each setting area has its own domain model, DTO, MediatR commands/queries, API controller, and UI page. All settings are managed by users with appropriate privileges and are persisted in the database.

## Priority Configuration

### Purpose

Define the priority levels available when creating or editing tasks. Each priority has bilingual names, display colors, and a sort order.

### Domain Model: PrioritySettings

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | Priority name in English |
| `NameArabic` | `string` | Priority name in Arabic |
| `NameColor` | `string` | Text color for display |
| `BackgroundColor` | `string` | Background color for display |
| `Order` | `int` | Sort order (lower = higher priority) |
| `IsDeleted` | `bool` | Soft delete flag |

### Seeded Defaults

Low, Medium, High, Urgent (each with distinct colors and order).

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreatePriorityCommand` | Command | `PriorityDto` | `Guid` |
| `UpdatePriorityCommand` | Command | `PriorityDto` | `bool` |
| `DeletePriorityCommand` | Command | `Guid` | `bool` |
| `RetrivePriorityCommand` | Command | `PriorityDto` | `bool` (restore soft-deleted) |
| `RetriveDeletePriorityCommand` | Command | `PriorityDto` | `bool` (undelete hard deleted) |
| `UpdatePrioritiesOrderCommand` | Command | `List<PriorityDto>` | `bool` (reorder) |
| `GetPriorityConfigurationsQuery` | Query | Pagination | `PagedResult<PriorityDto>` |
| `GetPriorityByIdQuery` | Query | `Guid` | `PriorityDto` |
| `CheckPriorityRelatedCompletedTaskQuery` | Query | `PriorityDto` | `bool` |
| `CheckRelatedTaskitemQuery` | Query | `Guid` | `bool` |
| `CheckRelatedComplatedTaskitemPriorityQuery` | Query | `PriorityDto` | `bool` |

### Validation Before Deletion

Before deleting a priority, the system checks:
- Whether any active tasks use this priority (`CheckRelatedTaskitemQuery`).
- Whether any completed tasks use this priority (`CheckRelatedComplatedTaskitemPriorityQuery`).

### UI Page

`ProirityConfiguration.razor` — Priority CRUD with drag-to-reorder for priority levels and completion status tracking.

---

## Status Configuration

### Purpose

Define the task statuses that control task workflow states. Each status has bilingual names and display colors.

### Domain Model: StatusSettings

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | Status name in English |
| `NameArabic` | `string` | Status name in Arabic |
| `NameColor` | `string` | Text color |
| `BackgroundColor` | `string` | Background color |

### Seeded Defaults

Open, In Progress, Completed, On Hold, Rejected.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateStatusCommand` | Command | `StatusSettingsDto` | `bool` |
| `GetStatusSettingsQuery` | Query | (none) | `List<StatusSettingsDto>` |
| `GetStatusByIdQuery` | Query | `Guid` | `StatusSettingsDto` |

Note: Statuses can only be updated (colors, names), not created or deleted, to maintain system integrity.

### UI Page

`StatusSettings.razor` — Status configuration with color editing.

---

## Source Configuration

### Purpose

Define the sources from which tasks originate (e.g., Ticket, Email, Phone, Chat). Sources help categorize how work enters the system.

### Domain Model: Source

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | Source name in English |
| `NameArabic` | `string` | Source name in Arabic |
| `IsActive` | `bool` | Whether the source is active |
| `IsDeleted` | `bool` | Soft delete flag |

Implements `IHasCreationMetaData` and `IHasUpdateMetaData`.

### Seeded Defaults

Ticket, Email, Phone, Chat.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateSourceCommand` | Command | `SourceDto` | `bool` |
| `UpdateSourceCommand` | Command | `SourceDto` | `bool` |
| `DeleteSourceCommand` | Command | `Guid` | `bool` |
| `RetriveSourceCommand` | Command | `SourceDto` | `bool` (restore) |
| `RetriveDeleteSourceCommand` | Command | `SourceDto` | `bool` (undelete) |
| `GetSourceConfigurationQuery` | Query | Pagination | `PagedResult<SourceDto>` |
| `GetSourceConfigurationQueryView` | Query | Pagination + isCompleted | `PagedResult<SourceDto>` |
| `GetSourceByIdQuery` | Query | `Guid` | `SourceDto` |
| `GetSourcesWithoutPaginationQuery` | Query | (none) | `List<SourceDto>` |
| `CheckRelatedTaskitemSourceQuery` | Query | `Guid` | `bool` |
| `CheckRelatedCompletedTaskQuery` | Query | `SourceDto` | `bool` |
| `CheckRelatedComplatedTaskitemSourceQuery` | Query | `SourceDto` | `bool` |

### UI Page

`SourceConfiguration.razor` — Source CRUD with active/inactive toggle.

---

## Type Configuration

### Purpose

Define task types (e.g., Bug, Feature, Enhancement). Types help categorize the nature of work.

### Domain Model: Type (TaskType)

| Property | Type | Description |
|----------|------|-------------|
| `Id` | `Guid` | Unique identifier |
| `NameEnglish` | `string` | Type name in English |
| `NameArabic` | `string` | Type name in Arabic |
| `IsActive` | `bool` | Whether the type is active |
| `IsDeleted` | `bool` | Soft delete flag |

Implements `IHasCreationMetaData` and `IHasUpdateMetaData`.

### Seeded Defaults

Bug, Feature, Enhancement.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateTypeCommand` | Command | `TypeDto` | `bool` |
| `UpdateTypeCommand` | Command | `TypeDto` | `bool` |
| `DeleteTypeCommand` | Command | `Guid` | `bool` |
| `RetriveTypeCommand` | Command | `TypeDto` | `bool` |
| `RetriveDeleteTypeCommand` | Command | `TypeDto` | `bool` |
| `GetTypeConfigurationQuery` | Query | Pagination | `PagedResult<TypeDto>` |
| `GetTypeConfigurationViewQuery` | Query | Pagination + isCompleted | `PagedResult<TypeDto>` |
| `GetTypeByIdQuery` | Query | `Guid` | `TypeDto` |
| `CheckRelatedTaskitemTypeQuery` | Query | `Guid` | `bool` |
| `CheckRelatedComplatedTaskitemTypeQuery` | Query | `TypeDto` | `bool` |
| `CheckTypeRelatedCompletedTaskQuery` | Query | `TypeDto` | `bool` |

### UI Pages

- `TypeConfiguration.razor` — Type CRUD with pagination and search.
- `TypeForm.razor` — Reusable form for type creation/editing.

---

## View Task Settings

### Purpose

Control which task view types are available in the application (Dashboard, List, Board, Calendar, Timeline, Gantt).

### Domain Model: ViewTaskSettings

Controls whether each view type is active and visible in the navigation.

### Seeded Defaults

Dashboard, List, Board, Calendar (views can be individually enabled/disabled).

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateViewTaskSettingActivationCommand` | Command | `List<ViewTaskSettingsDto>` | `bool` |
| `GetActiveViewTaskSettingsQuery` | Query | (none) | `List<ViewTaskSettingsDto>` (enabled only) |
| `GetViewTaskSettingsQuery` | Query | (none) | `List<ViewTaskSettingsDto>` (all) |

### ViewTypes Enum

```
ListView, CalendarView, TimeLineView, BoardView, GanttView
```

### UI Page

`MainView.razor` — Enable/disable view types.

---

## Default View Setting

### Purpose

Configure which view is shown by default when a user opens the task list, and the default subtask level display.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateDefaultViewSettingCommand` | Command | `DefaultViewSettingsDto` | `bool` |
| `UpdateSubTaskDefaultViewSettingCommand` | Command | `DefaultViewSettingsDto` | `bool` |
| `GetDefaultViewSettingQuery` | Query | (none) | `DefaultViewSettingsDto` |

### UI Pages

- `DefaultViewSetting.razor` — Select default view.
- `SubTaskLevel.razor` — Configure subtask hierarchy depth.

---

## Default Columns Settings

### Purpose

Configure which columns are visible in task list tables and their display order.

### Domain Model: DefaultColumnsSettings

Each setting represents a column with a name, active state, and display order.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateDefaultColumnsSettingsCommand` | Command | `List<DefaultColumnsSettingDto>` | `bool` |
| `GetDefaultColumnsSettingsQuery` | Query | (none) | `List<DefaultColumnsSettingDto>` |

### UI Page

`DefaultColumnsConfiguration.razor` — Drag-to-reorder column sequence and toggle visibility.

---

## Filter Fields Settings

### Purpose

Configure which fields are available as filters in the task list view.

### Domain Model: FilterFieldsSettings

Each setting represents a filterable field with active state.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateFilterFieldsSettingsCommand` | Command | `List<FilterFieldsSettingDto>` | `bool` |
| `GetFilterFieldsSettingsQuery` | Query | (none) | `List<FilterFieldsSettingDto>` |

### UI Page

`FilterFieldsConfiguration.razor` — Toggle available filter fields.

---

## Add Task Settings

### Purpose

Configure which fields are shown and required when creating a new task.

### Domain Model: AddTaskSettings

Each setting represents a field in the add task form with visibility and required flags.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateAddTaskSettingsCommand` | Command | `List<AddTaskSettingDto>` | `bool` |
| `GetAddTaskSettingsQuery` | Query | (none) | `List<AddTaskSettingDto>` |

### UI Page

`AddTaskSettingsConfiguration.razor` — Configure default fields in the task creation form.

---

## Rejection Settings

### Purpose

Configure the rules for task rejection, including the period during which assignees can reject a task.

### Domain Model: RejectionSettings

Controls the rejection period (number of days after assignment during which rejection is allowed).

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateRejectionSettingCommand` | Command | `RejectionSettingsDto` | `bool` |
| `GetRejectionSettingsQuery` | Query | (none) | `RejectionSettingsDto` |

### Rejection Logic

When an assignee attempts to reject a task (`RejectTaskCommand`), the handler reads the rejection settings to validate that the rejection is within the allowed period.

### UI Page

`RejectionConfiguration.razor` — Set rejection period configuration.

---

## Recycle Bin Settings

### Purpose

Configure how long soft-deleted tasks are retained before permanent deletion.

### Domain Model: RecycleBinSettings

Controls the retention period for deleted tasks.

### PeriodType Enum

```
Never    — Keep deleted tasks forever (-1)
Custom   — Configurable number of days
Days7    — 7 days
Days14   — 14 days
Days30   — 30 days
(and other predefined periods)
```

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateRecycleBinSettingCommand` | Command | `RecycleBinSettingDto` | `bool` |
| `GetRecycleBinSettingQuery` | Query | (none) | `RecycleBinSettingDto` |

### Background Job Integration

The `RecycleBinCleanupService` Hangfire job reads the recycle bin settings to determine the retention period and automatically deletes expired soft-deleted tasks (see [Background Jobs](background-jobs.md)).

### UI Page

`RecycleBinConfiguration.razor` — Set retention period.

---

## Working Days Settings

### Purpose

Configure which days of the week are working days and define holidays.

### Domain Model: WorkingDaysSettings

Working days configuration (e.g., Monday through Friday, Saturday and Sunday off).

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateWorkingDaysCommand` | Command | `List<WorkingDaysSettingsDto>` | `bool` |
| `GetWorkingDaysSettingQuery` | Query | (none) | `List<WorkingDaysSettingsDto>` |

### UI Page

`WorkingDaysSetting.razor` — Set working days and holidays.

---

## Weekly Report Settings

### Purpose

Configure weekly report generation parameters.

### Domain Model: WeeklyReportSettings

Controls report frequency, recipients, and content.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateWeeklyReportSettingCommand` | Command | `WeeklyReportSettingsDto` | `bool` |
| `GetWeeklyReportSettingQuery` | Query | (none) | `WeeklyReportSettingsDto` |

### UI Page

`WeeklyReportSettings.razor` — Configure weekly report settings.

---

## System Identity

### Purpose

Configure the application's branding, including the name, logo, and color theme.

### Domain Model: SystemIdentity

Stores the application identity settings.

### DTO: SystemIdentityDto

Contains: application name, logo (as base64 image), primary color, navigation background color, text colors, border color, and other theme variables.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `UpdateSystemIdentityCommand` | Command | `SystemIdentityDto` | `bool` |
| `GetSystemIdentityQuery` | Query | (none) | `SystemIdentityDto` |

### Theme Application

On the client side, `TaskyService.ChangeTheme()` reads the system identity and applies CSS custom properties:

| CSS Variable | Purpose |
|-------------|---------|
| `--primary-color` | Primary application color |
| `--active-primary` | Active/hover state color |
| `--light-200` | Light background shade |
| `--dark-900` | Dark text/background shade |

### UI Page

`SystemIdentitySetting.razor` — Brand customization with logo upload and color pickers.

---

## Notification Type Templates

### Purpose

Configure templates for email notifications, system notifications, and task history messages.

### Domain Model: NotificationTypeTemplate

Stores notification template content and type mappings.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateNotificationTypeTemplateCommand` | Command | `NotificationTypeTemplateDto` | `bool` |
| `UpdateNotificationTypeTemplatesCommand` | Command | `List<NotificationTypeTemplateDto>` | `bool` |
| `GetNotificationTypeTemplatesQuery` | Query | (none) | `List<NotificationTypeTemplateDto>` |
| `GetNotificationTypeTemplateByIdQuery` | Query | `Guid` | `NotificationTypeTemplateDto` |

### UI Pages

- `NotificationTypeTemplate.razor` — Template list with CRUD.
- `Form.razor` — Rich text template editor.

---

## Database Seeding

All configuration settings have corresponding seeders in `TaskyRevamp.Infrastructure/Seeders/` that populate default values on application startup:

| Seeder | Default Data |
|--------|-------------|
| `StatusSeeder` | Open, In Progress, Completed, On Hold, Rejected |
| `PrioritySeeder` | Low, Medium, High, Urgent |
| `SourceSettingSeeder` | Ticket, Email, Phone, Chat |
| `TypeSettingSeeder` | Bug, Feature, Enhancement |
| `ViewTaskListSeeder` | Dashboard, List, Board, Calendar |
| `SystemIdentitySeeder` | Default branding (logo, colors) |
| `DefaultViewSettingSeeder` | Default enabled views |
| `SubTaskLevelSeeder` | Level 1, 2, 3 hierarchy |
| `GeneralModuleSeeder` | General permission modules |
| `ReportModuleSeeder` | Report types |
| `FilterFieldsSettingSeeder` | Default filterable fields |
| `DefaultColumnsSettingSeeder` | Default visible columns |
| `AddTaskSettingSeeder` | Default add-task form fields |
| `WorkingDaysSeeder` | Mon-Fri working days |
| `NotificationTypeTemplateSeeder` | Email/system notification templates |

The main `DbSeeder.Seed(EfDbContext context)` orchestrator calls all individual seeders on application startup.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Priority | `PrioritySettings.cs`, `PriorityDto.cs`, `PrioritySettingController.cs`, `PrioritySeeder.cs` |
| Status | `StatusSettings.cs`, `StatusSettingsDto.cs`, `StatusSettingController.cs`, `StatusSeeder.cs` |
| Source | `Source.cs`, `SourceDto.cs`, `SourceSettingController.cs`, `SourceSettingSeeder.cs` |
| Type | `Type.cs`, `TypeDto.cs`, `TypeSettingController.cs`, `TypeSettingSeeder.cs` |
| Views | `ViewTaskSettings.cs`, `ViewTaskSettingsDto.cs`, `ViewTaskSettingController.cs`, `ViewTaskListSeeder.cs` |
| Columns | `DefaultColumnsSettings.cs`, `DefaultColumnsSettingDto.cs`, `DefaultColumnsSettingController.cs` |
| Filters | `FilterFieldsSettings.cs`, `FilterFieldsSettingDto.cs`, `FilterFieldsSettingController.cs` |
| Add Task | `AddTaskSettings.cs`, `AddTaskSettingDto.cs`, `AddTaskSettingController.cs` |
| Rejection | `RejectionSettings.cs`, `RejectionSettingsDto.cs`, `RejectionSettingController.cs` |
| Recycle Bin | `RecycleBinSettings.cs`, `RecycleBinSettingDto.cs`, `RecycleBinSettingController.cs` |
| Working Days | `WorkingDaysSettings.cs`, `WorkingDaysSettingsDto.cs`, `WorkingDaySettingsController.cs` |
| Weekly Report | `WeeklyReportSettings.cs`, `WeeklyReportSettingsDto.cs`, `WeeklyReportSettingController.cs` |
| Identity | `SystemIdentity.cs`, `SystemIdentityDto.cs`, `SystemIdentityController.cs` |
| Default View | `DefaultViewSettings.cs`, `DefaultViewSettingsDto.cs`, `DefaultViewSettingsController.cs` |
| Notifications | `NotificationTypeTemplate.cs`, `NotificationTypeTemplateDto.cs`, `NotificationTypeTemplateController.cs` |
| Seeders | `DbSeeder.cs`, all `*Seeder.cs` files in `Infrastructure/Seeders/` |
