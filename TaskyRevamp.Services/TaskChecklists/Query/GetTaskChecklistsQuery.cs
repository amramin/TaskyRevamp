using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskChecklist;

namespace TaskyRevamp.Services.TaskChecklists.Query;

public record GetTaskChecklistsQuery(Guid TaskId) : IRequest<List<TaskChecklistDto>>;
public class GetTaskChecklistsHandler : IRequestHandler<GetTaskChecklistsQuery, List<TaskChecklistDto>>
{
    private readonly IRepository<TaskChecklist> _taskChecklistRepository;
    public GetTaskChecklistsHandler(IRepository<TaskChecklist> taskChecklistRepository)
    {
        _taskChecklistRepository = taskChecklistRepository;
    }
    public async Task<List<TaskChecklistDto>> Handle(GetTaskChecklistsQuery request, CancellationToken cancellationToken)
    {
        var taskChecklists = new List<TaskChecklistDto>();
        var data = await _taskChecklistRepository.FindBy(c => c.TaskItemId == request.TaskId);
		foreach (var taskChecklist in data.Value!)
            taskChecklists.Add(taskChecklist.CopyToDto());
        return taskChecklists;
    }
}