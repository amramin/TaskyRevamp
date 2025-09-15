using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IRepository<TaskItem> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<TaskItem> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _tskRepository.Delete(request.Id);


        return true;
    }
}