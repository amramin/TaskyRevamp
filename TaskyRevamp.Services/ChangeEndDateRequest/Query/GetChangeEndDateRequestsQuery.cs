
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.GeneralDto;


namespace ChangeEndDateRequestyRevamp.Services.ChangeEndDateRequests.Query;

public record GetChangeEndDateRequestsQuery(QueryModel? Query) : IRequest<List<ChangeEndDateRequestDto>>;

public class GetChangeEndDateRequestsHandler : IRequestHandler<GetChangeEndDateRequestsQuery, List<ChangeEndDateRequestDto>>
{
    private readonly IRepository<ChangeEndDateRequest> _ChangeEndDateRequestRepository;


    public GetChangeEndDateRequestsHandler(IRepository<ChangeEndDateRequest> ChangeEndDateRequestRepository)
    {
        _ChangeEndDateRequestRepository = ChangeEndDateRequestRepository;
      
    }

    public async Task<List<ChangeEndDateRequestDto>> Handle(GetChangeEndDateRequestsQuery request, CancellationToken cancellationToken)
    {
        List<ChangeEndDateRequestDto> ChangeEndDateRequestss = new List<ChangeEndDateRequestDto>();
       

        var data = await _ChangeEndDateRequestRepository.All();



        foreach (var ChangeEndDateRequest in data.Value)
        {
        ;
            ChangeEndDateRequestss.Add(ChangeEndDateRequest.CopyToDto()
            );
        }

       // return ChangeEndDateRequests.ToList();

        return  ChangeEndDateRequestss;
    }

   
}