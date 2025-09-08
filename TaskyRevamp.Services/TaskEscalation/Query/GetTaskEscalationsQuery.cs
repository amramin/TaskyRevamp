
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;
using TaskyRevamp.Dto.GeneralDto;


namespace TaskEscalationyRevamp.Services.TaskEscalations.Query;

public record GetTaskEscalationsQuery(QueryModel? Query) : IRequest<List<TaskEscalationDto>>;

public class GetTaskEscalationsHandler : IRequestHandler<GetTaskEscalationsQuery, List<TaskEscalationDto>>
{
    private readonly IRepository<TaskEscalation> _TaskEscalationRepository;


    public GetTaskEscalationsHandler(IRepository<TaskEscalation> TaskEscalationRepository)
    {
        _TaskEscalationRepository = TaskEscalationRepository;
      
    }

    public async Task<List<TaskEscalationDto>> Handle(GetTaskEscalationsQuery request, CancellationToken cancellationToken)
    {
        List<TaskEscalationDto> TaskEscalationss = new List<TaskEscalationDto>();
       

        var data = await _TaskEscalationRepository.All();



        foreach (var TaskEscalation in data.Value)
        {
        ;
            TaskEscalationss.Add(TaskEscalation.CopyToDto());
        }

       // return TaskEscalations.ToList();

        return  TaskEscalationss;
    }

   
}