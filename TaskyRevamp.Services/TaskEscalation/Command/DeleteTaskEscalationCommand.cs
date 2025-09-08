using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskEscalations.Commands;

public record DeleteTaskEscalationCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskEscalationCommand, bool>
{
    private readonly IRepository<TaskEscalation> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<TaskEscalation> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskEscalationCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}