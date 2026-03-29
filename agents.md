# Agents

This document describes the AI agent roles and integration points within the TaskyRevamp project. It serves as a reference for AI-assisted development, issue triage, and automated workflows.

## Purpose

AI agents interacting with this repository should use this file to understand:

- The roles they can fulfill.
- The conventions they must follow.
- How to navigate the codebase effectively.

## Agent Roles

### Code Contributor Agent

**Responsibility**: Implement features, fix bugs, and refactor code based on issue descriptions.

**Guidelines**:

- Follow the patterns in [architecture.md](architecture.md) and [CONTRIBUTING.md](CONTRIBUTING.md).
- Use the MediatR CQRS pattern for new commands and queries in `TaskyRevamp.Services`.
- Add FluentValidation validators alongside new handlers.
- Place DTOs in `TaskyRevamp.Dto` under the appropriate subfolder.
- Place domain models in `TaskyRevamp.Domain/Models`.
- Place repository implementations in `TaskyRevamp.Infrastructure/Repositories`.
- Validate changes by running `dotnet build TaskyRevamp.sln`.

### Issue Triage Agent

**Responsibility**: Categorize, label, and prioritize incoming issues.

**Guidelines**:

- Apply the `bug` label for defect reports and `enhancement` for feature requests.
- Check for duplicate issues before processing.
- Reference related features from [features.md](features.md) when applicable.
- Ensure issue descriptions include enough context for a Code Contributor Agent to act on.

### Review Agent

**Responsibility**: Review pull requests for code quality, adherence to conventions, and correctness.

**Guidelines**:

- Verify that changes follow the project structure described in [architecture.md](architecture.md).
- Check that new handlers follow the existing MediatR pattern.
- Confirm the PR builds successfully.
- Ensure no sensitive data or credentials are included.

### Documentation Agent

**Responsibility**: Keep documentation accurate and up to date.

**Guidelines**:

- Update [features.md](features.md) when new features are added or existing ones change.
- Update [architecture.md](architecture.md) when structural changes are made.
- Ensure all new public-facing components are documented.

## Key Files for Agents

| File | Purpose |
|------|---------|
| [README.md](README.md) | Project overview and setup |
| [architecture.md](architecture.md) | System design and component layout |
| [features.md](features.md) | Feature inventory and status |
| [CONTRIBUTING.md](CONTRIBUTING.md) | Contribution workflow and code style |
| [SECURITY.md](SECURITY.md) | Security policies |

## Codebase Navigation

| Area | Location | Description |
|------|----------|-------------|
| API Endpoints | `TaskyRevamp.WebAPI/Controllers/` | REST API controllers |
| Business Logic | `TaskyRevamp.Services/` | MediatR command and query handlers |
| Domain Models | `TaskyRevamp.Domain/Models/` | Entity definitions |
| Data Transfer | `TaskyRevamp.Dto/` | Request and response DTOs |
| Database | `TaskyRevamp.Infrastructure/` | EF Core context, repositories, migrations |
| UI Pages | `TaskyRevamp/TaskyRevamp.Client/Pages/` | Blazor page components |
| UI Components | `TaskyRevamp/TaskyRevamp/Components/` | Shared Razor components |
| Real-time | `TaskyRevamp.Infrastructure/Hubs/` | SignalR hubs |
| Localization | `TaskyRevamp.Localization/Resources/` | Resource files for translations |
