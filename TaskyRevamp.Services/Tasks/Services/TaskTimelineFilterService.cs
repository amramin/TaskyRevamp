using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Services.Helpers;

namespace TaskyRevamp.Services.Tasks.Services;

/// <summary>
/// Centralises timeline-based filter expressions for tasks.
/// Extracted from GetTasksQuery to reduce the god handler (HI-01).
/// Eliminates the duplicated timeline blocks that appeared twice in Handle().
/// </summary>
public static class TaskTimelineFilterService
{
    /// <summary>
    /// Builds a timeline filter expression based on the view type ID.
    /// </summary>
    public static Expression<Func<TaskItem, bool>>? BuildTimelineFilter(Guid? viewTypeId)
    {
        if (viewTypeId == Guid.Parse(TimeLineView.Delayed.GetDescription()))
        {
            return t => t.EndDate < DateTime.Now.Date;
        }

        if (viewTypeId == Guid.Parse(TimeLineView.Today.GetDescription()))
        {
            return t => t.EndDate == DateTime.Now.Date;
        }

        if (viewTypeId == Guid.Parse(TimeLineView.ThisWeek.GetDescription()))
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            return t =>
                t.EndDate > today &&
                t.EndDate >= startOfWeek &&
                t.EndDate < endOfWeek;
        }

        if (viewTypeId == Guid.Parse(TimeLineView.ThisMonth.GetDescription()))
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            var startOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

            return t =>
                t.EndDate > endOfWeek &&
                t.EndDate < startOfNextMonth;
        }

        if (viewTypeId == Guid.Parse(TimeLineView.NextMonths.GetDescription()))
        {
            var today = DateTime.Today;
            var startOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

            return t => t.EndDate >= startOfNextMonth;
        }

        return null;
    }
}
