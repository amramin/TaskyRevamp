
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTaskQuery(Guid Id) : IRequest<CreateTaskDto>;

public class GetTaskByIdHandler : IRequestHandler<GetTaskQuery, CreateTaskDto>
{
    private readonly IRepository<TaskItem> _TaskRepository;

    public GetTaskByIdHandler(IRepository<TaskItem> TaskRepository)
    {
        _TaskRepository = TaskRepository;
    }

    public async Task<CreateTaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var res = await _TaskRepository.FindByKey(request.Id);
        CreateTaskDto TaskModel =    res.Value.CopyToDto();

        return TaskModel;
    }
}