using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using MediatR;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Interfaces.Repositeries;
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
    private readonly IRepository<StatusSettings> _statusSettings;
    private readonly IRepository<AddTaskSettings> _addTaskSettingRepository;
    private readonly IRepository<TaskItem> _taskrepo;
    private readonly IRepository<TaskAssignee> _taskAssigneeRepository;
    private readonly IRepository<TaskComments> _taskCommentRepository;
	private readonly IRepository<TaskChecklist> _taskChecklistRepository;

	public UpdateTaskCommandHandler(ITaskRepository taskRepository, IRepository<StatusSettings> statusSettings, IRepository<TaskItem> taskrepo, IRepository<TaskChecklist> taskChecklistRepository,
		IRepository<TaskComments> taskCommentRepository, IRepository<AddTaskSettings> addTaskSettingRepository, IRepository<TaskAssignee> taskAssigneeRepository)
	{
		_taskRepository = taskRepository;
		_statusSettings = statusSettings;
		_taskrepo = taskrepo;
		_taskCommentRepository = taskCommentRepository;
		_addTaskSettingRepository = addTaskSettingRepository;
		_taskAssigneeRepository = taskAssigneeRepository;
		_taskChecklistRepository = taskChecklistRepository;
	}

	public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskById(request.Task.Id);
        var TaskSatuses = await _statusSettings.All();
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        int oldprogress = task.Progress;
        var oldstatus = task.StatusId;
        task.SetData(request.Task);
        //if (oldprogress != request.Task.ActualProcess)
        //{
            if (TaskSatuses is not null)
            {
                if (request.Task.ActualProcess == 0 && (request.Task.StartDate > DateTime.Now))
                {
                    task.StatusId =Guid.Parse("547022EA-EF8C-4FBC-2236-08DE3318A61C");
                }
                else if (request.Task.ActualProcess == 0 && (request.Task.StartDate <= DateTime.Now))
                {
                    if (request.Task.EndDate < DateTime.Now)
                    {
                        task.StatusId = Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C");
                    }
                    else
                    {
                        task.StatusId = Guid.Parse("9843AF9D-1389-4740-B428-08DE3D8A77AB");
                    }
                }
                else if (request.Task.ActualProcess > 0 && request.Task.ActualProcess < 100)
                {
                    if(request.Task.EndDate < DateTime.Now)
                    {
                        task.StatusId = Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C");
                    }
                    else
                    {
                        task.StatusId = Guid.Parse("753404A6-8B18-43F7-2238-08DE3318A61C");
                    }
                }
                else if (request.Task.ActualProcess == 100)
                {
                    var res = await _addTaskSettingRepository.FindBy(t => t.NameEnglish == "Dependency");
                    var dependencySetting = res.Value?.FirstOrDefault();
					if (task.Dependencies is  not null && task.Dependencies.Count > 0 && dependencySetting!.IsActive)
                    {
                        var dependencies = task.Dependencies?.Select(p => p.DependentId).ToList();
                        var tasksnotcompleted = await _taskrepo.FindBy(d => dependencies!.Contains(d.Id));
                        if(tasksnotcompleted.Value!.Any(p => p.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C"))){
                            throw new Exception("DependencyError");
                        }
                        else
                        {
                            task.StatusId = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");
                        }
                    }
                    else
                    {
                        task.StatusId = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");
                    }
                }
            }
        //}
        //if (task.EndDate < DateTime.Now && (task.StatusId!= Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C")||
        //    task.StatusId!= Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C")|| task.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C")))
        //{
        //    task.StatusId = Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C");
        //}
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