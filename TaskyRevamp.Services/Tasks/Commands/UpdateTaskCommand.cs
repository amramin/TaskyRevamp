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
                    task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Not started").Id;
                }
                else if (request.Task.ActualProcess == 0 && (request.Task.StartDate <= DateTime.Now))
                {
                    if (request.Task.EndDate < DateTime.Now)
                    {
                        task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Delayed").Id;
                    }
                    else
                    {
                        task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "To do").Id;
                    }
                }
                else if (request.Task.ActualProcess > 0 && request.Task.ActualProcess < 100)
                {
                    if(request.Task.EndDate < DateTime.Now)
                    {
                        task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Delayed").Id;
                    }
                    else
                    {
                        task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "In progress").Id;
                    }
                }
                else if (request.Task.ActualProcess == 100)
                {
                    task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Pending review").Id;
                }
            }
        }
        await _taskRepository.UpdateTask(task);

        return true;
    }
}