using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskyRevamp.Infrastructure;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
	private readonly EfDbContext _context;

	public TaskRepository(EfDbContext context)
	{
		_context = context;
	}


	public async Task<List<TaskItem>> GetTasks(PagingParameterModel? paging, Expression<Func<TaskItem, object>> sortBy,
		string sortOrder, Expression<Func<TaskItem, bool>>? whereExp = null)

	{
		var tasks = _context.Set<TaskItem>().AsQueryable();
		if (whereExp != null)
			tasks = tasks.Where(whereExp).AsQueryable();
		if (!string.IsNullOrEmpty(sortOrder))
		{
			tasks = sortOrder.ToLower() == "desc" ? tasks.OrderByDescending(sortBy) : tasks.OrderBy(sortBy);
		}

		if (paging != null)
		{
			paging.Total = await tasks.CountAsync();
			tasks = tasks.Skip(paging.Page * paging.PageSize).Take(paging.PageSize);
		}

		tasks = AddIncludeToTasks(tasks);

		var taskList = await tasks.ToListAsync();

		return taskList;
	}

	private static IQueryable<TaskItem> AddIncludeToTasks(IQueryable<TaskItem> tasks)
	{
		tasks = tasks.Include(x => x.CreatedBy).ThenInclude(c => c.Department)
			.Include(x => x.UpdatedBy)
			.Include(x => x.Priority)
			.Include(x => x.Type)
			.Include(x => x.Source)
			.Include(x => x.status)
			.Include(x => x.Assignees).ThenInclude(a => a.User)
			.Include(x => x.Checklist)
			.Include(x => x.Subtasks)
			.Include(x => x.Attachments)
			.Include(x => x.History)
			.Include(x => x.Dependencies).ThenInclude(d => d!.Items)
			.Include(x => x.ChangeRequests)
			.Include(x => x.Escalations);
		return tasks;
	}

	public async Task<TaskItem?> GetTaskById(Guid id)
	{
		var task = _context.Set<TaskItem>().AsQueryable();
		//task = AddIncludeToTasks(task);
		var taskItem = await task.FirstOrDefaultAsync(x => x.Id == id);
		return taskItem;
	}

	public async Task<TaskItem?> UpdateTask(TaskItem task)
	{
		_context.Set<TaskItem>().Update(task);
		await _context.SaveChangesAsync();
		return task;
	}
}