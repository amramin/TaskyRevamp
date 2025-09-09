
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;


namespace ChangeEndDateRequestyRevamp.Services.ChangeEndDateRequests.Query;

public record GetChangeEndDateRequestQuery(Guid Id) : IRequest<ChangeEndDateRequestDto>;

public class GetChangeEndDateRequestByIdHandler : IRequestHandler<GetChangeEndDateRequestQuery, ChangeEndDateRequestDto>
{
    private readonly IRepository<ChangeEndDateRequest> _ChangeEndDateRequestRepository;

    public GetChangeEndDateRequestByIdHandler(IRepository<ChangeEndDateRequest> ChangeEndDateRequestRepository)
    {
        _ChangeEndDateRequestRepository = ChangeEndDateRequestRepository;
    }

    public async Task<ChangeEndDateRequestDto> Handle(GetChangeEndDateRequestQuery request, CancellationToken cancellationToken)
    {
        var res = await _ChangeEndDateRequestRepository.FindByKey(request.Id);
        ChangeEndDateRequestDto ChangeEndDateRequestModel = res.Value.CopyToDto();
        return ChangeEndDateRequestModel;
    }

 
}