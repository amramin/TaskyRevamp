# Search and Filtering

This document provides a comprehensive description of the search and filtering features in TaskyRevamp, covering dynamic search field mappings, pagination, sorting, and the advanced filtering system.

## Overview

TaskyRevamp provides a flexible search and filtering system that works across multiple entities (tasks, users, departments, privileges, delegations, etc.). The system uses dynamic search field enums and field-to-property mapping classes to allow configurable, extensible search without hardcoding filter logic.

## Pagination

### DTO: PagingParameterModel

Used for all paginated queries.

| Property | Type | Description |
|----------|------|-------------|
| `Page` | `int` | Current page number (1-based) |
| `PageSize` | `int` | Number of items per page |
| `Total` | `int` | Total item count (populated by the server) |

### DTO: PagedResult&lt;TEntity&gt;

Returned by all paginated queries.

| Property | Type | Description |
|----------|------|-------------|
| `Items` | `List<TEntity>` | Data items for the current page |
| `TotalCount` | `int` | Total number of matching items |
| `PageNumber` | `int` | Current page number |
| `PageSize` | `int` | Items per page |
| `TotalPages` | `int` | Computed: `ceil(TotalCount / PageSize)` |

### Configuration: PaginationSettings

| Property | Type | Description |
|----------|------|-------------|
| `DefaultPageSize` | `int` | Default number of items per page |

### Page Size Options

Available in the UI: `5, 10, 25, 100`.

### Client-side Query Building

`TaskyService.PreparePaginatedSearchQueryString()` builds query parameters:

```
?pageNumber={page}&pageSize={size}&sortByColumnName={column}&sortAscending={bool}&searchFields={fields}&searchText={text}&viewType={type}&viewTypeId={id}&isCompleted={bool}
```

`TaskyService.PrepareNoPaginatedSearchQueryString()` builds search queries without pagination.

## Search Field Enums

Each entity type has a dedicated search field enum that defines which fields can be searched:

| Enum | Entity | Description |
|------|--------|-------------|
| `SearchFieldTask` | TaskItem | Fields for task search |
| `SearchFieldDepartment` | Department | Fields for department search |
| `SearchFieldUser` | User | Fields for user search |
| `SearchFieldPrivilege` | Privilege | Fields for privilege search |
| `SearchFieldDelegation` | UserDelegation | Fields for delegation search |
| `SearchFieldUserDepartment` | User (by dept) | Fields for user-department search |
| `SearchFieldUserPrivilege` | User (by priv) | Fields for user-privilege search |
| `SearchFieldDeletedTask` | Deleted tasks | Fields for recycle bin search |
| `SearchFieldChangeDueDate` | ChangeEndDateRequest | Fields for due date request search |

## Search Field Mappings

Located in `TaskyRevamp.Services/SearchMappings/`.

Each mapping class translates a search field enum value to an EF Core expression on the corresponding entity property. This allows the search system to dynamically build LINQ queries based on user-selected search fields.

| Mapping Class | Entity | Description |
|---------------|--------|-------------|
| `TaskSearchFieldMap` | TaskItem | Maps search fields to task properties (title, description, status, priority, etc.) |
| `DepartmentSearchFieldMap` | Department | Maps to department name properties |
| `UserSearchFieldMap` | User | Maps to user name, email, username properties |
| `PrivilegeSearchFieldMap` | Privilege | Maps to privilege name properties |
| `UserPrivilegeSearchFieldMap` | User | Maps for user-by-privilege filtering |
| `UserDepartmentSearchFieldMap` | User | Maps for user-by-department filtering |
| `UserDelegationSearchFieldMap` | UserDelegation | Maps for delegation search |
| `RequestChangeDueDateSearchFieldMap` | ChangeEndDateRequest | Maps for due date request search |
| `DeletedTaskSearchFieldMap` | TaskItem (deleted) | Maps for recycle bin search |
| `TaskSourceSearchFieldMap` | Source | Maps for source configuration search |
| `TaskTypeSearchFieldMap` | Type | Maps for type configuration search |

### How Search Works

1. The UI sends a search request with:
   - `searchFields`: List of field enum values to search.
   - `searchText`: The text to search for.
   - `sortByColumnName`: Column to sort by.
   - `sortAscending`: Sort direction.
2. The query handler uses the appropriate search field map to build a LINQ `Where` expression.
3. The expression is applied to the EF Core query before pagination.
4. Results are returned as `PagedResult<T>`.

## Task-Specific Filtering

### TaskFilterComponent

An advanced filtering component for complex task queries that goes beyond simple text search.

### GetTasksQuery Parameters

The main task query (`GetTasksQuery`) accepts extensive filter parameters:

| Parameter | Type | Description |
|-----------|------|-------------|
| `pageNumber` | `int` | Current page |
| `pageSize` | `int` | Items per page |
| `sortByColumnName` | `string` | Sort column |
| `sortAscending` | `bool` | Sort direction |
| `searchFields` | `List<SearchFieldTask>` | Which fields to search |
| `searchText` | `string` | Search keyword |
| `viewType` | `int` | View type (ListView, CalendarView, TimeLineView, BoardView) |
| `viewTypeId` | `Guid?` | View-specific filter |
| `isCompleted` | `bool` | Filter by completion status |
| `filterComponent` | `TaskFilterComponent` | Advanced filter criteria |

### View Type Support

Different view types may filter and present data differently:

| ViewType | Value | Description |
|----------|-------|-------------|
| `ListView` | 0 | Standard table view |
| `CalendarView` | 1 | Calendar-based display |
| `TimeLineView` | 2 | Timeline/Gantt-style view |
| `BoardView` | 3 | Kanban board view |
| `GanttView` | 4 | Gantt chart view |

## Sorting

All paginated queries support server-side sorting:

- The `sortByColumnName` parameter specifies which column to sort by.
- The `sortAscending` boolean determines the sort direction.
- Sorting is applied in the EF Core query via `OrderBy` / `OrderByDescending` with dynamic expression building.

## UI Components

### Search Component

`Search.razor` in `TaskyRevamp.Client/Shared/` — Search input component that captures user search text and triggers search operations.

### Pagination Component

`Pagination.razor` in `TaskyRevamp.Client/Shared/` — Table pagination control.

| Parameter | Type | Description |
|-----------|------|-------------|
| `CurrentPage` | `int` | Current page number |
| `TotalPages` | `int` | Total number of pages |
| `TotalCount` | `int` | Total items |
| `PageSize` | `int` | Items per page (default: 10) |
| `ShowPageSize` | `bool` | Show page size dropdown (default: true) |
| `PageSizeOptions` | `int[]` | `{5, 10, 25, 100}` |
| `PageChanged` | `EventCallback<int>` | Page change callback |
| `PageSizeChangedCallback` | `EventCallback<int>` | Page size change callback |

UI elements:
- Page size dropdown (left).
- Item counter showing "X-Y of Z" (right).
- Page number buttons with first/last/previous/next navigation (center).

### SortableTable Component

`SortableTable.razor` in `TaskyRevamp.Client/Shared/` — Data table with clickable column headers for sorting.

### SortableColumn

`SortableColumn.cs` in `TaskyRevamp.Client/Shared/` — Column definition used by the sortable table.

## Configurable Filter Fields

The available filter fields in the task list are configurable through the Filter Fields Settings (see [System Configuration](system-configuration.md) — Filter Fields Settings). Administrators can enable or disable specific filters via the `FilterFieldsConfiguration.razor` page.

## Related Files Summary

| Area | Key Files |
|------|-----------|
| Search Mappings | All `*SearchFieldMap.cs` files in `TaskyRevamp.Services/SearchMappings/` |
| Search Enums | `SearchFieldTask.cs`, `SearchFieldDepartment.cs`, etc. in `TaskyRevamp.Dto/` |
| Pagination | `PagingParameterModel.cs`, `PagedResult.cs` in `TaskyRevamp.Dto/GeneralDto/` |
| Filter Settings | `FilterFieldsSettings.cs`, `FilterFieldsSettingDto.cs` |
| UI Components | `Search.razor`, `Pagination.razor`, `SortableTable.razor`, `SortableColumn.cs` in `Client/Shared/` |
| Task Service | `TaskyService.cs` — `PreparePaginatedSearchQueryString()`, `PrepareNoPaginatedSearchQueryString()` |
| Task Query | `GetTasksQuery.cs`, `GetTasksHandler.cs` in `Services/Tasks/` |
