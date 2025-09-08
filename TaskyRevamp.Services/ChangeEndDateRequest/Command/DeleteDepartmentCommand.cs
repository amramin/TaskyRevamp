using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Commands;

public record DeleteChangeEndDateRequestCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteChangeEndDateRequestCommand, bool>
{
    private readonly IRepository<ChangeEndDateRequest> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<ChangeEndDateRequest> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteChangeEndDateRequestCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}