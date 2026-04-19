using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;
using TaskItems = TaskyRevamp.Domain.Models.Task.TaskItem;

namespace TaskyRevamp.Services.Tasks.Commands
{
	public record CheckOpenedTaskForUserCommand(Guid userId) : IRequest<bool>;
	public class CheckOpenedTaskForUserHandler : IRequestHandler<CheckOpenedTaskForUserCommand, bool>
	{
		private readonly IRepository<TaskItems> _taskRepository;

		public CheckOpenedTaskForUserHandler(IRepository<TaskItems> taskRepository)
		{
			_taskRepository = taskRepository;
		}
		public async Task<bool> Handle(CheckOpenedTaskForUserCommand request, CancellationToken cancellationToken)
		{
			List<CreateTaskDto> tasks = new List<CreateTaskDto>();
			var res = await _taskRepository.AllAsNoTracking(includeProperties: $"{nameof(TaskItems.status)}");
			if (res.Success && res.Value != null)
			{
				tasks = res.Value.Select(t => t.ToDto()).ToList();
			}
			foreach (var task in tasks)
			{
				var hasUser = task.AssignedIds?.Any(id => id == request.userId) ?? false;
				var hasOpenStatus = task.TaskStatusName == "In Progress" ||
									task.TaskStatusName == "Reopened" ||
									task.TaskStatusName == "Pending Review";

				if (hasUser && hasOpenStatus)
				{
					return true;
				}
			}
			return false;
		}
	}
}
