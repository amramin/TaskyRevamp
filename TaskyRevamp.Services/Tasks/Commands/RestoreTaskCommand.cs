using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands
{
	public record RestoreTaskCommand(Guid taskId, int restoreOption) : IRequest<bool>;
	public class RestoreTaskHandler : IRequestHandler<RestoreTaskCommand, bool>
	{
		private readonly IRepository<TaskItem> _taskRepository;
        private readonly IRepository<TaskAssignee> _taskAssigneeRepository;

        public RestoreTaskHandler(IRepository<TaskItem> taskRepository, IRepository<TaskAssignee> taskAssigneeRepository)
		{
			_taskRepository = taskRepository;
			_taskAssigneeRepository = taskAssigneeRepository;
		}
		public async Task<bool> Handle(RestoreTaskCommand request, CancellationToken cancellationToken)
		{
			var taskRes = await _taskRepository.FindBy(t => t.Id == request.taskId, includeProperties: $"{nameof(TaskItem.CreatedBy)}");
			if(taskRes.Success && taskRes.Value != null)
			{
				var task = taskRes.Value.FirstOrDefault();
				// implement logic to make the task a main task with no subtasks if the parent task is deleted or closed 
				//if (task!.Parent != null)
				//{
				//	var parent= task!.Parent;
				//	if (parent.StatusId == Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C") || 
				//		parent.StatusId == Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C") || parent.IsDeleted)
				//	{
				//		task!.Parent = null;						
				//	}
				//}
				task!.IsDeleted = false;
				if(request.restoreOption == 2)
				{
					var creatorId = task.CreatedBy.Id;
                    var assignee = new TaskAssignee
                    {
                        TaskItemId = task.Id,
                        UserId = task.CreatedById,
                        AssigneeDate = DateTime.Now
                    };
                    await _taskAssigneeRepository.DeleteRang(task.TaskAssignees.Where(p=>p.TaskItemId==task.Id).Select(p=>p.Id).ToList());
                    await _taskAssigneeRepository.Insert(assignee);
                    await _taskAssigneeRepository.SaveChangesAsync();
                    //task.AssignedIds = creatorId != Guid.Empty ? new List<Guid> { creatorId } : new List<Guid>();
                }	
				await _taskRepository.Update(task);
				return true;
			}
			return false;
		}
	}
}
