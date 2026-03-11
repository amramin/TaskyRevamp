using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands;

public record SoftDeleteTaskCommand(Guid Id, Guid currentUserId) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<SoftDeleteTaskCommand, bool>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IRepository<TaskDependencies> _taskDependincesRepository;

	public DeleteGroupCommandHandler(IRepository<TaskItem> taskRepository, IRepository<TaskDependencies> taskDependincesRepository)
	{
		_taskRepository = taskRepository;
		_taskDependincesRepository = taskDependincesRepository;
	}

	public async Task<bool> Handle(SoftDeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var res = await _taskDependincesRepository.FindBy(x => x.TaskItemId == request.Id);
		if(res.Success && res.Value is not null && res.Value.Any())
			return false;
		var taskRes = await _taskRepository.FindBy(t => t.Id == request.Id);
		if(taskRes.Success && taskRes.Value is not null)
		{
			var task = taskRes.Value.FirstOrDefault();
			task!.SoftDelete(request.currentUserId);
			await _taskRepository.Update(task);
			return true;
		}
	return false;
	}
}