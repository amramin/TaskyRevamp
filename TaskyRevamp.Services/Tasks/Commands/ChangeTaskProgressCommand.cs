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
    public record ChangeTaskProgressCommand(Guid TaskId, int Progress) : IRequest<bool>;
    public class ChangeTaskProgressHandler : IRequestHandler<ChangeTaskProgressCommand, bool>
    {
        private readonly IRepository<TaskItem> _taskRepository;
        private readonly IRepository<StatusSettings> _statusSettings;

        public ChangeTaskProgressHandler(IRepository<TaskItem> taskRepository, IRepository<StatusSettings> statusSettings)
        {
            _taskRepository = taskRepository;
            _statusSettings = statusSettings;
        }

        public async Task<bool> Handle(ChangeTaskProgressCommand request, CancellationToken cancellationToken)
        {
            var res = await _taskRepository.FindByKey(request.TaskId);
            var TaskSatuses = await _statusSettings.All();
            if (res.Success && res.Value != null)
            {
                var task = res.Value;
                if (task.Progress != request.Progress)
                {
                    task.Progress = request.Progress;
                    if (TaskSatuses is not null)
                    {
                        if (task.Progress == 0 && (task.StartDate > DateTime.Now))
                        {
                            task.StatusId = Guid.Parse("547022EA-EF8C-4FBC-2236-08DE3318A61C");
                        }
                        else if (task.Progress == 0 && (task.StartDate <= DateTime.Now))
                        {
                            task.StatusId = Guid.Parse("9843AF9D-1389-4740-B428-08DE3D8A77AB");
                        }
                        else if (task.Progress > 0 && task.Progress < 100)
                        {
                            task.StatusId = Guid.Parse("753404A6-8B18-43F7-2238-08DE3318A61C");
                        }
                        else if (task.Progress == 100)
                        {
                            task.StatusId = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");
                        }
                    }
                    await _taskRepository.Update(task);
                    await _taskRepository.SaveChangesAsync();
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
    }
}
