using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record CheckDelayedTasksCommand : IRequest<bool>;
    public class CheckDelayedTasksHandler : IRequestHandler<CheckDelayedTasksCommand, bool>
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public CheckDelayedTasksHandler(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(CheckDelayedTasksCommand request, CancellationToken cancellationToken)
        {
            var res = await _taskRepository.AllAsNoTracking();
            List<Guid> NotAllowDelayStatuses = new List<Guid>()
            {
                TaskStatusConstants.Delayed,
                TaskStatusConstants.Done,
                TaskStatusConstants.Reopened,
                TaskStatusConstants.Completed,
                TaskStatusConstants.Deleted,
            };
            if (res.Success && res.Value != null)
            {
                var tasks = res.Value.Where(t => DateOnly.FromDateTime(t.EndDate?.Date??default) < DateOnly.FromDateTime(DateTime.UtcNow.Date) && !NotAllowDelayStatuses.Any(p => p == t.StatusId)).ToList();
                foreach (var task in tasks)
                {
                    task.StatusId = TaskStatusConstants.Delayed;
                    await _taskRepository.Update(task);
                    await _taskRepository.SaveChangesAsync();
                }
            }
            return true;
        }
    }
}
