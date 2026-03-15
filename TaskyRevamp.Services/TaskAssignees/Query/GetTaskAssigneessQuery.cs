using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskyRevamp.Services.TaskAssignees.Query;

public record GetTaskAssigneessQuery(QueryModel? Query) : IRequest<List<TaskAssigneesDto>>;

public class GetTaskAssigneessHandler : IRequestHandler<GetTaskAssigneessQuery, List<TaskAssigneesDto>>
{
    private readonly IRepository<Domain.Models.Task.TaskAssignee> _taskAssigneesRepository;


    public GetTaskAssigneessHandler(IRepository<Domain.Models.Task.TaskAssignee> taskAssigneesRepository)
    {
        _taskAssigneesRepository = taskAssigneesRepository;
      
    }

    public async Task<List<TaskAssigneesDto>> Handle(GetTaskAssigneessQuery request, CancellationToken cancellationToken)
    {
        var taskAssigneesss = new List<TaskAssigneesDto>();
       

        var data = await _taskAssigneesRepository.All();



        foreach (var taskAssignees in data.Value)
        {
        ;
            taskAssigneesss.Add(taskAssignees.CopyToDto());
        }

       // return TaskAssigneess.ToList();

        return  taskAssigneesss;
    }

   
}