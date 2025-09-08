using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskComments.Commands;

public record DeleteTaskCommentCommand(Guid Id) : IRequest<bool>;

public class DeleteCommandHandler : IRequestHandler<DeleteTaskCommentCommand, bool>
{
    private readonly IRepository<TaskComment> _tskRepository;

    public DeleteCommandHandler(IRepository<TaskComment> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}