using MediatR;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.ChecklistItems.Command;

public record DeleteChecklistItemCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteChecklistItemCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.ChecklistItem> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<Domain.Models.Task.ChecklistItem> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteChecklistItemCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}