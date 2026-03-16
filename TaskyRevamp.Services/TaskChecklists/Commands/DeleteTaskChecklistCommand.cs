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
        await _taskChecklistRepository.Delete(request.Id);
        return true;
    }
}