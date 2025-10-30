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
							includeProperties: $"{nameof(TaskItem.CreatedBy)},{nameof(TaskItem.UpdatedBy)}");


		foreach (var tsk in res.Items)
		{
			CreateTaskDto tasky = tsk.CopyToDto();
			tasky.CreatedByName = currentCulture == "ar" ? tsk.CreatedBy?.NameArabic : tsk.CreatedBy?.NameEnglish;
			tasky.UpdatedBy = currentCulture == "ar" ? tsk.UpdatedBy?.NameArabic : tsk.UpdatedBy?.NameEnglish;
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
			case "TitleEnglish":
				return sortAscending
					? q => q.OrderBy(u => u.TitleEnglish)
					: q => q.OrderByDescending(u => u.TitleEnglish);
			case "TitleArabic":
				return sortAscending
					? q => q.OrderBy(u => u.TitleArabic)
					: q => q.OrderByDescending(u => u.TitleArabic);
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

			case "Level":

				return sortAscending
					? q => q.OrderBy(u => u.Level)
					: q => q.OrderByDescending(u => u.Level);



			default:
				return q => q.OrderBy(u => u.CreateDate);
		}
	}


}