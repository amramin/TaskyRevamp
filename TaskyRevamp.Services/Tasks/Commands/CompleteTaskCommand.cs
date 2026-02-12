using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record CompleteTaskCommand(Guid id) : IRequest<bool>;

    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<StatusSettings> _statusSettings;

        public CompleteTaskCommandHandler(ITaskRepository taskRepository, IRepository<StatusSettings> statusSettings)
        {
            _taskRepository = taskRepository;
            _statusSettings = statusSettings;
        }
        public async Task<bool> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskById(request.id);
            var TaskSatuses = await _statusSettings.All();
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            if (task.Progress == 100&&task.StatusId== Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C"))
            {
                task.StatusId = Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C");
                await _taskRepository.UpdateTask(task);
            }
            else
            {
                return false;
            }
                return true;
        }
    }
}
