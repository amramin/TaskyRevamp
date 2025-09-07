using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.PinnedTaskss.Commands;

public record DeletePinnedTasksCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeletePinnedTasksCommand, bool>
{
    private readonly IRepository<PinnedTasks> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<PinnedTasks> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeletePinnedTasksCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}