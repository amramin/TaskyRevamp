using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Services.Tasks;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;
using TaskItemss = TaskyRevamp.Domain.Models.Task.TaskItem;

namespace TaskyRevamp.Services.TaskDependency.Command
{
	public record GetDependentTasksQuery(Guid taskId, int pageNumber, int pageSize, string sortByColumnName, bool sortAscending) : IRequest<PagedResult<CreateTaskDto>>;
	public class GetDependentTasksHandler : IRequestHandler<GetDependentTasksQuery, PagedResult<CreateTaskDto>>
	{
		private string _currentLanguage;
		private readonly IRepository<TaskItemss> _taskRepository;
		private readonly IRepository<TaskDependencies> _taskDependincesRepository;
		private readonly IRepository<User> _userRepository;

		public GetDependentTasksHandler(IRepository<TaskItemss> taskRepository, IRepository<TaskDependencies> taskDependincesRepository, IRepository<User> userRepository)
		{
			_currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; ;
			_taskRepository = taskRepository;
			_taskDependincesRepository = taskDependincesRepository;
			_userRepository = userRepository;
		}
		public async Task<PagedResult<CreateTaskDto>> Handle(GetDependentTasksQuery request, CancellationToken cancellationToken)
		{
			var DependentTassk = new PagedResult<CreateTaskDto>();
			var dependencies = await _taskDependincesRepository.FindBy(d => d.DependentId == request.taskId);
			if (dependencies?.Value == null || !dependencies.Value.Any())
				return DependentTassk;
			var taskIds = dependencies.Value.Select(d => d.TaskItemId).ToList();
			var tasks = await _taskRepository.GetPagedAsync(
				request.pageNumber,
				request.pageSize,
				t => taskIds.Contains(t.Id),
				null,
				orderBy: GetOrderBy(request.sortByColumnName, request.sortAscending),
				includeProperties: $"{nameof(TaskItemss.Priority)}," +
								   $"{nameof(TaskItemss.status)}," +
								   $"{nameof(TaskItemss.TaskAssignees)}.{nameof(TaskyRevamp.Domain.Models.Task.TaskAssignee.User)}"
			);
			if (tasks?.Items == null || !tasks.Items.Any())
				return DependentTassk;
			var dtoList = new List<CreateTaskDto>();
			foreach (var task in tasks.Items)
			{
				//var assgnedusr = await _userRepository.FindBy(k => task.AssignedIds.Contains(k.Id));
				var dto = task.ToDto();
				dto.PriorityName = _currentLanguage == "ar" ? task.Priority?.NameArabic : task.Priority?.NameEnglish;
				dto.PriorityBackgroundColor = task.Priority?.BackgroundColor;
				dto.PriorityColor = task.Priority?.NameColor;
				dto.TaskStatusName = _currentLanguage == "ar" ? task.status?.NameArabic : task.status?.NameEnglish;
				dto.TaskStatusBackgroundColor = task.status?.BackgroundColor;
				dto.TaskStatusColor = task.status?.NameColor;
				//dto.AssigneduserNames = string.Join(",", assgnedusr.Value!.Select(u => _currentLanguage == "ar" ? u.NameArabic : u.NameEnglish));
				dtoList.Add(dto);
			}
			DependentTassk.Items = dtoList;
			DependentTassk.TotalCount = tasks.TotalCount;
			DependentTassk.PageNumber = tasks.PageNumber;
			DependentTassk.PageSize = tasks.PageSize;
			return DependentTassk;
		}
		private Func<IQueryable<TaskItem>, IOrderedQueryable<TaskItem>> GetOrderBy(string sortByColumn, bool sortAscending)
		{
			string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			switch (sortByColumn)
			{
				case "Title":
					return sortAscending
						? q => q.OrderBy(u => currentCulture == "ar" ? u.Title : u.Title)
						: q => q.OrderByDescending(u => currentCulture == "ar" ? u.Title : u.Title);
				case "Priority":
					return sortAscending
						? q => q.OrderBy(u => u.Priority)
						: q => q.OrderByDescending(u => u.Priority);
				case "Status":
					return sortAscending
						? q => q.OrderBy(u => u.status!.NameEnglish)
						: q => q.OrderByDescending(u => u.status!.NameEnglish);
				case "End Date":
					return sortAscending
						? q => q.OrderBy(u => u.EndDate)
						: q => q.OrderByDescending(u => u.EndDate);
				case "Actual progress":
					return sortAscending
						? q => q.OrderBy(u => u.Progress)
						: q => q.OrderByDescending(u => u.Progress);
				default:
					return q => q.OrderBy(u => u.EndDate);
			}
		}
	}
}
