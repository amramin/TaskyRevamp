
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.PinnedTasks;

namespace TaskyRevamp.Services.PinnedTasks.Query;

public record GetPinnedTasksQuery(QueryModel? Query) : IRequest<List<PinnedTasksDto>>;

public class GetPinnedTaskssHandler : IRequestHandler<GetPinnedTasksQuery, List<PinnedTasksDto>>
{
    private readonly IRepository<Domain.Models.Task.PinnedTasks> _pinnedTasksRepository;


    public GetPinnedTaskssHandler(IRepository<Domain.Models.Task.PinnedTasks> pinnedTasksRepository)
    {
        _pinnedTasksRepository = pinnedTasksRepository;
    }

    public async Task<List<PinnedTasksDto>> Handle(GetPinnedTasksQuery request, CancellationToken cancellationToken)
    {
        var pinnedTasksDtos = new List<PinnedTasksDto>();
        var pinnedTasks = await _pinnedTasksRepository.All();
        
        foreach (var pinnedTask in   pinnedTasks?.Value??[])
        {
            pinnedTasksDtos.Add(pinnedTask.CopyToDto());
        }
        
        return pinnedTasksDtos;
    }
}