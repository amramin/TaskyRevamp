# Security Policy

## Supported Versions

| Version | Supported |
|---------|-----------|
| Latest on `main` | Yes |

## Reporting a Vulnerability

If you discover a security vulnerability in TaskyRevamp, please report it responsibly.

### How to Report

1. **Do not** open a public issue for security vulnerabilities.
2. Email the project maintainers with:
   - A description of the vulnerability.
   - Steps to reproduce the issue.
   - The potential impact.
   - Any suggested fixes, if available.

### What to Expect

- An acknowledgment of your report within 72 hours.
- Regular updates on the status of the fix.
- Credit in the release notes (unless you prefer to remain anonymous).

## Security Practices

TaskyRevamp follows these security practices:

- **Authentication**: JWT Bearer tokens with configurable expiration. Active Directory / LDAP integration for enterprise environments.
- **Authorization**: Role-based and permission-based access control.
- **Input Validation**: FluentValidation on all incoming requests.
- **Data Protection**: Parameterized queries via Entity Framework Core to prevent SQL injection.
- **Dependencies**: Regular review and update of NuGet packages.
- **Transport Security**: HTTPS enforced in production deployments.

## Scope

This policy applies to the TaskyRevamp application code and its direct dependencies. Third-party services and infrastructure are outside the scope of this policy.
