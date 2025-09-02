
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;


namespace TaskChecklistyRevamp.Services.TaskChecklists.Query;

public record GetTaskChecklistQuery(Guid Id) : IRequest<TaskChecklistDto>;

public class GetTaskChecklistByIdHandler : IRequestHandler<GetTaskChecklistQuery, TaskChecklistDto>
{
    private readonly IRepository<TaskChecklist> _TaskChecklistRepository;

    public GetTaskChecklistByIdHandler(IRepository<TaskChecklist> TaskChecklistRepository)
    {
        _TaskChecklistRepository = TaskChecklistRepository;
    }

    public async Task<TaskChecklistDto> Handle(GetTaskChecklistQuery request, CancellationToken cancellationToken)
    {
        var res = await _TaskChecklistRepository.FindByKey(request.Id);
        TaskChecklistDto TaskChecklistModel=  res.Value.CopyToDto();
   

        return TaskChecklistModel;
    }

 
}