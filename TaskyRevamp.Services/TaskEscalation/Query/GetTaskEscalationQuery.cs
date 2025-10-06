using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;

namespace TaskyRevamp.Services.TaskEscalation.Query;

public record GetTaskEscalationQuery(Guid Id) : IRequest<TaskEscalationDto>;

public class GetTaskEscalationByIdHandler : IRequestHandler<GetTaskEscalationQuery, TaskEscalationDto>
{
    private readonly IRepository<Domain.Models.Task.TaskEscalation> _taskEscalationRepository;

    public GetTaskEscalationByIdHandler(IRepository<Domain.Models.Task.TaskEscalation> taskEscalationRepository)
    {
        _taskEscalationRepository = taskEscalationRepository;
    }

    public async Task<TaskEscalationDto> Handle(GetTaskEscalationQuery request, CancellationToken cancellationToken)
    {
        var res = await _taskEscalationRepository.FindByKey(request.Id);
        var taskEscalationModel = res.Value.CopyToDto();

        return taskEscalationModel;
    }

 
}