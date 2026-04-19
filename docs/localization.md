# Localization

This document provides a comprehensive description of the localization feature in TaskyRevamp, covering multi-language support, resource files, culture switching, and how localization is applied across all layers of the application.

## Overview

TaskyRevamp supports two languages:

| Language | Culture Code | Direction |
|----------|-------------|-----------|
| English | `en-US` | Left-to-Right (LTR) |
| Arabic | `ar-EG` | Right-to-Left (RTL) |

Localization is applied throughout the entire application stack: UI labels, validation messages, entity display names, error messages, and API responses.

## Architecture

### Resource Files

Located in `TaskyRevamp.Localization/Resources/`:

| File | Purpose |
|------|---------|
| `SharedResources.resx` | Default (English) resource strings |
| `SharedResources.ar.resx` | Arabic translations |
| `SharedResources.Designer.cs` | Auto-generated strongly-typed accessor class |

### Strongly-Typed Access

The `SharedResources.Designer.cs` provides compile-time safe access to localized strings. Example properties:

| Key | English Value | Description |
|-----|-------------|-------------|
| `Action` | "Action" | UI label |
| `Active` | "Active" | Status label |
| `Loading` | "Loading" | Loading indicator |
| `Save` | "Save" | Button text |
| `Cancel` | "Cancel" | Button text |
| `RequiredField` | "This field is required" | Validation message |
| `InvalidEmail` | "Invalid email address" | Validation message |
| `UnexpectedError` | "An unexpected error occurred" | Error message |
| `NotFound` | "Not found" | Error message |
| `ClickToAdd` | "Click to add" | Action description |
| `ClickToDelete` | "Click to delete" | Action description |
| `ClickToEdit` | "Click to edit" | Action description |
| `InvalidOperation` | "Invalid operation" | Error message |

## Bilingual Entity Properties

Many domain models and DTOs have dual-language properties:

| Model | English Property | Arabic Property |
|-------|-----------------|-----------------|
| `Department` | `NameEnglish` | `NameArabic` |
| `User` | `NameEnglish` | `NameArabic` |
| `Privilege` | `NameEnglish` | `NameArabic` |
| `PrioritySettings` | `NameEnglish` | `NameArabic` |
| `StatusSettings` | `NameEnglish` | `NameArabic` |
| `Source` | `NameEnglish` | `NameArabic` |
| `Type` | `NameEnglish` | `NameArabic` |
| `GeneralModule` | `NameEnglish` | `NameArabic` |
| `ReportModule` | `NameEnglish`, `HintEnglish` | `NameArabic`, `HintArabic` |

The correct property is selected at runtime based on `Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName`:
- If `"ar"` → use Arabic property.
- Otherwise → use English property.

DTOs like `DepartmentDto` expose a computed `Name` property that returns the appropriate localized name, and `UserDto` has a `DisplayedName` property computed from the current culture.

## Configuration

### Server Host (Program.cs)

```csharp
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "ar-EG" };
    options
        .SetDefaultCulture("en-US")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

// In middleware pipeline:
app.UseRequestLocalization();
```

### WebAPI (Program.cs)

Same localization configuration with English (en) and Arabic (ar) support. Uses `RequestLocalizationOptions` and the localization middleware.

### Client (Program.cs)

```csharp
builder.Services.AddLocalization();
// Supported cultures: en-US, ar-EG
```

## Culture Switching

### CultureSelector Component

Located in `TaskyRevamp.Client/Layout/CultureSelector.razor`.

- Renders as a language toggle button in the header.
- Switching between English and Arabic:
  1. Stores the selected culture in `localStorage` as `"BlazorCulture"`.
  2. Removes cached column preferences (forces table re-render with new language).
  3. Performs a full page navigation to apply the culture cookie.
  4. The `CookieRequestCultureProvider` reads the cookie and sets the culture.

### Culture Propagation to API

The `ExtractCustomHeaderAttribute` action filter (applied to all API controllers) handles culture propagation:

1. Reads the `"BlazorCulture"` header from the HTTP request.
2. Sets `Thread.CurrentThread.CurrentCulture` and `Thread.CurrentThread.CurrentUICulture` to the specified culture.
3. Falls back to `en-US` if the header is missing or invalid.

This ensures that API responses use the same language as the requesting client.

### TaskyService Culture Handling

`TaskyService.CheckForToken()` adds the `"BlazorCulture"` header to every API request:

```
Request Headers:
  Authorization: Bearer {jwt-token}
  BlazorCulture: en-US (or ar-EG)
```

## Usage in Components

### Razor Components

```razor
@inject IStringLocalizer<SharedResources> Loc

<label>@Loc["YourKey"]</label>
<button>@Loc["Save"]</button>
<span class="error">@Loc["RequiredField"]</span>
```

### FluentValidation

Validators use `IStringLocalizer` for localized error messages:

```csharp
RuleFor(x => x.Title)
    .NotEmpty()
    .WithMessage(_localizer["RequiredField"]);
```

### Exception Handling

The `LocalizationExceptionMiddleware` provides localized error messages for validation and general exceptions.

## RTL Support

When the Arabic culture (`ar-EG`) is active:
- The UI layout direction changes to Right-to-Left.
- Navigation moves to the right side.
- Text alignment flips.
- CSS and Syncfusion components automatically adjust for RTL.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Resource Files | `SharedResources.resx`, `SharedResources.ar.resx`, `SharedResources.Designer.cs` (`Localization/Resources/`) |
| Configuration | `Program.cs` (all three: WebAPI, Server, Client) |
| Culture Selector | `CultureSelector.razor` (`Client/Layout/`) |
| Header Extraction | `ExtractCustomHeaderAttribute.cs` (`WebAPI/Pipeline/`) |
| Exception Localization | `LocalizationExceptionMiddleware.cs` (`WebAPI/Middleware/`) |
| HTTP Client | `TaskyService.cs` (`Client/Services/`) — Culture header injection |
