using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;

namespace TaskyRevamp.Services.TaskEscalation.Command;

public record UpdateTaskEscalationCommand(TaskEscalationDto TaskEscalation) : IRequest<bool>;

public class UpdateTaskEscalationCommandHandler : IRequestHandler<UpdateTaskEscalationCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.TaskEscalation> _taskEscalationRepository;

    public UpdateTaskEscalationCommandHandler(IRepository<Domain.Models.Task.TaskEscalation> taskEscalationRepository)
    {
        _taskEscalationRepository = taskEscalationRepository;
    }

    public async Task<bool> Handle(UpdateTaskEscalationCommand request, CancellationToken cancellationToken)
    {
        var taskEscalationResponse = await _taskEscalationRepository.FindByKey(request.TaskEscalation.Id);
        if (!taskEscalationResponse.Success)
        {
            return false;
        }
        var updated = taskEscalationResponse.Value;
      updated.SetData(request.TaskEscalation);
        await _taskEscalationRepository.Update(updated);

        return true;
    }
}