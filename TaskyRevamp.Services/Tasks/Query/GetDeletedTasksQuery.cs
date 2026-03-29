using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using MailKit.Search;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.Tasks.Query
{
	public record GetDeletedTasksQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDeletedTask> SearchFields, string SearchText) : IRequest<PagedResult<CreateTaskDto>>;
	public class GetDeletedTasksHandler : IRequestHandler<GetDeletedTasksQuery, PagedResult<CreateTaskDto>>
	{
		private readonly IRepository<TaskItem> _taskRepository;
		string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		public GetDeletedTasksHandler(IRepository<TaskItem> taskRepository)
		{
			_taskRepository = taskRepository;
		}
		public async Task<PagedResult<CreateTaskDto>> Handle(GetDeletedTasksQuery request, CancellationToken cancellationToken)
		{
			List<CreateTaskDto> alltasks = new List<CreateTaskDto>();
			var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);
			Expression<Func<TaskItem, bool>> searchExpression = null;
			if (request.SearchFields != null && request.SearchFields.Any())
			{
				var map = DeletedTaskSearchFieldMap.Map(currentCulture);
				var predicates = request.SearchFields.Select(x => map[x]).ToList();
				searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
			}
			var res = await _taskRepository.GetPagedAsync(
								request.pageNumber,
								request.pageSize,
								t => t.IsDeleted,
								searchExpression,
								orderBy: orderBy,
								includeProperties: $"{nameof(TaskItem.DeletedBy)},{nameof(TaskItem.Priority)}");
			foreach (var item in res.Items)
			{
				var task = item.ToDto();
				task.PriorityName = currentCulture == "ar" ? item.Priority?.NameArabic ?? "" : item.Priority?.NameEnglish ?? "";
				task.PriorityBackgroundColor = item.Priority?.BackgroundColor;
				task.PriorityColor = item.Priority?.NameColor;
				task.DeletedByName =  item.DeletedBy != null ? (currentCulture == "ar" ? item.DeletedBy.NameArabic : item.DeletedBy.NameEnglish) : null;
				alltasks.Add(task);
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
				case "Title":
					return sortAscending
						? q => q.OrderBy(u => currentCulture == "ar" ? u.Title : u.Title)
						: q => q.OrderByDescending(u => currentCulture == "ar" ? u.Title : u.Title);
				case "Priority":
					if (currentCulture == "ar")
					{
						return sortAscending
						? q => q.OrderBy(u => u.Priority.NameArabic)
						: q => q.OrderByDescending(u => u.Priority.NameArabic);
					}
					else
					{
						return sortAscending
						? q => q.OrderBy(u => u.Priority.NameEnglish)
						: q => q.OrderByDescending(u => u.Priority.NameEnglish);
					}
				case "Deleted By":
					if (currentCulture == "ar")
					{
						return sortAscending
							? q => q.OrderBy(u => u.DeletedBy!.NameArabic)
							: q => q.OrderByDescending(u => u.DeletedBy!.NameArabic);
					}
					else
					{
						return sortAscending
							? q => q.OrderBy(u => u.DeletedBy!.NameEnglish)
							: q => q.OrderByDescending(u => u.DeletedBy!.NameEnglish);
					}
				case "Deletion Date":
					return sortAscending
						? q => q.OrderBy(u => u.DeleteDate)
						: q => q.OrderByDescending(u => u.DeleteDate);
				case "Start Date":
					return sortAscending
						? q => q.OrderBy(u => u.StartDate)
						: q => q.OrderByDescending(u => u.StartDate);
				case "End Date":
					return sortAscending
						? q => q.OrderBy(u => u.EndDate)
						: q => q.OrderByDescending(u => u.EndDate);
				default:
					return q => q.OrderBy(u => u.StartDate);
			}
		}
	}
}
