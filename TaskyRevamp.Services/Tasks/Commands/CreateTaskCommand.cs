using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Commands;

public record CreateTaskCommand(CreateTaskDto CreateTaskDto) : IRequest<Guid>;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IRepository<TaskItem> _taskRepository;

    public CreateTaskHandler(IRepository<TaskItem> taskRepository) => _taskRepository = taskRepository;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        return Guid.NewGuid();
             /*
        var task =  new TaskItem(request.CreateTaskDto.Id,request.CreateTaskDto.Title, request.CreateTaskDto.Description);
        await _taskRepository.Insert(task);
        await _taskRepository.SaveChangesAsync();
        return task.Id;
             */
    }
}
