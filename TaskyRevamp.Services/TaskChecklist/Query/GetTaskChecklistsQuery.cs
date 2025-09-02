
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.GeneralDto;


namespace TaskChecklistyRevamp.Services.TaskChecklists.Query;

public record GetTaskChecklistsQuery(QueryModel? Query) : IRequest<List<TaskChecklistDto>>;

public class GetTaskChecklistsHandler : IRequestHandler<GetTaskChecklistsQuery, List<TaskChecklistDto>>
{
    private readonly IRepository<TaskChecklist> _TaskChecklistRepository;


    public GetTaskChecklistsHandler(IRepository<TaskChecklist> TaskChecklistRepository)
    {
        _TaskChecklistRepository = TaskChecklistRepository;
      
    }

    public async Task<List<TaskChecklistDto>> Handle(GetTaskChecklistsQuery request, CancellationToken cancellationToken)
    {
        List<TaskChecklistDto> TaskChecklistss = new List<TaskChecklistDto>();
       

        var data = await _TaskChecklistRepository.All();



        foreach (var TaskChecklist in data.Value)
        {
   
            TaskChecklistss.Add(TaskChecklist.CopyToDto());
        }

       // return TaskChecklists.ToList();

        return  TaskChecklistss;
    }

   
}