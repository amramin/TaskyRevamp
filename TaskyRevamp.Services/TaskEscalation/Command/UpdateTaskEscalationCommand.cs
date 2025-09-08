using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;



namespace TaskEscalationyRevamp.Services.TaskEscalations.Commands;

public record UpdateTaskEscalationCommand(TaskEscalationDto TaskEscalation) : IRequest<bool>;

public class UpdateTaskEscalationCommandHandler : IRequestHandler<UpdateTaskEscalationCommand, bool>
{
    private readonly IRepository<TaskEscalation> _TaskEscalationRepository;

    public UpdateTaskEscalationCommandHandler(IRepository<TaskEscalation> TaskEscalationRepository)
    {
        _TaskEscalationRepository = TaskEscalationRepository;
    }

    public async Task<bool> Handle(UpdateTaskEscalationCommand request, CancellationToken cancellationToken)
    {
        var TaskEscalationResponse = await _TaskEscalationRepository.FindByKey(request.TaskEscalation.Id);
        if (!TaskEscalationResponse.Success)
        {
            return false;
        }
        var updated = TaskEscalationResponse.Value;
      updated.SetData(request.TaskEscalation);
        await _TaskEscalationRepository.Update(updated);

        return true;
    }
}