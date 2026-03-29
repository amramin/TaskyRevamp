using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Tasks;
using TaskItems = TaskyRevamp.Domain.Models.Task.TaskItem;

namespace TaskyRevamp.Services.Users.Command
{
	public record IfUserHasOpenTasksOnDepartmentCommand(Guid departmentId, Guid userId) : IRequest<bool>;
	public class IfUserHasOpenTaskHandler : IRequestHandler<IfUserHasOpenTasksOnDepartmentCommand, bool>
	{
		private readonly IRepository<TaskItems> _taskRepository;

		public IfUserHasOpenTaskHandler(IRepository<TaskItems> taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<bool> Handle(IfUserHasOpenTasksOnDepartmentCommand request, CancellationToken cancellationToken)
		{
			List<CreateTaskDto> tasks = new List<CreateTaskDto>();
			var res = await _taskRepository.AllAsNoTracking(includeProperties: $"{nameof(TaskItems.status)}");
			if (res.Success && res.Value != null)
			{
				tasks = res.Value.Select(t => t.ToDto()).ToList();
			}
			foreach (var task in tasks)
			{
				var hasDep = task.AssignedDepartmentIds?.Any(id => id == request.departmentId) ?? false;
				var hasUser = task.AssignedIds?.Any(id => id == request.userId) ?? false;
				var hasOpenStatus = task.TaskStatusName == "In Progress" ||
									task.TaskStatusName == "Reopened" ||
									task.TaskStatusName == "Pending Review";

				if (hasDep && hasUser && hasOpenStatus)
				{
					return true;
				}
			}
			return false;
		}
	}
}
