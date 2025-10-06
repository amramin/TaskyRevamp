using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskChecklist;

namespace TaskyRevamp.Services.TaskChecklists.Query;

public record GetTaskChecklistsQuery(QueryModel? Query) : IRequest<List<TaskChecklistDto>>;

public class GetTaskChecklistsHandler : IRequestHandler<GetTaskChecklistsQuery, List<TaskChecklistDto>>
{
    private readonly IRepository<TaskChecklist> _taskChecklistRepository;


    public GetTaskChecklistsHandler(IRepository<TaskChecklist> taskChecklistRepository)
    {
        _taskChecklistRepository = taskChecklistRepository;
    }

    public async Task<List<TaskChecklistDto>> Handle(GetTaskChecklistsQuery request,
        CancellationToken cancellationToken)
    {
        var taskChecklistss = new List<TaskChecklistDto>();


        var data = await _taskChecklistRepository.All();


        foreach (var taskChecklist in data.Value)
        {
            taskChecklistss.Add(taskChecklist.CopyToDto());
        }

        // return TaskChecklists.ToList();

        return taskChecklistss;
    }
}