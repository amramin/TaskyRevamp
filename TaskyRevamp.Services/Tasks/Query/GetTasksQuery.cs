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
        IRepository<Department> departmentRepository,
        IRepository<TaskDependencies> taskDependenciesRepository)
    {
        _taskRepository = taskRepository;
        _dtoEnricher = new TaskDtoEnricher(departmentRepository, taskDependenciesRepository);
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
            dto = await _dtoEnricher.EnrichAsync(tsk, dto, currentCulture, _taskRepository);
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
        Expression<Func<TaskItem, bool>> expression = null;
        if (taskFilter == null) return expression;

        expression = t =>
            (string.IsNullOrEmpty(taskFilter.Title) || (t.Title != null && t.Title.ToLower().Contains(taskFilter.Title.ToLower()))) &&
            (taskFilter.Priority == null || taskFilter.Priority.Contains(t.PriorityId)) &&
            (taskFilter.Status == null || taskFilter.Status.Contains(t.StatusId)) &&
            (taskFilter.Source == null || taskFilter.Source.Contains(t.TaskSourceId)) &&
            (taskFilter.Type == null || taskFilter.Type.Contains(t.TaskTypeId)) &&
            (taskFilter.AssignedTo == null || (t.TaskAssignees != null && t.TaskAssignees.Any(ass => taskFilter.AssignedTo.Contains(ass.UserId)))) &&
            (taskFilter.AssignedToDepartment == null || (t.AssignedDepartmentIds != null && t.AssignedDepartmentIds.Any(id => taskFilter.AssignedToDepartment.Contains(id)))) &&
            (taskFilter.CreatedBy == null || taskFilter.CreatedBy.Contains(t.CreatedById)) &&
            (taskFilter.CreatedByDepartment == null || taskFilter.CreatedByDepartment.Contains(t.CreatedBy.Department!.Id));

        // Start date range
        if (taskFilter.FromStartDate.HasValue && taskFilter.ToStartDate.HasValue)
            expression = expression.And(t => taskFilter.FromStartDate <= t.StartDate && taskFilter.ToStartDate >= t.StartDate);
        else if (taskFilter.FromStartDate.HasValue)
            expression = expression.And(t => taskFilter.FromStartDate <= t.StartDate);
        else if (taskFilter.ToStartDate.HasValue)
            expression = expression.And(t => taskFilter.ToStartDate >= t.StartDate);

        // End date range
        if (taskFilter.FromEndDate.HasValue && taskFilter.ToEndDate.HasValue)
            expression = expression.And(t => taskFilter.FromEndDate <= t.EndDate && taskFilter.ToEndDate >= t.EndDate);
        else if (taskFilter.FromEndDate.HasValue)
            expression = expression.And(t => taskFilter.FromEndDate <= t.EndDate);
        else if (taskFilter.ToEndDate.HasValue)
            expression = expression.And(t => taskFilter.ToEndDate >= t.EndDate);

        // Creation date range
        if (taskFilter.FromCreationDate.HasValue && taskFilter.ToCreationDate.HasValue)
            expression = expression.And(t => taskFilter.FromCreationDate <= t.CreateDate && taskFilter.ToCreationDate >= t.CreateDate);
        else if (taskFilter.FromCreationDate.HasValue)
            expression = expression.And(t => taskFilter.FromCreationDate <= t.CreateDate);
        else if (taskFilter.ToCreationDate.HasValue)
            expression = expression.And(t => taskFilter.ToCreationDate >= t.CreateDate);

        return expression;
    }
}
