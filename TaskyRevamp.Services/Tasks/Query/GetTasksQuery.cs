using MediatR;
using MimeKit.Cryptography;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;

using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTasksQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldTask> SearchFields, string SearchText, int viewType, Guid? viewTypeId, bool IsCompleted = false,TaskFilterComponent TaskFilter=null) : IRequest<PagedResult<CreateTaskDto>>;

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
        if (request.IsCompleted)
        {
            searchExpression = t => t.StatusId == Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C");
        }
        else
        {
            searchExpression = t => t.StatusId != Guid.Parse("D8E94CCE-586A-46D3-223D-08DE3318A61C") && t.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C");

        }
        if(request.TaskFilter is null)
        {
            if (request.viewType == (int)ViewTypes.TimeLineView)
            {
                if (request.viewTypeId == Guid.Parse(TimeLineView.Delayed.GetDescription()))
                {
                    searchExpression = searchExpression.And(t => t.EndDate < DateTime.Now.Date);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.Today.GetDescription()))
                {
                    searchExpression = searchExpression.And(t => t.EndDate == DateTime.Now.Date);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.ThisWeek.GetDescription()))
                {
                    var today = DateTime.Today;

                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);

                    searchExpression = searchExpression.And(t =>
                        t.EndDate > today &&
                        t.EndDate >= startOfWeek &&
                        t.EndDate < endOfWeek);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.ThisMonth.GetDescription()))
                {
                    var today = DateTime.Today;
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);
                    // First day of current month
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);

                    // First day of next month
                    var startOfNextMonth = startOfMonth.AddMonths(1);

                    // Expression for EF Core
                    searchExpression = searchExpression.And(t =>
                        t.EndDate > endOfWeek &&
                        t.EndDate < startOfNextMonth);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.NextMonths.GetDescription()))
                {
                    var today = DateTime.Today;

                    // First day of next month
                    var startOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

                    // Expression: EndDate >= start of next month
                    searchExpression = searchExpression.And(t =>
                        t.EndDate >= startOfNextMonth);

                }
            }
            else if (request.viewType == (int)ViewTypes.StatusView)
            {
                searchExpression = searchExpression.And(t => t.StatusId == request.viewTypeId);
            }
            else if (request.viewType == (int)ViewTypes.SourceView)
            {
                if (request.viewTypeId == Guid.Empty)
                {
                    searchExpression = searchExpression.And(t => t.TaskSourceId == null);
                }
                else
                {
                    searchExpression = searchExpression.And(t => t.TaskSourceId == request.viewTypeId);
                }
            }
            else if (request.viewType == (int)ViewTypes.TypeView)
            {
                if (request.viewTypeId == Guid.Empty)
                {
                    searchExpression = searchExpression.And(t => t.TaskTypeId == null);

                }
                else
                {
                    searchExpression = searchExpression.And(t => t.TaskTypeId == request.viewTypeId);
                }
            }
        }
        else
        {
            if (request.viewType == (int)ViewTypes.TimeLineView)
            {
                if (request.viewTypeId == Guid.Parse(TimeLineView.Delayed.GetDescription()))
                {
                    searchExpression = searchExpression.And(t => t.EndDate < DateTime.Now.Date);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.Today.GetDescription()))
                {
                    searchExpression = searchExpression.And(t => t.EndDate == DateTime.Now.Date);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.ThisWeek.GetDescription()))
                {
                    var today = DateTime.Today;

                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);

                    searchExpression = searchExpression.And(t =>
                        t.EndDate > today &&
                        t.EndDate >= startOfWeek &&
                        t.EndDate < endOfWeek);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.ThisMonth.GetDescription()))
                {
                    var today = DateTime.Today;
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(7);
                    // First day of current month
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);

                    // First day of next month
                    var startOfNextMonth = startOfMonth.AddMonths(1);

                    // Expression for EF Core
                    searchExpression = searchExpression.And(t =>
                        t.EndDate > endOfWeek &&
                        t.EndDate < startOfNextMonth);
                }
                else if (request.viewTypeId == Guid.Parse(TimeLineView.NextMonths.GetDescription()))
                {
                    var today = DateTime.Today;

                    // First day of next month
                    var startOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

                    // Expression: EndDate >= start of next month
                    searchExpression = searchExpression.And(t =>
                        t.EndDate >= startOfNextMonth);

                }
            }
            else if (request.viewType == (int)ViewTypes.StatusView)
            {
                if(request.TaskFilter.Status is null)
                {
                    request.TaskFilter.Status=new List<Guid?> { request.viewTypeId };
                }
                else
                {
                    if (request.TaskFilter.Status.Contains(request.viewTypeId))
                    {
                        request.TaskFilter.Status = new List<Guid?> { request.viewTypeId };
                    }
                    else
                    {
                        request.TaskFilter.Status = new List<Guid?> { null };
                    }
                }
            }
            else if (request.viewType == (int)ViewTypes.SourceView)
            {
                if(request.TaskFilter.Source is null)
                {
                    if(request.viewTypeId == Guid.Empty)
                    {
                        request.TaskFilter.Source=new List<Guid?> { null};
                    }
                    else
                    {
                        request.TaskFilter.Source=new List<Guid?> { request.viewTypeId };
                    }
                }
                else
                {
                    if (request.TaskFilter.Source.Contains(request.viewTypeId))
                    {
                        request.TaskFilter.Source = new List<Guid?> { request.viewTypeId };
                    }
                    else
                    {
                        request.TaskFilter.Source = new List<Guid?> { Guid.Empty };
                    }
                }
            }
            else if (request.viewType == (int)ViewTypes.TypeView)
            {
                if(request.TaskFilter.Type is null)
                {
                    if (request.viewTypeId == Guid.Empty)
                    {
                        request.TaskFilter.Type = new List<Guid?> { null };

                    }
                    else
                    {
                        request.TaskFilter.Type = new List<Guid?> { request.viewTypeId };
                    }
                }
                else
                {
                    if (request.TaskFilter.Type.Contains(request.viewTypeId))
                    {
                        request.TaskFilter.Type = new List<Guid?> { request.viewTypeId };
                    }
                    else
                    {
                        request.TaskFilter.Type = new List<Guid?> { Guid.Empty };
                    }
                    
                }
            }
            var searchexpression = SearchDelegate(request.TaskFilter);
            searchExpression = searchexpression is null ? searchExpression : searchExpression.And(searchexpression);
        }
            var res = await _taskRepository.GetPagedAsync(
                                request.pageNumber,
                                request.pageSize,
                                t => t.IsDeleted == false,
                                searchExpression,
                                orderBy: orderBy,
                                includeProperties: $"{nameof(TaskItem.CreatedBy)},{nameof(TaskItem.Priority)},{nameof(TaskItem.ActualWeight)},{nameof(TaskItem.Type)},{nameof(TaskItem.Source)},{nameof(TaskItem.status)},{nameof(TaskItem.UpdatedBy)},{nameof(TaskItem.TaskAssignees)}.{nameof(TaskyRevamp.Domain.Models.Task.TaskAssignee.User)},{nameof(TaskItem.ChangeEndDateRequests)}");

        foreach (var tsk in res.Items)
        {
            //var assgnedusr = await _userRepository.FindBy(k => tsk.AssignedIds.Contains(k.Id));
            var CreatorDepartment =  _departmenRepository.FirstOrDefaultAsNoTracking(k => k.Id == (tsk.CreatedBy!.DepartmentId??Guid.Empty));
            tsk.ActualWeight=new Weight(tsk.Weight ?? 0);
            tsk.PlannedWeight = new Weight(tsk.Weight ?? 0);
            CreateTaskDto tasky = tsk.CopyToDto();
            tasky.TypeName = currentCulture == "ar" ? tsk.Type?.NameArabic??"" : tsk.Type?.NameEnglish??"";
            tasky.SourceName = currentCulture == "ar" ? tsk.Source?.NameArabic??"" : tsk.Source?.NameEnglish ?? "";
            tasky.PriorityName = currentCulture == "ar" ? tsk.Priority?.NameArabic??"" : tsk.Priority?.NameEnglish ?? "";
            tasky.PriorityBackgroundColor = tsk.Priority?.BackgroundColor;
            tasky.PriorityColor = tsk.Priority?.NameColor;
            var departments = await _departmenRepository.FindBy(k => tsk.AssignedDepartmentIds.Contains(k.Id));
            var depsName = currentCulture == "ar" ? departments.Value!.Select(k => k.NameArabic): departments.Value!.Select(k => k.NameEnglish);
            tasky.AssignedDepartmentName = string.Join(", ", depsName);
            var dependencies = await _taskDependincesRepository.FindBy(p => p.TaskItemId == tasky.Id);
            if (dependencies is not null)
            {
                var dependenciesids = dependencies.Value!.Select(p => p.DependentId);
                var tasksdependent = await _taskRepository.FindBy(p => dependenciesids.Contains(p.Id));
                var DependencyNames = string.Join(", ", tasksdependent.Value!.Select(d => d.Title));
                tasky.DependencyNames= DependencyNames;
                tasky.Dependencies = dependenciesids.ToList();
            }
            tasky.ChangeEndDateRequestCount = tsk.ChangeEndDateRequests?.Count(c=>c.Status==ChangeRequestStatus.Pending) ?? 0;
            tasky.TaskStatusName = currentCulture == "ar" ? tsk.status?.NameArabic ?? "" : tsk.status?.NameEnglish ?? "";
            tasky.TaskStatusBackgroundColor = tsk.status?.BackgroundColor;
            tasky.TaskStatusColor = tsk.status?.NameColor;
            tasky.CreatedByName = currentCulture == "ar" ? tsk.CreatedBy?.NameArabic : tsk.CreatedBy?.NameEnglish;
            tasky.Createdbydepartment = currentCulture == "ar" ? CreatorDepartment?.NameArabic! : CreatorDepartment?.NameEnglish!;
            tasky.UpdatedBy = currentCulture == "ar" ? tsk.UpdatedBy?.NameArabic : tsk.UpdatedBy?.NameEnglish;
            tasky.AssigneduserNames = string.Join(",", tsk.TaskAssignees!.Select(u => currentCulture == "ar" ? u.User.NameArabic??"" : u.User.NameEnglish??""));
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
                    ? q => q.OrderBy(u => u.Priority.Order)
                    : q => q.OrderByDescending(u => u.Priority.Order);
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
            case "Actual progress":
                return sortAscending
                    ? q => q.OrderBy(u => u.Progress)
                    : q => q.OrderByDescending(u => u.Progress);
            case "Created by department":
                return sortAscending
                    ? q => q.OrderBy(u => currentCulture == "ar" ? u.CreatedBy!.Department!.NameArabic : u.CreatedBy.Department!.NameEnglish)
                    : q => q.OrderByDescending(u => currentCulture == "ar" ? u.CreatedBy.Department!.NameArabic : u.CreatedBy.Department!.NameEnglish);
            default:
                return q => q.OrderBy(u => u.CreateDate);
        }
    }
    private Expression<Func<TaskItem, bool>> SearchDelegate(TaskFilterComponent taskFilter)
    {
        Expression<Func<TaskItem, bool>> Expression = null;
        if (taskFilter != null)
        {
            Expression = t => (string.IsNullOrEmpty(taskFilter.Title) ||(t.Title != null &&t.Title.ToLower().Contains(taskFilter.Title.ToLower()))) &&
            (taskFilter.Priority == null || taskFilter.Priority.Contains(t.PriorityId)) &&
            (taskFilter.Status == null || taskFilter.Status.Contains(t.StatusId)) &&
            (taskFilter.Source == null || taskFilter.Source.Contains(t.TaskSourceId)) &&
            (taskFilter.Type == null || taskFilter.Type.Contains(t.TaskTypeId)) &&
            (taskFilter.AssignedTo == null || (t.TaskAssignees != null && t.TaskAssignees.Any(ass => taskFilter.AssignedTo.Contains(ass.UserId)))) &&
            (taskFilter.AssignedToDepartment == null || (t.AssignedDepartmentIds != null && t.AssignedDepartmentIds.Any(id => taskFilter.AssignedToDepartment.Contains(id)))) &&
            (taskFilter.CreatedBy==null||taskFilter.CreatedBy.Contains(t.CreatedById))&&
            (taskFilter.CreatedByDepartment == null || taskFilter.CreatedByDepartment.Contains(t.CreatedBy.Department!.Id))
            ;
            //to Task Start Date
            if(taskFilter.FromStartDate.HasValue && taskFilter.ToStartDate.HasValue)
            {
                Expression=Expression.And(t => (taskFilter.FromStartDate <= t.StartDate && taskFilter.ToStartDate >= t.StartDate));
            }else if (taskFilter.FromStartDate.HasValue && !taskFilter.ToStartDate.HasValue)
            {
                Expression = Expression.And(t => taskFilter.FromStartDate <= t.StartDate);
            }else if(!taskFilter.FromStartDate.HasValue && taskFilter.ToStartDate.HasValue)
            {
                Expression = Expression.And(t => taskFilter.ToStartDate >= t.StartDate);
            }

            //To Task End Date
            if (taskFilter.FromEndDate.HasValue && taskFilter.ToEndDate.HasValue)
            {
                Expression = Expression.And(t => (taskFilter.FromEndDate <= t.EndDate && taskFilter.ToEndDate >= t.EndDate));
            }
            else if (taskFilter.FromEndDate.HasValue && !taskFilter.ToEndDate.HasValue)
            {
                Expression = Expression.And(t => taskFilter.FromEndDate <= t.EndDate);
            }
            else if (!taskFilter.FromEndDate.HasValue && taskFilter.ToEndDate.HasValue)
            {
                Expression = Expression.And(t => taskFilter.ToEndDate >= t.EndDate);
            }

            //To Task CreationDate Date
            if (taskFilter.FromCreationDate.HasValue && taskFilter.ToCreationDate.HasValue)
            {
                Expression = Expression.And(t => (taskFilter.FromCreationDate <= t.CreateDate && taskFilter.ToCreationDate >= t.CreateDate));
            }
            else if (taskFilter.FromCreationDate.HasValue && !taskFilter.ToCreationDate.HasValue)
            {
                Expression = Expression.And(t => taskFilter.FromCreationDate <= t.CreateDate);
            }
            else if (!taskFilter.FromCreationDate.HasValue && taskFilter.ToCreationDate.HasValue)
            {
                Expression = Expression.And(t => taskFilter.ToCreationDate >= t.CreateDate);
            }
        }
        return Expression;
    }
}