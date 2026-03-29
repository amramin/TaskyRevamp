using TaskyRevamp.Domain.Models.Task;

namespace TaskyRevamp.Services.Tasks.Services;

/// <summary>
/// Centralises task sorting logic for query results.
/// Extracted from GetTasksQuery to reduce the god handler (HI-01).
/// </summary>
public static class TaskSortingService
{
    /// <summary>
    /// Returns a sorting function for the given column name and direction.
    /// </summary>
    public static Func<IQueryable<TaskItem>, IOrderedQueryable<TaskItem>> GetOrderBy(string sortByColumn, bool sortAscending)
    {
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        return sortByColumn switch
        {
            "Creation Date" => sortAscending
                ? q => q.OrderBy(u => u.CreateDate)
                : q => q.OrderByDescending(u => u.CreateDate),

            "Title" => sortAscending
                ? q => q.OrderBy(u => u.Title)
                : q => q.OrderByDescending(u => u.Title),

            "Priority" => sortAscending
                ? q => q.OrderBy(u => u.Priority.Order)
                : q => q.OrderByDescending(u => u.Priority.Order),

            "Source" => sortAscending
                ? q => q.OrderBy(u => u.Source)
                : q => q.OrderByDescending(u => u.Source),

            "UpdateDate" => sortAscending
                ? q => q.OrderBy(u => u.UpdateDate)
                : q => q.OrderByDescending(u => u.UpdateDate),

            "Created By" => sortAscending
                ? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
                : q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish),

            "UpdatedBy" => sortAscending
                ? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
                : q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish),

            "Status" => sortAscending
                ? q => q.OrderBy(u => u.status!.NameEnglish)
                : q => q.OrderByDescending(u => u.status!.NameEnglish),

            "Start Date" => sortAscending
                ? q => q.OrderBy(u => u.StartDate)
                : q => q.OrderByDescending(u => u.StartDate),

            "End Date" => sortAscending
                ? q => q.OrderBy(u => u.EndDate)
                : q => q.OrderByDescending(u => u.EndDate),

            "weight" => sortAscending
                ? q => q.OrderBy(u => u.Weight)
                : q => q.OrderByDescending(u => u.Weight),

            "Type" => sortAscending
                ? q => q.OrderBy(u => u.Type)
                : q => q.OrderByDescending(u => u.Type),

            "Actual progress" => sortAscending
                ? q => q.OrderBy(u => u.Progress)
                : q => q.OrderByDescending(u => u.Progress),

            "Created by department" => sortAscending
                ? q => q.OrderBy(u => currentCulture == "ar" ? u.CreatedBy!.Department!.NameArabic : u.CreatedBy.Department!.NameEnglish)
                : q => q.OrderByDescending(u => currentCulture == "ar" ? u.CreatedBy.Department!.NameArabic : u.CreatedBy.Department!.NameEnglish),

            _ => q => q.OrderBy(u => u.CreateDate),
        };
    }
}
