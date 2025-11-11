using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Commands;

public record CreateTaskCommand(CreateTaskDto CreateTaskDto) : IRequest<Guid>;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IRepository<TaskItem> _taskRepository;

    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Department> _depRepository;

    public CreateTaskHandler(IRepository<TaskItem> taskRepository, IRepository<User> userRepository,
        IRepository<Department> depRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _depRepository = depRepository;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByKey(request.CreateTaskDto.CreatedBy);
        if (user is null || user.IsFailure || user.Value is null)
            throw new Exception("User Not Found");

        var departments = await _depRepository.FindBy(k => request.CreateTaskDto.AssignedDepartmentIds.Contains(k.Id));
        if (departments is null || departments.IsFailure || departments.Value is null)
            throw new Exception("Departments Not Found");

        var task = new TaskItem(request.CreateTaskDto.Id, request.CreateTaskDto.TitleEnglish,
            request.CreateTaskDto.TitleArabic, request.CreateTaskDto.DescriptionEnglish,
            request.CreateTaskDto.DescriptionArabic, request.CreateTaskDto.TypeId, request.CreateTaskDto.SourceId,
            request.CreateTaskDto.StartDate, request.CreateTaskDto.EndDate,
         request.CreateTaskDto.Priority
            , new Weight(request.CreateTaskDto.weight), user.Value.Id, departments.Value.ToList(),
            request.CreateTaskDto.AssignedIds, request.CreateTaskDto.ReminderDate, request.CreateTaskDto.ActualProcess, request.CreateTaskDto.weight, request.CreateTaskDto.Dependencies);

        await _taskRepository.Insert(task);
        await _taskRepository.SaveChangesAsync();
        return task.Id;
    }
}