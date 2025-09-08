using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskAssigneess.Commands;

public record DeleteTaskAssigneesCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskAssigneesCommand, bool>
{
    private readonly IRepository<TaskAssignees> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<TaskAssignees> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskAssigneesCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}