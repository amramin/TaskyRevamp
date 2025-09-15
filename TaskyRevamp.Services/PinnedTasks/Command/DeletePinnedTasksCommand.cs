using MediatR;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.PinnedTasks.Command;

public record DeletePinnedTasksCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeletePinnedTasksCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.PinnedTasks> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<Domain.Models.Task.PinnedTasks> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeletePinnedTasksCommand request, CancellationToken cancellationToken)
    {
        await _tskRepository.Delete(request.Id);
        
        return true;
    }
}