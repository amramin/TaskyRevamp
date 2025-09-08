using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;



namespace TaskAssigneesyRevamp.Services.TaskAssigneess.Commands;

public record UpdateTaskAssigneesCommand(TaskAssigneesDto TaskAssignees) : IRequest<bool>;

public class UpdateTaskAssigneesCommandHandler : IRequestHandler<UpdateTaskAssigneesCommand, bool>
{
    private readonly IRepository<TaskAssignees> _TaskAssigneesRepository;

    public UpdateTaskAssigneesCommandHandler(IRepository<TaskAssignees> TaskAssigneesRepository)
    {
        _TaskAssigneesRepository = TaskAssigneesRepository;
    }

    public async Task<bool> Handle(UpdateTaskAssigneesCommand request, CancellationToken cancellationToken)
    {
        var TaskAssigneesResponse = await _TaskAssigneesRepository.FindByKey(request.TaskAssignees.Id);
        if (!TaskAssigneesResponse.Success)
        {
            return false;
        }
        var updated = TaskAssigneesResponse.Value;
        updated.SetData(request.TaskAssignees);
        await _TaskAssigneesRepository.Update(updated);

        return true;
    }
}