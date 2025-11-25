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
	public record UpdateTasksDepartmentCommand(Guid OldDepartmentId, Guid NewDepartmentId) : IRequest<bool>;
	public class UpdateTasksDepartmentHandler : IRequestHandler<UpdateTasksDepartmentCommand, bool>
	{
		private readonly IRepository<TaskItems> _taskRepository;

		public UpdateTasksDepartmentHandler(IRepository<TaskItems> taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<bool> Handle(UpdateTasksDepartmentCommand request, CancellationToken cancellationToken)
		{
			var res = await _taskRepository.AllAsNoTracking();
			if (res.Success && res.Value != null)
			{
				var tasks = res.Value.Where(u => u.AssignedDepartmentIds.Contains(request.OldDepartmentId)).ToList();
				foreach (var task in tasks)
				{
					task.AssignedDepartmentIds.Remove(request.OldDepartmentId);
					if (!task.AssignedDepartmentIds.Contains(request.NewDepartmentId))
						task.AssignedDepartmentIds.Add(request.NewDepartmentId);
					await _taskRepository.Update(task);
					await _taskRepository.SaveChangesAsync();
				}
			}
			return true;
		}
	}
}
