using MediatR;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTaskQuery(Guid Id) : IRequest<CreateTaskDto>;

public class GetTaskByIdHandler : IRequestHandler<GetTaskQuery, CreateTaskDto>
{
 
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }


    public async Task<CreateTaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var res =  await _taskRepository.GetTaskById(request.Id);
        if (res is null)
        {
            throw new Exception("Task not found");
        }
        var taskDto = res.CopyToDto();

        return taskDto;
    }
}