using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;

namespace TaskyRevamp.Services.TaskChecklists.Commands;

public record UpdateTaskChecklistCommand(TaskChecklistDto TaskChecklist) : IRequest<bool>;

public class UpdateTaskChecklistCommandHandler : IRequestHandler<UpdateTaskChecklistCommand, bool>
{
    private readonly IRepository<TaskChecklist> _taskChecklistRepository;

    public UpdateTaskChecklistCommandHandler(IRepository<TaskChecklist> taskChecklistRepository)
    {
        _taskChecklistRepository = taskChecklistRepository;
    }
    public async Task<bool> Handle(UpdateTaskChecklistCommand request, CancellationToken cancellationToken)
    {
        var taskChecklistResponse = await _taskChecklistRepository.FindByKey(request.TaskChecklist.Id);
        if (!taskChecklistResponse.Success)
            return false;
        var updated = taskChecklistResponse.Value;
        if (updated != null)
        {
            updated.SetData(request.TaskChecklist);
            await _taskChecklistRepository.Update(updated);
            return true;
		}
        return false;
    }
}