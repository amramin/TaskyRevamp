
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;
using TaskyRevamp.Dto.GeneralDto;


namespace PinnedTasksyRevamp.Services.PinnedTaskss.Query;

public record GetPinnedTaskssQuery(QueryModel? Query) : IRequest<List<PinnedTasksDto>>;

public class GetPinnedTaskssHandler : IRequestHandler<GetPinnedTaskssQuery, List<PinnedTasksDto>>
{
    private readonly IRepository<PinnedTasks> _PinnedTasksRepository;


    public GetPinnedTaskssHandler(IRepository<PinnedTasks> PinnedTasksRepository)
    {
        _PinnedTasksRepository = PinnedTasksRepository;
      
    }

    public async Task<List<PinnedTasksDto>> Handle(GetPinnedTaskssQuery request, CancellationToken cancellationToken)
    {
        List<PinnedTasksDto> PinnedTasksss = new List<PinnedTasksDto>();
       

        var data = await _PinnedTasksRepository.All();



        foreach (var PinnedTasks in data.Value)
        {
        ;
            PinnedTasksss.Add(PinnedTasks.CopyToDto());
        }

       // return PinnedTaskss.ToList();

        return  PinnedTasksss;
    }

   
}