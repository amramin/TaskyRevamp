using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Query;

public record GetChangeEndDateRequestQuery(Guid Id) : IRequest<ChangeEndDateRequestDto>;

public class GetChangeEndDateRequestByIdHandler : IRequestHandler<GetChangeEndDateRequestQuery, ChangeEndDateRequestDto>
{
    private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;

    public GetChangeEndDateRequestByIdHandler(
        IRepository<Domain.Models.Task.ChangeEndDateRequest> changeEndDateRequestRepository)
    {
        _changeEndDateRequestRepository = changeEndDateRequestRepository;
    }

    public async Task<ChangeEndDateRequestDto> Handle(GetChangeEndDateRequestQuery request,
        CancellationToken cancellationToken)
    {
        var res = await _changeEndDateRequestRepository.FindByKey(request.Id);
        if (res?.Value == null || !res.Success)
            throw new Exception("ChangeEndDateRequest not found");
        var changeEndDateRequestDto = res.Value.CopyToDto();
        return changeEndDateRequestDto;
    }
}