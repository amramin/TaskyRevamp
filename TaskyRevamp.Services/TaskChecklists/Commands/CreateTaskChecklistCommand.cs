using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;
namespace TaskyRevamp.Services.TaskChecklists.Commands;

public record CreateTaskChecklistCommand(TaskChecklistDto TaskChecklistDto) : IRequest<Guid>;
public class CreateTaskChecklistHandler : IRequestHandler<CreateTaskChecklistCommand, Guid>
{
    private readonly IRepository<TaskChecklist> _taskChecklistRepository;
    public CreateTaskChecklistHandler(IRepository<TaskChecklist> taskChecklistRepository) => _taskChecklistRepository = taskChecklistRepository;
    public async Task<Guid> Handle(CreateTaskChecklistCommand request, CancellationToken cancellationToken)
    {
        var taskChecklist = new TaskChecklist(request.TaskChecklistDto.TaskId, request.TaskChecklistDto.Title);
        await _taskChecklistRepository.Insert(taskChecklist);
        return taskChecklist.Id;
    }
}