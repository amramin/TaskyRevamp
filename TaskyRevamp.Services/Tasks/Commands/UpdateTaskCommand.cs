using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;


namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskCommand(CreateTaskDto Task) : IRequest<bool>;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly IRepository<TaskItem> _taskRepository;

    public UpdateTaskCommandHandler(IRepository<TaskItem> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var TaskResponse = await _taskRepository.FindByKey(request.Task.Id);
        if (!TaskResponse.Success)
        {
            return false;
        }
        var updated = TaskResponse.Value;
      updated.SetData(request.Task);
        await _taskRepository.Update(updated);

        return true;
    }
}