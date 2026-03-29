using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using MediatR;
using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Interfaces.Services;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using TaskyRevamp.Dto.TaskDto;
using TaskAttachment = TaskyRevamp.Domain.Models.Task.TaskAttachments;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;


namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskCommand(CreateTaskDto Task) : IRequest<bool>;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IRepository<AddTaskSettings> _addTaskSettingRepository;
    private readonly IRepository<TaskAssignee> _taskAssigneeRepository;
    private readonly IRepository<TaskComments> _taskCommentRepository;
	private readonly IRepository<TaskChecklist> _taskChecklistRepository;
	private readonly ITaskStatusDeterminer _statusDeterminer;

	public UpdateTaskCommandHandler(ITaskRepository taskRepository, IRepository<TaskChecklist> taskChecklistRepository,
		IRepository<TaskComments> taskCommentRepository, IRepository<AddTaskSettings> addTaskSettingRepository,
		IRepository<TaskAssignee> taskAssigneeRepository, ITaskStatusDeterminer statusDeterminer)
	{
		_taskRepository = taskRepository;
		_taskCommentRepository = taskCommentRepository;
		_addTaskSettingRepository = addTaskSettingRepository;
		_taskAssigneeRepository = taskAssigneeRepository;
		_taskChecklistRepository = taskChecklistRepository;
		_statusDeterminer = statusDeterminer;
	}

	public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskById(request.Task.Id);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        int oldprogress = task.Progress;
        var oldstatus = task.StatusId;
        task.SetData(request.Task);

        if (request.Task.ActualProcess == 100)
        {
            var res = await _addTaskSettingRepository.FindBy(t => t.NameEnglish == "Dependency");
            var dependencySetting = res.Value?.FirstOrDefault();
            if (task.Dependencies is not null && task.Dependencies.Count > 0 && dependencySetting!.IsActive)
            {
                var dependencyIds = task.Dependencies.Select(p => p.DependentId).ToList();
                if (!await _statusDeterminer.AreDependenciesCompleted(dependencyIds))
                {
                    throw new Exception("DependencyError");
                }
            }
        }

        task.StatusId = _statusDeterminer.DetermineStatus(
            request.Task.ActualProcess,
            request.Task.StartDate,
            request.Task.EndDate,
            task.StatusId);

        await _taskRepository.UpdateTask(task);
        await UpdateAssignees(request.Task, task);
        if (!string.IsNullOrEmpty(request.Task.Content))
        {
            TaskComments taskComment = new TaskComments(task.Id, request.Task.Content);
            await _taskCommentRepository.Insert(taskComment);
        }
		var taskChecklists = new List<TaskChecklist>();
		if (request.Task.Checklists is not null && request.Task.Checklists.Count > 0)
		{
			foreach (var checklist in request.Task.Checklists)
			{
				var taskChecklist = new TaskChecklist(task.Id, checklist.Title);
				if (checklist.Items != null && checklist.Items.Any())
				{
					foreach (var item in checklist.Items)
						taskChecklist.items.Add(new ChecklistItem(item.Title, taskChecklist.Id, item.AssignedUserId, item.EndDate, item.IsDone));
				}
				taskChecklists.Add(taskChecklist);
			}
			await _taskChecklistRepository.InsertRange(taskChecklists);
		}
		return true;
    }
    private async System.Threading.Tasks.Task UpdateAssignees(CreateTaskDto taskDto,TaskItem taskItem)
    {

        var existingAssignees = taskItem.TaskAssignees.ToList(); 
        var newAssigneesIds = taskDto.AssignedIds ?? new List<Guid>();

        var toRemove = existingAssignees
            .Where(a => !newAssigneesIds.Contains(a.UserId)).Select(p=>p.Id)
            .ToList();

        if (toRemove.Any())
        {
            await _taskAssigneeRepository.DeleteRang(toRemove);
        }

        var toAdd = newAssigneesIds
            .Where(id => !existingAssignees.Any(a => a.UserId == id))
            .Select(id => new TaskAssignee
            {
                TaskItemId = taskItem.Id,
                UserId = id,
                AssigneeDate = DateTime.Now
            }).ToList();

        if (toAdd.Any())
        {
            await _taskAssigneeRepository.InsertRange(toAdd);
        }

       await _taskAssigneeRepository.SaveChangesAsync();
    }
}