# File Management

This document provides a comprehensive description of the file management feature in TaskyRevamp, covering file upload, download, validation, preview, and Excel operations.

## Overview

TaskyRevamp provides file management capabilities for task attachments, user imports, and data exports. Files are managed through the `IFileManagement` interface on the server and the `FileManagementService` on the client.

## Server-side: IFileManagement Interface

Located in `TaskyRevamp.Domain/Interfaces/`.

### Methods

| Method | Signature | Description |
|--------|-----------|-------------|
| `UploadFile` | `(List<byte>, string fileName, FileType) -> Task<Guid>` | Upload from byte list |
| `UploadFile` | `(byte[], string fileName, FileType) -> Task<Guid>` | Upload from byte array |
| `UploadFile` | `(string base64, string fileName, FileType) -> Task<Guid>` | Upload from base64 string |
| `DownloadFile` | `(Guid fileId) -> Task<string>` | Download file as string |
| `DownloadFileAsBytes` | `(Guid fileId) -> Task<byte[]>` | Download file as byte array |
| `GetFileName` | `(Guid fileId) -> Task<string>` | Get file name by ID |
| `GetFileInfo` | `(Guid fileId) -> Task<FileDto>` | Get file metadata |
| `GetAllFiles` | `() -> List<FileDto>` | List all files |
| `GetFilesInfo` | `(List<Guid>) -> Task<List<FileDto>>` | Get metadata for multiple files |
| `ResolveFileType` | `(string fileName) -> FileType` | Determine file type from extension |
| `DeleteFile` | `(Guid fileId, FileType) -> Task<Guid>` | Delete a file |

### FileType Enum

Located in `TaskyRevamp.Domain/FileType.cs`. Categorizes files by their purpose and storage location.

### FileDto

Located in `TaskyRevamp.Dto/Files/`.

Contains file metadata including: ID, file name, content type, size, and creation information.

## Client-side: FileManagementService

Located in `TaskyRevamp.Client/Services/FileManagementService.cs`.

### File Validation

**ValidateAttachment(file, maxSize)**:
- Validates file extension against allowed list.
- Validates file size against maximum.
- Returns `ValidationResult` with `NotValid` flag and localized error message.

**ValidateUploadedFile(file, maxSize)**:
- Similar validation for task file uploads.
- Uses a separate allowed extensions list.

### Supported Extensions

**Attachments:**
```
.pdf, .docx, .doc, .ppt, .pptx, .jpeg, .jpg, .png, .txt, .csv, .json, .xml
```

**Task uploads:**
```
.pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .txt, .csv, .jpg, .jpeg, .png
```

**Maximum file size:** 26 MB (configurable via `IConfiguration`).

### File Operations

| Method | Description |
|--------|-------------|
| `UploadStreamFiles(taskId, files, allowedExtensions)` | Multipart upload via `TaskAttachmentConsumer` |
| `DownloadFile(bytes, fileName)` | Browser download via JavaScript interop |
| `PreviewFile(bytes, fileName, contentType)` | Opens file preview in browser |
| `ReadFileAsync(file)` | Reads browser file into byte array |
| `CanPreview(fileName)` | Checks if file is previewable (PDF, images, text) |

### File Utilities

| Method | Description |
|--------|-------------|
| `GetFileIcon(fileName)` | Returns Bootstrap icon CSS class based on file extension |
| `GetMimeType(fileName)` | Returns the MIME content type for a file |
| `GetFileSize(bytes)` | Formats file size as human-readable string (B, KB, MB) |

## Excel Operations

The `FileManagementService` includes built-in Excel support using the **OpenXML SDK** (`DocumentFormat.OpenXml`).

### Export

**ExportToExcelAsync&lt;T&gt;(data, columns, fileName)**:
1. Creates a new `.xlsx` file using OpenXML SDK.
2. Writes column headers from the `columns` parameter.
3. Iterates through `data` collection and writes each row.
4. Triggers browser download of the generated file.

### Import

**ReadExcelRows(fileBytes)**:
1. Reads an `.xlsx` file from byte array.
2. Parses all rows into `List<string[]>`.
3. Returns data for processing (e.g., user import, department bulk creation).

### Template Generation

**DownloadUsersTemplateAsync()**:
1. Generates a localized Excel template with appropriate column headers.
2. Headers are in English or Arabic based on the current culture.
3. Used by the bulk user import feature in `LinkUser.razor`.

## File Upload Flow

### Task Attachment Upload

1. User selects file(s) in the UI (via `AddTask.razor`, `EditTask.razor`, or `ViewTask.razor`).
2. `FileManagementService.ValidateUploadedFile()` checks extension and size.
3. `FileManagementService.UploadStreamFiles()` creates multipart form content.
4. `TaskAttachmentConsumer.AddTaskAttachment()` sends to `/api/taskattachment`.
5. Server-side `AddTaskAttachmentCommand` handler uses `IFileManagement.UploadFile()` to store the file.
6. A `TaskAttachments` record is created linking the file to the task.

### Task Attachment Download

1. User clicks download in the UI.
2. `TaskAttachmentConsumer.GetAttachmentInfo(fileId)` fetches file as byte array.
3. `FileManagementService.DownloadFile(bytes, fileName)` triggers browser download via JS interop.

### Task Attachment Preview

1. User clicks preview in the UI.
2. `FileManagementService.CanPreview(fileName)` checks if the file type supports preview.
3. If previewable, `FileManagementService.PreviewFile(bytes, fileName, contentType)` opens in a new browser tab.

## File Storage

Files are stored on the server filesystem in the `TaskyRevamp.WebAPI/Upload/` directory, organized by file type. The `IFileManagement` implementation manages the physical file storage and the metadata tracking.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Interface | `IFileManagement.cs` (`Domain/Interfaces/`) |
| File Type | `FileType.cs` (`Domain/`) |
| DTO | `FileDto.cs` (`Dto/Files/`) |
| Client Service | `FileManagementService.cs` (`Client/Services/`) |
| Attachment Logic | `TaskyRevamp.Services/TaskAttachments/`, `TaskAttachmentController.cs` |
| Consumer | `TaskAttachmentConsumer.cs` (`Client/Consumer/`) |
| Storage | `TaskyRevamp.WebAPI/Upload/` (physical file storage) |
| Excel Library | `DocumentFormat.OpenXml` NuGet package |
