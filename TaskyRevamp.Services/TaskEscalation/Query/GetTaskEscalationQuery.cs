
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;


namespace TaskEscalationyRevamp.Services.TaskEscalations.Query;

public record GetTaskEscalationQuery(Guid Id) : IRequest<TaskEscalationDto>;

public class GetTaskEscalationByIdHandler : IRequestHandler<GetTaskEscalationQuery, TaskEscalationDto>
{
    private readonly IRepository<TaskEscalation> _TaskEscalationRepository;

    public GetTaskEscalationByIdHandler(IRepository<TaskEscalation> TaskEscalationRepository)
    {
        _TaskEscalationRepository = TaskEscalationRepository;
    }

    public async Task<TaskEscalationDto> Handle(GetTaskEscalationQuery request, CancellationToken cancellationToken)
    {
        var res = await _TaskEscalationRepository.FindByKey(request.Id);
        TaskEscalationDto TaskEscalationModel = res.Value.CopyToDto();

        return TaskEscalationModel;
    }

 
}