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
            if (task.Progress == 100&&task.StatusId== TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Pending review").Id)
            {
                task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Completed").Id;
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
