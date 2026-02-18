using MediatR;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using TaskyRevamp.Dto.TaskDto;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;
using TaskAttachment = TaskyRevamp.Domain.Models.Task.TaskAttachments;


namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskCommand(CreateTaskDto Task) : IRequest<bool>;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IRepository<StatusSettings> _statusSettings;
    private readonly IRepository<TaskDependencies> _taskDependincesRepository;
    private readonly IRepository<AddTaskSettings> _addTaskSettingRepository;
    private readonly IRepository<TaskItem> _taskrepo;
    private readonly IRepository<TaskComments> _taskCommentRepository;
    private readonly IRepository<Attachment> _attachmentRepository;
    private readonly IRepository<TaskAttachment> _taskAttachmentRepository;
    private readonly HashSet<string> AllowedUploadedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".jpg", ".jpeg", ".png" };
    private readonly IFileManagement _fileManagement;

	public UpdateTaskCommandHandler(ITaskRepository taskRepository, IFileManagement fileManagement, IRepository<Attachment> attachmentRepository, IRepository<TaskAttachment> taskAttachmentRepository, IRepository<StatusSettings> statusSettings, IRepository<TaskDependencies> taskDependincesRepository, IRepository<TaskItem> taskrepo, IRepository<TaskComments> taskCommentRepository, IRepository<AddTaskSettings> addTaskSettingRepository)
	{
		_taskRepository = taskRepository;
		_statusSettings = statusSettings;
		_taskDependincesRepository = taskDependincesRepository;
		_taskrepo = taskrepo;
		_taskCommentRepository = taskCommentRepository;
		_attachmentRepository = attachmentRepository;
		_taskAttachmentRepository = taskAttachmentRepository;
		_fileManagement = fileManagement;
		_addTaskSettingRepository = addTaskSettingRepository;
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
        if (oldprogress != request.Task.ActualProcess)
        {
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
        }
        await _taskRepository.UpdateTask(task);
        if (request.Task.uploadAttachmentDtos is not null)
        {
            var attachmnentsDto = request.Task.uploadAttachmentDtos.ToList();
            if (attachmnentsDto != null)
            {
                await uploadTaskFiles(attachmnentsDto, task.Id);
            }
        }
        if (!string.IsNullOrEmpty(request.Task.Content))
        {
            TaskComments taskComment = new TaskComments(task.Id, request.Task.Content);
            await _taskCommentRepository.Insert(taskComment);
        }
        return true;
    }
    public async Task uploadTaskFiles(List<UploadAttachmentDto> uploadAttachmentDtos, Guid taskId)
    {
        var taskAttachmentsResult = await _taskAttachmentRepository.FindBy(t => t.TaskItemId == taskId);
        TaskAttachment taskAttachments;
        if (!taskAttachmentsResult.Success || taskAttachmentsResult.Value == null || !taskAttachmentsResult.Value.Any())
        {
            taskAttachments = new TaskAttachment { Id = Guid.NewGuid(), TaskItemId = taskId };
            await _taskAttachmentRepository.Insert(taskAttachments);
        }
        else
            taskAttachments = taskAttachmentsResult.Value.FirstOrDefault()!;
        foreach (var dto in uploadAttachmentDtos)
        {
            var extension = Path.GetExtension(dto.FileName)?.ToLower();
            if (!AllowedUploadedExtensions.Contains(extension!))
                continue;
            var fileType = _fileManagement.ResolveFileType(dto.FileName);
            var fileId = await _fileManagement.UploadFile(dto.Bytes, dto.FileName, fileType);
            var attachment = new Attachment(Guid.NewGuid(), dto.FileName, fileId, dto.Size, taskAttachments.Id, fileType);
            await _attachmentRepository.Insert(attachment);
        }
    }
}