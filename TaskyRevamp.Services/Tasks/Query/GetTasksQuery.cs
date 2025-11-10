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

    public GetTasksHandler(IRepository<TaskItem> taskRepository, IRepository<User> userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
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
                            includeProperties: $"{nameof(TaskItem.CreatedBy)},{nameof(TaskItem.ActualWeight)},{nameof(TaskItem.Type)},{nameof(TaskItem.Source)},{nameof(TaskItem.UpdatedBy)},{nameof(TaskItem.Assignees)}.{nameof(TaskyRevamp.Domain.Models.Task.TaskAssignees.User)}");

        foreach (var tsk in res.Items)
        {
            var assgnedusr = await _userRepository.FindBy(k => tsk.AssignedIds.Contains(k.Id));

            CreateTaskDto tasky = tsk.CopyToDto();
            tasky.TypeName = tsk.Type?.Name;
            tasky.SourceName = tsk.Source?.Name;

            tasky.CreatedByName = currentCulture == "ar" ? tsk.CreatedBy?.NameArabic : tsk.CreatedBy?.NameEnglish;
            tasky.UpdatedBy = currentCulture == "ar" ? tsk.UpdatedBy?.NameArabic : tsk.UpdatedBy?.NameEnglish;
            tasky.AssigneduserNames = string.Join(",", assgnedusr.Value.Select(k => k.Username));// string.Join(", ", tsk.Assignees.Select(k => k.User.NameEnglish));
            if (tasky.AssigneduserNames.Count() > 0)
            {
                var initials = string.Join(", ",

                    tasky.AssigneduserNames
            .Split(',', StringSplitOptions.RemoveEmptyEntries) // split users
            .Select(u =>
            {
                var parts = u.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    return $"{parts[0][0]}{parts[1][0]}"; // first letter of first and last name
                }
                else if (parts.Length == 1)
                {
                    return $"{parts[0][0]}"; // only first name exists
                }
                else
                {
                    return string.Empty;
                }
            })
            .Where(x => !string.IsNullOrEmpty(x)) // remove empty
    );

                tasky.AssigneduserNames = initials;
            }
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
            case "CreateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreateDate)
                    : q => q.OrderByDescending(u => u.CreateDate);
            case "Title":
                return sortAscending
                    ? q => q.OrderBy(u => currentCulture == "ar" ? u.TitleArabic : u.TitleEnglish)
                    : q => q.OrderByDescending(u => currentCulture == "ar" ? u.TitleArabic : u.TitleEnglish);
            case "Priority":
                return sortAscending
                    ? q => q.OrderBy(u => u.Priority)
                    : q => q.OrderByDescending(u => u.Priority);
            case "UpdateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdateDate)
                    : q => q.OrderByDescending(u => u.UpdateDate);

            case "CreatedBy":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish);

            case "UpdatedBy":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);

            case "TaskStatus":

                return sortAscending
                    ? q => q.OrderBy(u => u.Status)
                    : q => q.OrderByDescending(u => u.Status);
            case "StartDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.StartDate)
                    : q => q.OrderByDescending(u => u.StartDate);
            case "EndDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.EndDate)
                    : q => q.OrderByDescending(u => u.EndDate);
            case "weight":
                return sortAscending
                    ? q => q.OrderBy(u => u.ActualWeight)
                    : q => q.OrderByDescending(u => u.ActualWeight);

            default:
                return q => q.OrderBy(u => u.CreateDate);
        }
    }


}