using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;

using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTasksQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldTask> SearchFields, string SearchText) : IRequest<PagedResult<CreateTaskDto>>;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, PagedResult<CreateTaskDto>>
{
    private string _currentLanguage;


    private readonly IRepository<User> _userRepository;
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IRepository<Department> _departmenRepository;
    private readonly IRepository<TaskDependencies> _taskDependincesRepository;

    public GetTasksHandler(IRepository<TaskItem> taskRepository, IRepository<User> userRepository, IRepository<TaskDependencies> taskDependincesRepository, IRepository<Department> departmentRepository)
    {
        _taskRepository = taskRepository;
        _departmenRepository = departmentRepository;
        _userRepository = userRepository;
        _currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        _taskDependincesRepository = taskDependincesRepository;

    }

    public async Task<PagedResult<CreateTaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        List<CreateTaskDto> alltasks = new List<CreateTaskDto>();
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;



        var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

        Expression<Func<TaskItem, bool>> searchExpression = null;
        if (request.SearchFields != null && request.SearchFields.Any())
        {
            var predicates = request.SearchFields.Select(x => TaskSearchFieldMap.Map[x]).ToList();
            searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
        }

        var res = await _taskRepository.GetPagedAsync(
                            request.pageNumber,
                            request.pageSize,
                            null,
                            searchExpression,
                            orderBy: orderBy,
                            includeProperties: $"{nameof(TaskItem.CreatedBy)},{nameof(TaskItem.Priority)},{nameof(TaskItem.ActualWeight)},{nameof(TaskItem.Type)},{nameof(TaskItem.Source)},{nameof(TaskItem.status)},{nameof(TaskItem.UpdatedBy)},{nameof(TaskItem.Assignees)}.{nameof(TaskyRevamp.Domain.Models.Task.TaskAssignees.User)}");

        foreach (var tsk in res.Items)
        {
            var assgnedusr = await _userRepository.FindBy(k => tsk.AssignedIds.Contains(k.Id));
            var CreatorDepartment =  _departmenRepository.FirstOrDefaultAsNoTracking(k => k.Id == (tsk.CreatedBy.DepartmentId??Guid.Empty));
            tsk.ActualWeight=new Weight(tsk.Weight ?? 0);
            tsk.PlannedWeight = new Weight(tsk.Weight ?? 0);
            CreateTaskDto tasky = tsk.CopyToDto();
            tasky.TypeName = currentCulture == "ar" ? tsk.Type?.NameArabic : tsk.Type?.NameEnglish;
            tasky.SourceName = currentCulture == "ar" ? tsk.Source?.NameArabic : tsk.Source?.NameEnglish;
            tasky.PriorityName = currentCulture == "ar" ? tsk.Priority?.NameArabic : tsk.Priority?.NameEnglish;
            var departments = await _departmenRepository.FindBy(k => tsk.AssignedDepartmentIds.Contains(k.Id));
            var depsName = currentCulture == "ar" ? departments.Value.Select(k => k.NameArabic): departments.Value.Select(k => k.NameEnglish);
            tasky.AssignedDepartmentName = string.Join(" ", depsName);
            var dependencies = await _taskDependincesRepository.FindBy(p => p.TaskItemId == tasky.Id);
            if (dependencies is not null)
            {
                var dependenciesids = dependencies.Value!.Select(p => p.DependentId);
                var tasksdependent = await _taskRepository.FindBy(p => dependenciesids.Contains(p.Id));
                var DependencyNames = string.Join(", ", tasksdependent.Value!.Select(d => d.Title));
                tasky.DependencyNames= DependencyNames;

            }
            tasky.TaskStatusName = currentCulture == "ar" ? tsk.status?.NameArabic : tsk.status?.NameEnglish;
            tasky.CreatedByName = currentCulture == "ar" ? tsk.CreatedBy?.NameArabic : tsk.CreatedBy?.NameEnglish;
            tasky.Createdbydepartment = currentCulture == "ar" ? CreatorDepartment?.NameArabic! : CreatorDepartment?.NameEnglish!;
            tasky.UpdatedBy = currentCulture == "ar" ? tsk.UpdatedBy?.NameArabic : tsk.UpdatedBy?.NameEnglish;
            tasky.AssigneduserNames = string.Join(",", assgnedusr.Value!.Select(u => currentCulture == "ar" ? u.NameArabic : u.NameEnglish));
            alltasks.Add(tasky);
        }
        return new PagedResult<CreateTaskDto>
        {
            Items = alltasks,
            TotalCount = res.TotalCount,
            PageNumber = request.pageNumber,
            PageSize = request.pageSize
        };
    }
    private Func<IQueryable<TaskItem>, IOrderedQueryable<TaskItem>> GetOrderBy(string sortByColumn, bool sortAscending)
    {
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        switch (sortByColumn)
        {
            case "Creation Date":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreateDate)
                    : q => q.OrderByDescending(u => u.CreateDate);
            case "Title":
                return sortAscending
                    ? q => q.OrderBy(u => currentCulture == "ar" ? u.Title : u.Title)
                    : q => q.OrderByDescending(u => currentCulture == "ar" ? u.Title : u.Title);
            case "Priority":
                return sortAscending
                    ? q => q.OrderBy(u => u.Priority)
                    : q => q.OrderByDescending(u => u.Priority);
            //case "Planned Progress":
            //    return sortAscending
            //        ? q => q.OrderBy(u => u.Priority)
            //        : q => q.OrderByDescending(u => u.Priority);
            case "Source":
                return sortAscending
                    ? q => q.OrderBy(u => u.Source)
                    : q => q.OrderByDescending(u => u.Source);
            case "UpdateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdateDate)
                    : q => q.OrderByDescending(u => u.UpdateDate);

            case "Created By":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish);

            case "UpdatedBy":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);

            case "Status":

                return sortAscending
                    ? q => q.OrderBy(u => u.status!.NameEnglish)
                    : q => q.OrderByDescending(u => u.status!.NameEnglish);
            case "Start Date":
                return sortAscending
                    ? q => q.OrderBy(u => u.StartDate)
                    : q => q.OrderByDescending(u => u.StartDate);
            case "End Date":
                return sortAscending
                    ? q => q.OrderBy(u => u.EndDate)
                    : q => q.OrderByDescending(u => u.EndDate);
            case "weight":
                return sortAscending
                    ? q => q.OrderBy(u => u.Weight)
                    : q => q.OrderByDescending(u => u.Weight);
            case "Type":
                return sortAscending
                    ? q => q.OrderBy(u => u.Type)
                    : q => q.OrderByDescending(u => u.Type);
            case "Actual Progress":
                return sortAscending
                    ? q => q.OrderBy(u => u.Progress)
                    : q => q.OrderByDescending(u => u.Progress);
            case "Created by department":
                return sortAscending
                    ? q => q.OrderBy(u => currentCulture == "ar" ? u.CreatedBy.Department.NameArabic : u.CreatedBy.Department.NameEnglish)
                    : q => q.OrderByDescending(u => currentCulture == "ar" ? u.CreatedBy.Department.NameArabic : u.CreatedBy.Department.NameEnglish);
            default:
                return q => q.OrderBy(u => u.CreateDate);
        }
    }
}