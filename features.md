# Features

This document lists the features of TaskyRevamp along with their current status and descriptions. It serves as a reference for contributors, AI agents, and stakeholders.

For detailed documentation of each feature, see the linked documents in the `docs/` directory.

## Feature Inventory

### Task Management

> **Detailed documentation**: [docs/task-management.md](docs/task-management.md)

| Feature | Status | Description |
|---------|--------|-------------|
| Create Task | Implemented | Create new tasks with title, description, priority, and due date |
| Edit Task | Implemented | Update task details and properties |
| Delete Task | Implemented | Remove tasks from the system |
| Task Views | Implemented | Multiple views for browsing and filtering tasks |
| Task Search | Implemented | Search and filter tasks with dynamic field mappings |

### Task Collaboration

> **Detailed documentation**: [docs/task-collaboration.md](docs/task-collaboration.md)

| Feature | Status | Description |
|---------|--------|-------------|
| Task Assignees | Implemented | Assign one or more users to a task |
| Task Comments | Implemented | Add and view comments on tasks |
| Task Checklists | Implemented | Create checklist items within a task |
| Task Attachments | Implemented | Upload and manage file attachments on tasks |
| Task Escalation | Implemented | Escalate tasks based on rules or manual action |

### User and Access Management

> **Detailed documentation**: [docs/authentication-and-authorization.md](docs/authentication-and-authorization.md) | [docs/departments-and-users.md](docs/departments-and-users.md)

| Feature | Status | Description |
|---------|--------|-------------|
| User Authentication | Implemented | JWT-based login and session management |
| Active Directory Integration | Implemented | Sync users and authenticate via LDAP |
| Role-based Authorization | Implemented | Control access based on user roles |
| Permission Management | Implemented | Granular permission checks for actions |
| User Delegation | Implemented | Delegate tasks and responsibilities between users |

### Organization

> **Detailed documentation**: [docs/departments-and-users.md](docs/departments-and-users.md) | [docs/system-configuration.md](docs/system-configuration.md)

| Feature | Status | Description |
|---------|--------|-------------|
| Department Management | Implemented | Create and manage organizational departments |
| System Configuration | Implemented | Application-wide settings and preferences |

### Communication

> **Detailed documentation**: [docs/notifications-and-email.md](docs/notifications-and-email.md)

| Feature | Status | Description |
|---------|--------|-------------|
| In-app Notifications | Implemented | Real-time notifications via SignalR |
| Email Notifications | Implemented | Email alerts using MailKit |

### Infrastructure

> **Detailed documentation**: [docs/background-jobs.md](docs/background-jobs.md) | [docs/localization.md](docs/localization.md) | [docs/file-management.md](docs/file-management.md) | [docs/search-and-filtering.md](docs/search-and-filtering.md)

| Feature | Status | Description |
|---------|--------|-------------|
| Background Jobs | Implemented | Scheduled tasks via Hangfire (cleanup, AD sync) |
| File Logging | Implemented | Request and error logging to files |
| Localization | Implemented | Multi-language UI support via resource files |
| Swagger API Docs | Implemented | Interactive API documentation at `/swagger` |
| Real-time Updates | Implemented | Live UI updates via SignalR hubs |

## Detailed Feature Documentation

| Document | Topics Covered |
|----------|---------------|
| [Task Management](docs/task-management.md) | Task CRUD, lifecycle, status transitions, dependencies, pinned tasks, recycle bin |
| [Task Collaboration](docs/task-collaboration.md) | Assignees, comments, checklists, attachments, escalation, change end date requests, history |
| [Authentication & Authorization](docs/authentication-and-authorization.md) | JWT auth, LDAP/AD integration, privileges, permissions, delegation |
| [Departments & Users](docs/departments-and-users.md) | Department hierarchy, user management, linking, statistics |
| [System Configuration](docs/system-configuration.md) | Priority, status, source, type, views, columns, filters, rejection, recycle bin, identity, reports |
| [Notifications & Email](docs/notifications-and-email.md) | SignalR notifications, email via MailKit, notification templates |
| [Background Jobs](docs/background-jobs.md) | Hangfire setup, AD sync, recycle bin cleanup, delayed task checks |
| [Localization](docs/localization.md) | Multi-language (English/Arabic), RTL support, culture switching |
| [Search & Filtering](docs/search-and-filtering.md) | Dynamic search fields, pagination, sorting, view types |
| [File Management](docs/file-management.md) | Upload/download, validation, Excel operations, preview |
| [API Reference](docs/api-reference.md) | All REST API endpoints by controller |
| [Domain Models](docs/domain-models.md) | All entity models, properties, relationships, enums, value objects |

## Planned Enhancements

| Enhancement | Priority | Description |
|-------------|----------|-------------|
| Unit and Integration Tests | High | Add test projects for automated testing |
| Audit Logging | Medium | Track changes to tasks and system actions |
| Dashboard and Reporting | Medium | Visual summaries and task analytics |
| API Rate Limiting | Low | Throttle API requests for abuse prevention |
