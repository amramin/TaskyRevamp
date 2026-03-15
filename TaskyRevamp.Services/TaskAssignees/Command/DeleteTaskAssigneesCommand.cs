using MediatR;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskAssignees.Command;

public record DeleteTaskAssigneesCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskAssigneesCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.TaskAssignee> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<Domain.Models.Task.TaskAssignee> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskAssigneesCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}