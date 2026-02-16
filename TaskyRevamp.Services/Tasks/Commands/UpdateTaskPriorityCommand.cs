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
    public record UpdateTaskPriorityCommand(Guid TaskId, Guid PriorityId) : IRequest<bool>;
    public class UpdateTaskPriorityHandler : IRequestHandler<UpdateTaskPriorityCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<PrioritySettings> _prioritySettings;
        private readonly IRepository<TaskItem> _taskRepo;

        public UpdateTaskPriorityHandler(ITaskRepository taskRepository, IRepository<TaskItem> repository, IRepository<PrioritySettings> prioritySettings)
        {
            _taskRepository = taskRepository;
            _prioritySettings = prioritySettings;
            _taskRepo = repository;
        }

        public async Task<bool> Handle(UpdateTaskPriorityCommand request, CancellationToken cancellationToken)
        {
            var res = await _taskRepository.GetTaskById(request.TaskId);
            var TaskPriorities = await _prioritySettings.All();
            if (res is not null)
            {
                var task = res;
                if (TaskPriorities.Value is null||!TaskPriorities.Value.Any(p => p.Id == request.PriorityId))
                {
                   throw new Exception("No Priorities");
                }
                task.PriorityId = request.PriorityId;
                await _taskRepository.UpdateTask(task);
            }
            return true;
        }
    }
}
