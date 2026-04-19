using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Interfaces.Services;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record ChangeTaskProgressCommand(Guid TaskId, int Progress) : IRequest<bool>;
    public class ChangeTaskProgressHandler : IRequestHandler<ChangeTaskProgressCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStatusDeterminer _statusDeterminer;

        public ChangeTaskProgressHandler(ITaskRepository taskRepository, ITaskStatusDeterminer statusDeterminer)
        {
            _taskRepository = taskRepository;
            _statusDeterminer = statusDeterminer;
        }

        public async Task<bool> Handle(ChangeTaskProgressCommand request, CancellationToken cancellationToken)
        {
            var res = await _taskRepository.GetTaskById(request.TaskId);
            if (res is not null)
            {
                var task = res;
                if (task.Progress != request.Progress)
                {
                    task.Progress = request.Progress;

                    if (task.Progress == 100)
                    {
                        var dependencyIds = task.Dependencies?.Select(p => p.DependentId).ToList() ?? new List<Guid>();
                        if (!await _statusDeterminer.AreDependenciesCompleted(dependencyIds))
                        {
                            return false;
                        }
                    }

                    task.StatusId = _statusDeterminer.DetermineStatus(
                        task.Progress,
                        task.StartDate,
                        task.EndDate,
                        task.StatusId);

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
