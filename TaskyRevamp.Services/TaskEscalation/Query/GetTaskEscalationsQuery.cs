using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskEscalation;

namespace TaskyRevamp.Services.TaskEscalation.Query;

public record GetTaskEscalationsQuery(QueryModel? Query) : IRequest<List<TaskEscalationDto>>;

public class GetTaskEscalationsHandler : IRequestHandler<GetTaskEscalationsQuery, List<TaskEscalationDto>>
{
    private readonly IRepository<Domain.Models.Task.TaskEscalation> _taskEscalationRepository;


    public GetTaskEscalationsHandler(IRepository<Domain.Models.Task.TaskEscalation> taskEscalationRepository)
    {
        _taskEscalationRepository = taskEscalationRepository;
      
    }

    public async Task<List<TaskEscalationDto>> Handle(GetTaskEscalationsQuery request, CancellationToken cancellationToken)
    {
        var taskEscalationss = new List<TaskEscalationDto>();
       

        var data = await _taskEscalationRepository.All();



        foreach (var taskEscalation in data.Value)
        {
        ;
            taskEscalationss.Add(taskEscalation.CopyToDto());
        }

       // return TaskEscalations.ToList();

        return  taskEscalationss;
    }

   
}