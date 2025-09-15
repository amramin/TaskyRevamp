using MediatR;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.TaskComment.Command;

public record DeleteTaskCommentCommand(Guid Id) : IRequest<bool>;

public class DeleteCommandHandler : IRequestHandler<DeleteTaskCommentCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.TaskComment> _tskRepository;

    public DeleteCommandHandler(IRepository<Domain.Models.Task.TaskComment> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}