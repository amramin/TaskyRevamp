
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;
using TaskyRevamp.Dto.GeneralDto;


namespace TaskAssigneesyRevamp.Services.TaskAssigneess.Query;

public record GetTaskAssigneessQuery(QueryModel? Query) : IRequest<List<TaskAssigneesDto>>;

public class GetTaskAssigneessHandler : IRequestHandler<GetTaskAssigneessQuery, List<TaskAssigneesDto>>
{
    private readonly IRepository<TaskAssignees> _TaskAssigneesRepository;


    public GetTaskAssigneessHandler(IRepository<TaskAssignees> TaskAssigneesRepository)
    {
        _TaskAssigneesRepository = TaskAssigneesRepository;
      
    }

    public async Task<List<TaskAssigneesDto>> Handle(GetTaskAssigneessQuery request, CancellationToken cancellationToken)
    {
        List<TaskAssigneesDto> TaskAssigneesss = new List<TaskAssigneesDto>();
       

        var data = await _TaskAssigneesRepository.All();



        foreach (var TaskAssignees in data.Value)
        {
        ;
            TaskAssigneesss.Add(TaskAssignees.CopyToDto());
        }

       // return TaskAssigneess.ToList();

        return  TaskAssigneesss;
    }

   
}