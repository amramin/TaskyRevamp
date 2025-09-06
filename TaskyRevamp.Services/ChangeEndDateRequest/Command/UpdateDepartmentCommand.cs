using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;



namespace ChangeEndDateRequestyRevamp.Services.ChangeEndDateRequests.Commands;

public record UpdateChangeEndDateRequestCommand(ChangeEndDateRequestDto ChangeEndDateRequest) : IRequest<bool>;

public class UpdateChangeEndDateRequestCommandHandler : IRequestHandler<UpdateChangeEndDateRequestCommand, bool>
{
    private readonly IRepository<ChangeEndDateRequest> _ChangeEndDateRequestRepository;

    public UpdateChangeEndDateRequestCommandHandler(IRepository<ChangeEndDateRequest> ChangeEndDateRequestRepository)
    {
        _ChangeEndDateRequestRepository = ChangeEndDateRequestRepository;
    }

    public async Task<bool> Handle(UpdateChangeEndDateRequestCommand request, CancellationToken cancellationToken)
    {
        var ChangeEndDateRequestResponse = await _ChangeEndDateRequestRepository.FindByKey(request.ChangeEndDateRequest.Id);
        if (!ChangeEndDateRequestResponse.Success)
        {
            return false;
        }
        var updated = ChangeEndDateRequestResponse.Value;
        updated.SetData(request.ChangeEndDateRequest);
            await _ChangeEndDateRequestRepository.Update(updated);

        return true;
    }
}