using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskyRevamp.Services.TaskAssignees.Command;

public record CreateTaskAssigneesCommand(TaskAssigneesDto TaskAssigneesDto) : IRequest<Guid>;

public class CreateTaskAssigneesHandler : IRequestHandler<CreateTaskAssigneesCommand, Guid>
{
    private readonly IRepository<Domain.Models.Task.TaskAssignee> _taskAssigneesRepository;

    public CreateTaskAssigneesHandler(IRepository<Domain.Models.Task.TaskAssignee> taskAssigneesRepository) => _taskAssigneesRepository = taskAssigneesRepository;

    public async Task<Guid> Handle(CreateTaskAssigneesCommand request, CancellationToken cancellationToken)
    {
        var taskAssignees=new Domain.Models.Task.TaskAssignee();
        taskAssignees.SetData(request.TaskAssigneesDto);
       
        await _taskAssigneesRepository.Insert(taskAssignees);
        await _taskAssigneesRepository.SaveChangesAsync();
        return taskAssignees.Id;

    }
}
