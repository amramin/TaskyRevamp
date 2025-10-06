using MediatR;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskEscalation.Command;

public record DeleteTaskEscalationCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskEscalationCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.TaskEscalation> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<Domain.Models.Task.TaskEscalation> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskEscalationCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}