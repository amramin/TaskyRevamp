# Contributing to TaskyRevamp

Thank you for your interest in contributing to TaskyRevamp. This guide covers conventions, workflow, and expectations for both human contributors and AI agents.

## Getting Started

1. Fork the repository and create a feature branch from `main`.
2. Follow the setup instructions in [README.md](README.md).
3. Make your changes in small, focused commits.
4. Submit a pull request using the [PR template](.github/PULL_REQUEST_TEMPLATE.md).

## Development Workflow

### Branching Strategy

- `main` — stable, production-ready code.
- Feature branches — use the format `feature/<short-description>`.
- Bug fix branches — use the format `fix/<short-description>`.

### Commit Messages

Write clear, concise commit messages:

```
<type>: <short summary>

<optional body with additional context>
```

Types: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`.

### Code Style

- Follow standard C# coding conventions.
- Use nullable reference types (`<Nullable>enable</Nullable>`).
- Keep methods focused and short.
- Use MediatR commands and queries for new business logic in `TaskyRevamp.Services`.
- Place DTOs in the appropriate subfolder within `TaskyRevamp.Dto`.
- Place domain models and interfaces in `TaskyRevamp.Domain`.

### Pull Requests

- Reference the related issue number in the PR description.
- Ensure the solution builds without errors (`dotnet build TaskyRevamp.sln`).
- Provide a clear description of what changed and why.
- Keep PRs small and focused on a single concern.

## Guidelines for AI Agents

AI agents contributing to this repository should follow these conventions:

- **Read context first**: Review [architecture.md](architecture.md), [features.md](features.md), and [agents.md](agents.md) before making changes.
- **Follow existing patterns**: Use MediatR handlers, FluentValidation validators, and the existing repository pattern when adding features.
- **Respect project structure**: Place files in the correct project and folder per the architecture.
- **Prefer minimal changes**: Make the smallest change that correctly addresses the issue.
- **Validate changes**: Run `dotnet build TaskyRevamp.sln` to confirm no build errors.

## Reporting Issues

- Use the [bug report template](.github/ISSUE_TEMPLATE/bug_report.md) for bugs.
- Use the [feature request template](.github/ISSUE_TEMPLATE/feature_request.md) for enhancements.
- Provide as much detail as possible to help both human and AI contributors understand the issue.

## Code of Conduct

All contributors are expected to follow the [Code of Conduct](CODE_OF_CONDUCT.md).
