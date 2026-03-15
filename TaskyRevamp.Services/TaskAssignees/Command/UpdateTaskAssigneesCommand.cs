using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskyRevamp.Services.TaskAssignees.Command;

public record UpdateTaskAssigneesCommand(TaskAssigneesDto TaskAssignees) : IRequest<bool>;

public class UpdateTaskAssigneesCommandHandler : IRequestHandler<UpdateTaskAssigneesCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.TaskAssignee> _taskAssigneesRepository;

    public UpdateTaskAssigneesCommandHandler(IRepository<Domain.Models.Task.TaskAssignee> taskAssigneesRepository)
    {
        _taskAssigneesRepository = taskAssigneesRepository;
    }

    public async Task<bool> Handle(UpdateTaskAssigneesCommand request, CancellationToken cancellationToken)
    {
        var taskAssigneesResponse = await _taskAssigneesRepository.FindByKey(request.TaskAssignees.Id);
        if (!taskAssigneesResponse.Success)
        {
            return false;
        }
        var updated = taskAssigneesResponse.Value;
        updated.SetData(request.TaskAssignees);
        await _taskAssigneesRepository.Update(updated);

        return true;
    }
}