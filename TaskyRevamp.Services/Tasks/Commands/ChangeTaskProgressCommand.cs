using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record ChangeTaskProgressCommand(Guid TaskId, int Progress) : IRequest<bool>;
    public class ChangeTaskProgressHandler : IRequestHandler<ChangeTaskProgressCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<StatusSettings> _statusSettings;
        private readonly IRepository<TaskItem> _taskRepo;

        public ChangeTaskProgressHandler(ITaskRepository taskRepository, IRepository<TaskItem> repository, IRepository<StatusSettings> statusSettings)
        {
            _taskRepository = taskRepository;
            _statusSettings = statusSettings;
            _taskRepo = repository;
        }

        public async Task<bool> Handle(ChangeTaskProgressCommand request, CancellationToken cancellationToken)
        {
            var res = await _taskRepository.GetTaskById(request.TaskId);
            var TaskSatuses = await _statusSettings.All();
            if (res is not null)
            {
                var task = res;
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
                            if (task.StatusId !=Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C"))
                            {
                                task.StatusId = Guid.Parse("753404A6-8B18-43F7-2238-08DE3318A61C");
                            }
                        }
                        else if (task.Progress == 100)
                        {
                            var dependencies = task.Dependencies?.Select(p => p.DependentId).ToList();
                            var tasksnotcompleted = await _taskRepo.FindBy(d => dependencies.Contains(d.Id));
                            if (tasksnotcompleted.Value.Any(p => p.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C")))
                            {
                                return false;
                            }
                            else
                            {
                                task.StatusId = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");
                            }
                        }
                    }
                    await _taskRepository.UpdateTask(task);
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
