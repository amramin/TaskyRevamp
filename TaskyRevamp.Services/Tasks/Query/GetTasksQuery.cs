using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.Tasks.Services;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTasksQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldTask> SearchFields, string SearchText, int viewType, Guid? viewTypeId, bool IsCompleted = false,TaskFilterComponent TaskFilter=null) : IRequest<PagedResult<CreateTaskDto>>;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, PagedResult<CreateTaskDto>>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly TaskDtoEnricher _dtoEnricher;

    public GetTasksHandler(
        IRepository<TaskItem> taskRepository,
        TaskDtoEnricher dtoEnricher)
    {
        _taskRepository = taskRepository;
        _dtoEnricher = dtoEnricher;
    }

    public async Task<PagedResult<CreateTaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var orderBy = TaskSortingService.GetOrderBy(request.sortByColumnName, request.sortAscending);
        var searchExpression = BuildSearchExpression(request);

        var res = await _taskRepository.GetPagedAsync(
            request.pageNumber,
            request.pageSize,
            t => t.IsDeleted == false,
            searchExpression,
            orderBy: orderBy,
            includeProperties: $"{nameof(TaskItem.CreatedBy)},{nameof(TaskItem.Priority)},{nameof(TaskItem.ActualWeight)},{nameof(TaskItem.Type)},{nameof(TaskItem.Source)},{nameof(TaskItem.status)},{nameof(TaskItem.UpdatedBy)},{nameof(TaskItem.TaskAssignees)}.{nameof(TaskAssignee.User)},{nameof(TaskItem.ChangeEndDateRequests)}");

        var alltasks = new List<CreateTaskDto>();
        foreach (var tsk in res.Items)
        {
            tsk.ActualWeight = new Weight(tsk.Weight ?? 0);
            tsk.PlannedWeight = new Weight(tsk.Weight ?? 0);
            var dto = tsk.ToDto();
            dto = await _dtoEnricher.EnrichAsync(tsk, dto, currentCulture);
            alltasks.Add(dto);
        }

        return new PagedResult<CreateTaskDto>
        {
            Items = alltasks,
            TotalCount = res.TotalCount,
            PageNumber = request.pageNumber,
            PageSize = request.pageSize
        };
    }

    private static Expression<Func<TaskItem, bool>> BuildSearchExpression(GetTasksQuery request)
    {
        Expression<Func<TaskItem, bool>> searchExpression = request.IsCompleted
            ? t => t.StatusId == TaskStatusConstants.Completed
            : t => t.StatusId != TaskStatusConstants.Deleted && t.StatusId != TaskStatusConstants.Completed;

        if (request.TaskFilter is null)
        {
            // No advanced filter — apply view-type filters directly
            if (request.viewType == (int)ViewTypes.TimeLineView)
            {
                var timelineFilter = TaskTimelineFilterService.BuildTimelineFilter(request.viewTypeId);
                if (timelineFilter is not null)
                    searchExpression = searchExpression.And(timelineFilter);
            }
            else if (request.viewType == (int)ViewTypes.StatusView)
            {
                searchExpression = searchExpression.And(t => t.StatusId == request.viewTypeId);
            }
            else if (request.viewType == (int)ViewTypes.SourceView)
            {
                searchExpression = request.viewTypeId == Guid.Empty
                    ? searchExpression.And(t => t.TaskSourceId == null)
                    : searchExpression.And(t => t.TaskSourceId == request.viewTypeId);
            }
            else if (request.viewType == (int)ViewTypes.TypeView)
            {
                searchExpression = request.viewTypeId == Guid.Empty
                    ? searchExpression.And(t => t.TaskTypeId == null)
                    : searchExpression.And(t => t.TaskTypeId == request.viewTypeId);
            }
        }
        else
        {
            // Advanced filter exists — apply timeline + view overrides, then delegate to SearchDelegate
            if (request.viewType == (int)ViewTypes.TimeLineView)
            {
                var timelineFilter = TaskTimelineFilterService.BuildTimelineFilter(request.viewTypeId);
                if (timelineFilter is not null)
                    searchExpression = searchExpression.And(timelineFilter);
            }
            else
            {
                TaskViewFilterService.ApplyViewTypeOverrides(request.TaskFilter, request.viewType, request.viewTypeId);
            }

            var filterExpression = SearchDelegate(request.TaskFilter);
            if (filterExpression is not null)
                searchExpression = searchExpression.And(filterExpression);
        }

        return searchExpression;
    }

    private static Expression<Func<TaskItem, bool>> SearchDelegate(TaskFilterComponent taskFilter)
    {
        // Normalize empty lists to null (from master bug fix)
        if (taskFilter.Priority?.Count == 0) taskFilter.Priority = null;
        if (taskFilter.Status?.Count == 0) taskFilter.Status = null;
        if (taskFilter.Source?.Count == 0) taskFilter.Source = null;
        if (taskFilter.Type?.Count == 0) taskFilter.Type = null;
        if (taskFilter.AssignedTo?.Count == 0) taskFilter.AssignedTo = null;
        if (taskFilter.AssignedToDepartment?.Count == 0) taskFilter.AssignedToDepartment = null;
        if (taskFilter.CreatedBy?.Count == 0) taskFilter.CreatedBy = null;
        if (taskFilter.CreatedByDepartment?.Count == 0) taskFilter.CreatedByDepartment = null;

        Expression<Func<TaskItem, bool>> expression = null;
        if (taskFilter == null) return expression;

        // Convert Guid.Empty to null for Source/Type filters (from master bug fix)
        if (taskFilter.Source is not null && taskFilter.Source.Contains(Guid.Empty))
        {
            taskFilter.Source.RemoveAll(x => x == Guid.Empty);
            taskFilter.Source.Add(null);
        }

        if (taskFilter.Type is not null && taskFilter.Type.Contains(Guid.Empty))
        {
            taskFilter.Type.RemoveAll(x => x == Guid.Empty);
            taskFilter.Type.Add(null);
        }

        expression = t =>
            (string.IsNullOrEmpty(taskFilter.Title) || (t.Title != null && t.Title.ToLower().Contains(taskFilter.Title.ToLower()))) &&
            (taskFilter.Priority == null || taskFilter.Priority.Contains(t.PriorityId)) &&
            (taskFilter.Status == null || taskFilter.Status.Any(s => s.HasValue && s.Value == t.StatusId)) &&
            (taskFilter.Source == null || taskFilter.Source.Any(s => s == null ? t.TaskSourceId == null : t.TaskSourceId == s.Value)) &&
            (taskFilter.Type == null || taskFilter.Type.Any(s => s == null ? t.TaskTypeId == null : t.TaskTypeId == s.Value)) &&
            (taskFilter.AssignedTo == null || (t.TaskAssignees != null && t.TaskAssignees.Any(ass => taskFilter.AssignedTo.Contains(ass.UserId)))) &&
            (taskFilter.AssignedToDepartment == null || (t.AssignedDepartmentIds != null && t.AssignedDepartmentIds.Any(id => taskFilter.AssignedToDepartment.Contains(id)))) &&
            (taskFilter.CreatedBy == null || taskFilter.CreatedBy.Contains(t.CreatedById)) &&
            (taskFilter.CreatedByDepartment == null || taskFilter.CreatedByDepartment.Contains(t.CreatedBy.Department!.Id));

        // Start date range (using DateOnly for date-only comparison — from master bug fix)
        if (taskFilter.FromStartDate.HasValue && taskFilter.ToStartDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.FromStartDate ?? default) <= DateOnly.FromDateTime(t.StartDate ?? default)
                && DateOnly.FromDateTime(taskFilter.ToStartDate ?? default) >= DateOnly.FromDateTime(t.StartDate ?? default));
        else if (taskFilter.FromStartDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.FromStartDate ?? default) <= DateOnly.FromDateTime(t.StartDate ?? default));
        else if (taskFilter.ToStartDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.ToStartDate ?? default) >= DateOnly.FromDateTime(t.StartDate ?? default));

        // End date range
        if (taskFilter.FromEndDate.HasValue && taskFilter.ToEndDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.FromEndDate ?? default) <= DateOnly.FromDateTime(t.EndDate ?? default)
                && DateOnly.FromDateTime(taskFilter.ToEndDate ?? default) >= DateOnly.FromDateTime(t.EndDate ?? default));
        else if (taskFilter.FromEndDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.FromEndDate ?? default) <= DateOnly.FromDateTime(t.EndDate ?? default));
        else if (taskFilter.ToEndDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.ToEndDate ?? default) >= DateOnly.FromDateTime(t.EndDate ?? default));

        // Creation date range
        if (taskFilter.FromCreationDate.HasValue && taskFilter.ToCreationDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.FromCreationDate ?? default) <= DateOnly.FromDateTime(t.CreateDate)
                && DateOnly.FromDateTime(taskFilter.ToCreationDate ?? default) >= DateOnly.FromDateTime(t.CreateDate));
        else if (taskFilter.FromCreationDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.FromCreationDate ?? default) <= DateOnly.FromDateTime(t.CreateDate));
        else if (taskFilter.ToCreationDate.HasValue)
            expression = expression.And(t => DateOnly.FromDateTime(taskFilter.ToCreationDate ?? default) >= DateOnly.FromDateTime(t.CreateDate));

        return expression;
    }
}
