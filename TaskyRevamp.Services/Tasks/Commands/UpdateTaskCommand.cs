using MediatR;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;


namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskCommand(CreateTaskDto Task) : IRequest<bool>;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IRepository<StatusSettings> _statusSettings;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository, IRepository<StatusSettings> statusSettings)
    {
        _taskRepository = taskRepository;
        _statusSettings = statusSettings;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskById(request.Task.Id);
        var TaskSatuses = await _statusSettings.All();
        if (task == null)
        {
            throw new Exception("Task not found");
        }
 

        task.SetData(request.Task);
        if (task.Progress != request.Task.ActualProcess)
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
                    task.StatusId = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");
                }
            }
        }
        await _taskRepository.UpdateTask(task);

        return true;
    }
}