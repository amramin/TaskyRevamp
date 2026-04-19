# Architecture

This document describes the technical architecture of TaskyRevamp, including its project structure, key components, integrations, and major workflows.

## Architecture Overview

TaskyRevamp follows **Clean Architecture** principles with the **CQRS (Command Query Responsibility Segregation)** pattern implemented via MediatR. The solution separates concerns across multiple projects to enforce boundaries between layers.

```
┌─────────────────────────────────┐
│        Blazor Server UI         │  Presentation
│   (TaskyRevamp / .Client)       │
└──────────────┬──────────────────┘
               │ HTTP
┌──────────────▼──────────────────┐
│       ASP.NET Core Web API      │  API Layer
│      (TaskyRevamp.WebAPI)       │
└──────────────┬──────────────────┘
               │ MediatR
┌──────────────▼──────────────────┐
│        Service Layer            │  Business Logic
│     (TaskyRevamp.Services)      │
└──────────────┬──────────────────┘
               │
┌──────────────▼──────────────────┐
│        Domain Layer             │  Models & Interfaces
│      (TaskyRevamp.Domain)       │
└──────────────┬──────────────────┘
               │
┌──────────────▼──────────────────┐
│     Infrastructure Layer        │  Data Access & External Services
│  (TaskyRevamp.Infrastructure)   │
└─────────────────────────────────┘
```

## Projects

### TaskyRevamp (Blazor Server UI)

- **Host project** (`TaskyRevamp/TaskyRevamp`): Server-side Blazor host, Razor components, and static assets.
- **Client project** (`TaskyRevamp/TaskyRevamp.Client`): Pages, layout, shared components, and HTTP client services that consume the Web API.

### TaskyRevamp.WebAPI

ASP.NET Core Web API providing RESTful endpoints. Responsibilities:

- Controller-based routing.
- JWT authentication and authorization middleware.
- Request validation pipeline.
- Swagger/OpenAPI documentation.
- Exception handling middleware.
- File logging.

### TaskyRevamp.Services

Business logic layer using MediatR for CQRS. Each feature area has its own folder containing:

- **Commands** — operations that modify state.
- **Queries** — operations that read state.
- **Handlers** — MediatR handlers that process commands and queries.
- **Validators** — FluentValidation validators for request validation.

Feature areas include: Tasks, TaskAssignees, TaskAttachments, TaskChecklists, TaskComment, TaskEscalation, Account, Departments, Notification, Email, Permission, SystemConfiguration, UserDelegation, and BackgroundJobs.

### TaskyRevamp.Domain

Domain models, interfaces, and repository abstractions. This layer has no dependencies on other projects.

### TaskyRevamp.Dto

Data Transfer Objects organized by feature area. Used for API request/response contracts and inter-layer communication.

### TaskyRevamp.Infrastructure

Data access and external service integrations:

- **EF Core DbContext** and entity configurations.
- **Repository implementations** for domain interfaces.
- **Database migrations**.
- **SignalR hubs** for real-time communication.
- **Database seeders** for initial data.

### TaskyRevamp.Localization

Multi-language support using ASP.NET Core localization with `.resx` resource files.

## Key Integrations

| Integration | Technology | Purpose |
|-------------|------------|---------|
| Database | SQL Server + EF Core 9.0 | Persistent data storage |
| Authentication | JWT Bearer + AD/LDAP | User identity and access |
| Real-time updates | SignalR | Live task and notification updates |
| Background jobs | Hangfire + SQL Server storage | Scheduled and deferred processing |
| Email | MailKit / MimeKit | Notification and communication emails |
| File handling | OpenXml | Document generation and processing |
| UI components | Syncfusion Blazor | Rich interactive UI elements |

## Major Workflows

### Task Lifecycle

1. User creates a task via the Blazor UI or API.
2. The API controller dispatches a MediatR command.
3. The command handler validates input, persists the task, and triggers notifications.
4. SignalR broadcasts the update to connected clients.
5. Assignees receive notifications (in-app and/or email).

### Authentication Flow

1. User submits credentials to the login endpoint.
2. Credentials are validated against the database or Active Directory.
3. A JWT token is issued on success.
4. Subsequent requests include the token in the `Authorization` header.
5. Middleware validates the token and sets the user context.

### Background Job Processing

1. Hangfire schedules recurring or delayed jobs (defined in `TaskyRevamp.Services/BackgroundJobs`).
2. Jobs execute tasks such as cleanup, escalation checks, and Active Directory synchronization.
3. Job state is persisted in SQL Server via Hangfire's storage provider.

## Deployment

The application is deployed via Azure Pipelines to a Windows IIS server:

- **Web API** is published to an IIS site.
- **Blazor UI** is published to a separate IIS site.
- Deployment uses `app_offline.htm` for zero-downtime updates.

See `azure-pipelines.yml` for the full CI/CD configuration.
