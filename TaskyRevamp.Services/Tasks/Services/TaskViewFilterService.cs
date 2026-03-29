using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.Tasks.Services;

/// <summary>
/// Centralises view-type filter/override logic for Status, Source, and Type views.
/// Extracted from GetTasksQuery to reduce the god handler (HI-01).
/// </summary>
public static class TaskViewFilterService
{
    /// <summary>
    /// Applies view-type overrides to the TaskFilterComponent when a filter already exists.
    /// Mutates the filter in place so the SearchDelegate can use the updated values.
    /// </summary>
    public static void ApplyViewTypeOverrides(TaskFilterComponent taskFilter, int viewType, Guid? viewTypeId)
    {
        if (viewType == (int)ViewTypes.StatusView)
        {
            if (taskFilter.Status is null)
            {
                taskFilter.Status = new List<Guid?> { viewTypeId };
            }
            else
            {
                taskFilter.Status = taskFilter.Status.Contains(viewTypeId)
                    ? new List<Guid?> { viewTypeId }
                    : new List<Guid?> { null };
            }
        }
        else if (viewType == (int)ViewTypes.SourceView)
        {
            if (taskFilter.Source is null)
            {
                taskFilter.Source = viewTypeId == Guid.Empty
                    ? new List<Guid?> { null }
                    : new List<Guid?> { viewTypeId };
            }
            else
            {
                taskFilter.Source = taskFilter.Source.Contains(viewTypeId)
                    ? new List<Guid?> { viewTypeId }
                    : new List<Guid?> { Guid.Empty };
            }
        }
        else if (viewType == (int)ViewTypes.TypeView)
        {
            if (taskFilter.Type is null)
            {
                taskFilter.Type = viewTypeId == Guid.Empty
                    ? new List<Guid?> { null }
                    : new List<Guid?> { viewTypeId };
            }
            else
            {
                taskFilter.Type = taskFilter.Type.Contains(viewTypeId)
                    ? new List<Guid?> { viewTypeId }
                    : new List<Guid?> { Guid.Empty };
            }
        }
    }
}
