using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;

namespace TaskyRevamp.Services.TaskChecklists.Query;

public record GetTaskChecklistQuery(Guid Id) : IRequest<TaskChecklistDto>;

public class GetTaskChecklistByIdHandler : IRequestHandler<GetTaskChecklistQuery, TaskChecklistDto>
{
    private readonly IRepository<TaskChecklist> _taskChecklistRepository;

    public GetTaskChecklistByIdHandler(IRepository<TaskChecklist> taskChecklistRepository)
    {
        _taskChecklistRepository = taskChecklistRepository;
    }

    public async Task<TaskChecklistDto> Handle(GetTaskChecklistQuery request, CancellationToken cancellationToken)
    {
        var res = await _taskChecklistRepository.FindByKey(request.Id);
        var taskChecklistModel=  res.Value.CopyToDto();
   

        return taskChecklistModel;
    }

 
}