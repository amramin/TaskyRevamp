using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Commands;


namespace ChangeEndDateRequestyRevamp.Services.ChangeEndDateRequests.Commands;

public record UpdateChangeEndDateRequestCommand(ChangeEndDateRequestDto ChangeEndDateRequest) : IRequest<bool>;

public class UpdateChangeEndDateRequestCommandHandler : IRequestHandler<UpdateChangeEndDateRequestCommand, bool>
{
    private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;

    public UpdateChangeEndDateRequestCommandHandler(IRepository<ChangeEndDateRequest> changeEndDateRequestRepository)
    {
        _changeEndDateRequestRepository = changeEndDateRequestRepository;
    }

    public async Task<bool> Handle(UpdateChangeEndDateRequestCommand request, CancellationToken cancellationToken)
    {
        var changeEndDateRequestResponse =
            await _changeEndDateRequestRepository.FindByKey(request.ChangeEndDateRequest.Id);
        if (!changeEndDateRequestResponse.Success)
        {
            return false;
        }

        if (changeEndDateRequestResponse.Value is null)
        {
            throw new Exception("ChangeEndDateRequest not found");
        }


        var changeEndDateRequest = changeEndDateRequestResponse.Value;
        changeEndDateRequest.SetData(request.ChangeEndDateRequest);
        await _changeEndDateRequestRepository.Update(changeEndDateRequest);

        return true;
    }
}