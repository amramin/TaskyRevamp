using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskStatusCommand(Guid TaskId, Domain.Models.Task.TaskStatus Status) : IRequest<Unit>;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand,Unit>
{
    private readonly IRepository<TaskItem> _taskRepository;

    public UpdateTaskStatusHandler(IRepository<TaskItem> taskRepository) => _taskRepository = taskRepository;

    public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        //var task = await _taskRepository.GetByIdAsync(request.TaskId)
        //           ?? throw new KeyNotFoundException();

        //task.UpdateStatus(request.Status);
        //await _taskRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
