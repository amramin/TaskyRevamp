# Notifications and Email

This document provides a comprehensive description of the notification and email features in TaskyRevamp, covering real-time notifications via SignalR, system notifications, email sending, and notification template management.

## Overview

TaskyRevamp has three notification channels:

1. **Real-time in-app notifications** — Delivered instantly via SignalR WebSocket connections.
2. **System notifications** — Persistent notifications stored in the database and displayed in the UI notification panel.
3. **Email notifications** — Sent via SMTP using MailKit/MimeKit.

All notification content is driven by configurable templates (see [System Configuration](system-configuration.md) — Notification Type Templates).

## SignalR Real-time Communication

### NotificationHub

Located in `TaskyRevamp.Infrastructure/Hubs/`.

A minimal SignalR hub for broadcasting real-time notifications to connected clients.

- Endpoint: `/notification-hub` (configured in `Program.cs`).
- Methods: `OnConnectedAsync()` — Base override for connection handling.
- Purpose: Pushes live task updates and notification alerts to the Blazor UI.

### ChatHub

Located in `TaskyRevamp.Infrastructure/Hubs/`.

A SignalR hub for direct user-to-user messaging.

| Method | Signature | Description |
|--------|-----------|-------------|
| `SendMessage` | `(Guid receiverId, DiscussionMessageDto message) -> Task` | Sends a chat message to a specific user via SignalR group messaging |

- Clients receive messages via the `"ReceiveMessage"` event.
- Uses SignalR group-based routing to target individual users.

### Client-side Integration

The Blazor UI subscribes to SignalR events in `MainLayout.razor`:
- Notifications appear in the header notification bell.
- Real-time updates refresh task lists and detail views when changes occur.

### Configuration

In `Program.cs` (Server host):

```csharp
builder.Services.AddSignalR();
// ...
app.MapHub<NotificationHub>("/notification-hub");
```

Response compression is enabled for HTTPS to optimize WebSocket traffic.

## System Notifications

### MediatR Command

| Command | Input | Output | Description |
|---------|-------|--------|-------------|
| `CreateSystemNotificationCommand` | `SystemNotificationDto` | `bool` | Creates a persistent system notification |

### DTO: NotificationDto

Located in `TaskyRevamp.Dto/`.

Contains notification data including recipient, message content, notification type, read status, and timestamp.

### Notification Service

The notification service (registered in `Program.cs`) handles:
- Creating notification records in the database.
- Broadcasting via SignalR to connected clients.
- Managing notification read status.

## Email Notifications

### Overview

TaskyRevamp sends email notifications using MailKit and MimeKit. Email configuration (SMTP server, port, credentials) is loaded from `MailSettings` in `appsettings.json`.

### DTO: EmailDto

Located in `TaskyRevamp.Dto/Email/`.

Contains: recipient address, subject, body (HTML), CC, BCC, and attachment references.

### API Endpoint

Base path: `/api/sendemail`

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/SendEmail` | Sends an email asynchronously |

### Client Consumer: SendEmailConsumer

- `SendEmail(dto)` — Sends an email through the API.

### Configuration

In `Program.cs` (WebAPI):

```csharp
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
```

`MailSettings` includes: SMTP host, port, username, password, sender name, sender email, and SSL settings.

## Notification Type Templates

### Overview

Notification templates define the content and format for different notification events. Templates support both email and system notification formats and can be customized through the UI.

### Domain Model: NotificationTypeTemplate

Located in `TaskyRevamp.Domain/Models/`.

Stores template content, notification type mapping, and active status.

### DTO: NotificationTypeTemplateDto

Contains: template ID, type identifier, subject template, body template (with placeholders), active flag, and metadata.

### MediatR Operations

| Operation | Type | Input | Output |
|-----------|------|-------|--------|
| `CreateNotificationTypeTemplateCommand` | Command | `NotificationTypeTemplateDto` | `bool` |
| `UpdateNotificationTypeTemplatesCommand` | Command | `List<NotificationTypeTemplateDto>` | `bool` |
| `GetNotificationTypeTemplatesQuery` | Query | (none) | `List<NotificationTypeTemplateDto>` |
| `GetNotificationTypeTemplateByIdQuery` | Query | `Guid` | `NotificationTypeTemplateDto` |
| `GetNotificationTemplateQuery` | Query | `string typeName` | Template by type |

### API Endpoints

Base path: `/api/notificationtypetemplate`

### Client Consumer: NotificationTypeTemplateConsumer

- `GetNotificationTypeTemplates()` — List all templates.
- `GetNotificationTypeTemplateById(id)` — Get single template.
- `AddNotificationTypeTemplate(dto)` — Create template.
- `UpdateNotificationTypeTemplate(dto)` — Update single template.
- `UpdateNotificationTypeTemplates(list)` — Batch update templates.
- `DeleteNotificationTypeTemplate(id)` — Delete template.

### Seeded Defaults

`NotificationTypeTemplateSeeder` populates default templates for:
- Task assignment notifications.
- Task completion notifications.
- Task rejection notifications.
- Due date change request notifications.
- Escalation notifications.

### UI Pages

| Page | Location | Description |
|------|----------|-------------|
| `NotificationTypeTemplate.razor` | `TaskyRevamp.Client/Pages/NotificationTypeTemplate/` | Template list with CRUD operations |
| `Form.razor` | `TaskyRevamp.Client/Pages/NotificationTypeTemplate/` | Rich text editor for template content |

## UI Components

### Notification Bell (MainLayout)

The notification bell in the header (`MainLayout.razor`) shows:
- Count of unread notifications.
- Dropdown list of recent notifications.
- Click to navigate to notification details.

### Toast Notifications

`ToastService` and `Toast.razor` provide temporary on-screen messages:
- Types: success (green), error (red), info (blue).
- Auto-hide after 5 seconds.
- Manual close button.
- Triggered by `ToastService.ShowToastAsync(message, type)`.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| SignalR Hubs | `NotificationHub.cs`, `ChatHub.cs` (`Infrastructure/Hubs/`) |
| Notification Logic | `TaskyRevamp.Services/Notification/`, `CreateSystemNotificationCommand.cs` |
| Email Logic | `TaskyRevamp.Services/Email/`, `SendEmailController.cs`, `SendEmailConsumer.cs` |
| Templates | `NotificationTypeTemplate.cs` (`Domain/Models/`), `NotificationTypeTemplateController.cs` |
| Client UI | `Toast.razor` (`Client/Shared/`), `MainLayout.razor` (`Client/Layout/`) |
| Configuration | `MailSettings` in `appsettings.json`, `Program.cs` |
| Seeder | `NotificationTypeTemplateSeeder.cs` (`Infrastructure/Seeders/`) |
