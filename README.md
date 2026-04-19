# TaskyRevamp

A full-stack task management application built with .NET 9.0, featuring a Blazor Server UI and ASP.NET Core Web API backend.

## Overview

TaskyRevamp is an enterprise-grade task management system designed for teams that need structured task tracking, real-time collaboration, and workflow automation. It follows Clean Architecture principles with the CQRS pattern via MediatR.

## Tech Stack

| Layer | Technology |
|-------|------------|
| **UI** | Blazor Server, Syncfusion Blazor Components |
| **API** | ASP.NET Core 9.0 Web API |
| **ORM** | Entity Framework Core 9.0 |
| **Database** | SQL Server |
| **Authentication** | JWT Bearer Tokens, Active Directory / LDAP |
| **Real-time** | SignalR |
| **Background Jobs** | Hangfire |
| **Email** | MailKit / MimeKit |
| **Validation** | FluentValidation |
| **Mediator** | MediatR (CQRS) |
| **Localization** | ASP.NET Core Localization |
| **CI/CD** | Azure Pipelines |

## Project Structure

```
TaskyRevamp.sln
├── TaskyRevamp/                    # Blazor Server UI
│   ├── TaskyRevamp/               # Server-side host
│   └── TaskyRevamp.Client/        # Client-side pages and components
├── TaskyRevamp.WebAPI/             # ASP.NET Core Web API
├── TaskyRevamp.Services/           # Business logic and MediatR handlers
├── TaskyRevamp.Domain/             # Domain models and interfaces
├── TaskyRevamp.Dto/                # Data Transfer Objects
├── TaskyRevamp.Infrastructure/     # EF Core, repositories, SignalR hubs
└── TaskyRevamp.Localization/       # Multi-language resource files
```

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) (local or remote instance)
- A code editor such as [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/amramin/TaskyRevamp.git
cd TaskyRevamp
```

### 2. Configure the Database

Update the connection string in both:

- `TaskyRevamp.WebAPI/appsettings.json`
- `TaskyRevamp/TaskyRevamp/appsettings.json`

### 3. Apply Migrations

```bash
dotnet ef database update --project TaskyRevamp.Infrastructure --startup-project TaskyRevamp.WebAPI
```

### 4. Build the Solution

```bash
dotnet build TaskyRevamp.sln
```

### 5. Run the Application

Start the Web API:

```bash
dotnet run --project TaskyRevamp.WebAPI
```

Start the Blazor Server UI:

```bash
dotnet run --project TaskyRevamp/TaskyRevamp
```

The API includes Swagger UI for interactive endpoint exploration at `/swagger`.

## Documentation

| Document | Description |
|----------|-------------|
| [Architecture](architecture.md) | System architecture, components, and workflows |
| [Features](features.md) | Feature list with status and descriptions |
| [Agents](agents.md) | AI agent roles and integration guidelines |
| [Contributing](CONTRIBUTING.md) | How to contribute to the project |
| [Code of Conduct](CODE_OF_CONDUCT.md) | Community standards and expectations |
| [Security](SECURITY.md) | Security policy and vulnerability reporting |

## License

This project is proprietary. See the repository owner for licensing details.
