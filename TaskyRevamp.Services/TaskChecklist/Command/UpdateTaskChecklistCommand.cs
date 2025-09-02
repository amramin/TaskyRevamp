using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;



namespace TaskChecklistyRevamp.Services.TaskChecklists.Commands;

public record UpdateTaskChecklistCommand(TaskChecklistDto TaskChecklist) : IRequest<bool>;

public class UpdateTaskChecklistCommandHandler : IRequestHandler<UpdateTaskChecklistCommand, bool>
{
    private readonly IRepository<TaskChecklist> _TaskChecklistRepository;

    public UpdateTaskChecklistCommandHandler(IRepository<TaskChecklist> TaskChecklistRepository)
    {
        _TaskChecklistRepository = TaskChecklistRepository;
    }

    public async Task<bool> Handle(UpdateTaskChecklistCommand request, CancellationToken cancellationToken)
    {
        var TaskChecklistResponse = await _TaskChecklistRepository.FindByKey(request.TaskChecklist.Id);
        if (!TaskChecklistResponse.Success)
        {
            return false;
        }
        var updated = TaskChecklistResponse.Value;
      updated.SetData(request.TaskChecklist);
        await _TaskChecklistRepository.Update(updated);

        return true;
    }
}