using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.TaskAttachments.Command;
using TaskAttachment = TaskyRevamp.Domain.Models.Task.TaskAttachments;
namespace TaskyRevamp.Services.Tasks.Commands;

public record CreateTaskCommand(CreateTaskDto CreateTaskDto) : IRequest<string>;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, string>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IRepository<Attachment> _attachmentRepository;
    private readonly IRepository<TaskAttachment> _taskAttachmentRepository;
    private readonly IRepository<StatusSettings> _statusSettings;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Department> _depRepository;
    private readonly IFileManagement _fileManagement;
    private readonly HashSet<string> AllowedUploadedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".jpg", ".jpeg", ".png" };
    public CreateTaskHandler(IRepository<TaskItem> taskRepository, IRepository<User> userRepository, IRepository<StatusSettings> statusSettings,
        IRepository<Department> depRepository, IFileManagement fileManagement, IRepository<Attachment> attachmentRepository, IRepository<TaskAttachment> taskAttachmentRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _depRepository = depRepository;
        _fileManagement = fileManagement;
        _attachmentRepository = attachmentRepository;
        _taskAttachmentRepository = taskAttachmentRepository;
        _statusSettings = statusSettings;
    }

    public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByKey(request.CreateTaskDto.CreatedBy.Value);
        if (user is null || user.IsFailure || user.Value is null)
            throw new Exception("User Not Found");
        var TaskSatuses = await _statusSettings.All();
        var departments = await _depRepository.FindBy(k => request.CreateTaskDto.AssignedDepartmentIds.Contains(k.Id));
        if (departments is null || departments.IsFailure || departments.Value is null)
            throw new Exception("Departments Not Found");

        var task = new TaskItem(request.CreateTaskDto.Id, request.CreateTaskDto.Title,
             request.CreateTaskDto.Description!,
             request.CreateTaskDto.TypeId, request.CreateTaskDto.SourceId,
            request.CreateTaskDto.StartDate, request.CreateTaskDto.EndDate,
         request.CreateTaskDto.Priority
            , new Weight(request.CreateTaskDto.weight), user.Value.Id, departments.Value.ToList(),
            request.CreateTaskDto.AssignedIds, request.CreateTaskDto.ReminderDate, request.CreateTaskDto.ActualProcess, request.CreateTaskDto.weight, request.CreateTaskDto.Dependencies);

        if (TaskSatuses is not null)
        {
            if (request.CreateTaskDto.ActualProcess == 0 && (request.CreateTaskDto.StartDate > DateTime.Now))
            {
                task.StatusId = Guid.Parse("547022EA-EF8C-4FBC-2236-08DE3318A61C");
            }
            else if (request.CreateTaskDto.ActualProcess == 0 && (request.CreateTaskDto.StartDate <= DateTime.Now))
            {
                task.StatusId = Guid.Parse("9843AF9D-1389-4740-B428-08DE3D8A77AB");
            }
            else if (request.CreateTaskDto.ActualProcess > 0 && request.CreateTaskDto.ActualProcess < 100)
            {
                task.StatusId = Guid.Parse("753404A6-8B18-43F7-2238-08DE3318A61C");
            }
            else if (request.CreateTaskDto.ActualProcess == 100)
            {
                task.StatusId = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");
            }
        }
        await _taskRepository.Insert(task);
        if (request.CreateTaskDto.uploadAttachmentDtos is not null)
        {
            var attachmnentsDto = request.CreateTaskDto.uploadAttachmentDtos.ToList();
           if(attachmnentsDto != null)
            {
                await uploadTaskFiles(attachmnentsDto, task.Id);
            }
        }
        return task.Id.ToString();
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