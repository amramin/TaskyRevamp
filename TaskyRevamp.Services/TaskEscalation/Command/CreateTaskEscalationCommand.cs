using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;

namespace TaskyRevamp.Services.TaskEscalation.Command;

public record CreateTaskEscalationCommand(TaskEscalationDto TaskEscalationDto) : IRequest<Guid>;

public class CreateTaskEscalationHandler : IRequestHandler<CreateTaskEscalationCommand, Guid>
{
    private readonly IRepository<Domain.Models.Task.TaskEscalation> _taskEscalationRepository;

    public CreateTaskEscalationHandler(IRepository<Domain.Models.Task.TaskEscalation> taskEscalationRepository) => _taskEscalationRepository = taskEscalationRepository;

    public async Task<Guid> Handle(CreateTaskEscalationCommand request, CancellationToken cancellationToken)
    {
        var taskEscalation=new Domain.Models.Task.TaskEscalation();
        taskEscalation.SetData(request.TaskEscalationDto);
       
        await _taskEscalationRepository.Insert(taskEscalation);
        await _taskEscalationRepository.SaveChangesAsync();
        return taskEscalation.Id;

    }
}
