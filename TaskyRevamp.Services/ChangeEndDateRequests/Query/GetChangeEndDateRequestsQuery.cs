using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Query;

public record GetChangeEndDateRequestsQuery(QueryModel? Query) : IRequest<List<ChangeEndDateRequestDto>>;

public class
    GetChangeEndDateRequestsHandler : IRequestHandler<GetChangeEndDateRequestsQuery, List<ChangeEndDateRequestDto>>
{
    private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;


    public GetChangeEndDateRequestsHandler(IRepository<ChangeEndDateRequest> changeEndDateRequestRepository)
    {
        _changeEndDateRequestRepository = changeEndDateRequestRepository;
    }

    public async Task<List<ChangeEndDateRequestDto>> Handle(GetChangeEndDateRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var changeEndDateRequestsDto = new List<ChangeEndDateRequestDto>();


        var changeEndDateRequests = await _changeEndDateRequestRepository.All();


        foreach (var changeEndDateRequest in changeEndDateRequests.Value)
        {
            changeEndDateRequestsDto.Add(changeEndDateRequest.CopyToDto()
            );
        }
        
        return changeEndDateRequestsDto;
    }
}