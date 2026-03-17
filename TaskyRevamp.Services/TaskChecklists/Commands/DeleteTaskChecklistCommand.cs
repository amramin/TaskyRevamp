using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
namespace TaskyRevamp.Services.TaskChecklists.Commands;

public record DeleteTaskChecklistCommand(Guid Id) : IRequest<bool>;
public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskChecklistCommand, bool>
{
    private readonly IRepository<TaskChecklist> _taskChecklistRepository;
    public DeleteGroupCommandHandler(IRepository<TaskChecklist> taskChecklistRepository)
    {
		_taskChecklistRepository = taskChecklistRepository;
    }
    public async Task<bool> Handle(DeleteTaskChecklistCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new Exception("Invalid TaskChecklist Id");

        var taskChecklist = await _taskChecklistRepository.FindBy(tc => tc.Id == request.Id, includeProperties: $"{nameof(TaskChecklist.items)}");
        if (taskChecklist == null || taskChecklist.Value == null || taskChecklist.Value.FirstOrDefault() == null)
            throw new Exception("Task Checklist not found");

        var taskChecklistItems = taskChecklist.Value.FirstOrDefault()!.items;
		if (taskChecklistItems.Any())
            return false;
		await _taskChecklistRepository.Delete(request.Id);
        return true;
    }
}