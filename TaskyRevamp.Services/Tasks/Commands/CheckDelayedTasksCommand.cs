using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C"),
                Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C"),
                Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C"),
                Guid.Parse("D8E94CCE-586A-46D3-223D-08DE3318A61C"),
            };
            if (res.Success && res.Value != null)
            {
                var tasks = res.Value.Where(t => t.EndDate < DateTime.UtcNow.Date && !NotAllowDelayStatuses.Any(p => p == t.StatusId)).ToList();
                foreach (var task in tasks)
                {
                    task.StatusId = Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C");
                    await _taskRepository.Update(task);
                    await _taskRepository.SaveChangesAsync();
                }
            }
            return true;
        }
    }
}
