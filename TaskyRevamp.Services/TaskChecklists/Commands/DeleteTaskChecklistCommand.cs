using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskChecklists.Commands;

public record DeleteTaskChecklistCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskChecklistCommand, bool>
{
    private readonly IRepository<TaskChecklist> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<TaskChecklist> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskChecklistCommand request, CancellationToken cancellationToken)
    {
        await _tskRepository.Delete(request.Id);


        return true;
    }
}