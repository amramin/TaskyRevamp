using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Commands;

public record CreateTaskCommand(CreateTaskDto CreateTaskDto) : IRequest<string>;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, string>
{
    private readonly IRepository<TaskItem> _taskRepository;

    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Department> _depRepository;
    private readonly IFileManagement _fileManagement;

    public CreateTaskHandler(IRepository<TaskItem> taskRepository, IRepository<User> userRepository,
        IRepository<Department> depRepository, IFileManagement fileManagement)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _depRepository = depRepository;
        _fileManagement = fileManagement;
    }

    public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByKey(request.CreateTaskDto.CreatedBy.Value);
        if (user is null || user.IsFailure || user.Value is null)
            throw new Exception("User Not Found");

        var departments = await _depRepository.FindBy(k => request.CreateTaskDto.AssignedDepartmentIds.Contains(k.Id));
        if (departments is null || departments.IsFailure || departments.Value is null)
            throw new Exception("Departments Not Found");


        Guid fileid = Guid.Empty;
        if (!string.IsNullOrEmpty(request.CreateTaskDto.PhotoBase64))
        {
            fileid = await _fileManagement.UploadFile(request.CreateTaskDto.PhotoBase64,
               request.CreateTaskDto.PhotoName, FileType.AdvertisementPhoto, request.CreateTaskDto.Extention, request.CreateTaskDto.Photo);
            if (fileid == null || fileid == Guid.Empty)
                throw new Exception("file Not Found");
        }
        var task = new TaskItem(request.CreateTaskDto.Id, fileid, request.CreateTaskDto.TitleEnglish,
            request.CreateTaskDto.TitleArabic, request.CreateTaskDto.DescriptionEnglish,
            request.CreateTaskDto.DescriptionArabic, request.CreateTaskDto.TypeId, request.CreateTaskDto.SourceId,
            request.CreateTaskDto.StartDate.Value, request.CreateTaskDto.EndDate.Value,
         request.CreateTaskDto.Priority
            , new Weight(request.CreateTaskDto.weight), user.Value.Id, departments.Value.ToList(),
            request.CreateTaskDto.AssignedIds, request.CreateTaskDto.ReminderDate, request.CreateTaskDto.ActualProcess, request.CreateTaskDto.weight, request.CreateTaskDto.Dependencies);

        await _taskRepository.Insert(task);
        await _taskRepository.SaveChangesAsync();
        return task.Id.ToString();
    }
}