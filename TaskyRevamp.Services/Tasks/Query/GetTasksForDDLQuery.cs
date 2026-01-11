using MediatR;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTasksForDDLQuery(Guid Taskid) : IRequest<List<CreateTaskDto>>;

public class GetTasksForDDLHandler : IRequestHandler<GetTasksForDDLQuery, List<CreateTaskDto>>
{
    private readonly IRepository<TaskItem> _TaskRepository;
    private readonly IRepository<TaskDependencies> _taskDependincesRepository;

    public GetTasksForDDLHandler(IRepository<TaskItem> TaskRepository, IRepository<TaskDependencies> taskDependincesRepository)
    {
        _TaskRepository = TaskRepository;
        _taskDependincesRepository = taskDependincesRepository;
    }

    public async Task<List<CreateTaskDto>> Handle(GetTasksForDDLQuery request, CancellationToken cancellationToken)
    {
        List<CreateTaskDto> allTasks = new List<CreateTaskDto>();


        var data = await _TaskRepository.FindBy(K => K.Id != null, includeProperties: $"{nameof(TaskItem.CreatedBy)}");

        var id = request.Taskid;
        var ids = new List<Guid>();
        //var dependents = await _taskDependincesRepository.FindBy(d => d.DependentId == id);
        //if(dependents.Success&&dependents.Value is not null)
        //{
        //    ids = dependents.Value.Select(d => d.TaskItemId).ToList();
        //    var tsks= await _taskDependincesRepository.FindBy(d => ids.Contains(d.DependentId));
        //    if (tsks.Success && tsks.Value is not null)
        //    {
        //        ids.AddRange(tsks.Value.Select(d => d.TaskItemId).ToList());
        //    }
        //    }
        ids.AddRange(await GetAllDependenciesAsync(id));
        ids.Add(id);
        foreach (var Task in data.Value)
        {
            CreateTaskDto dep = Task.CopyToDto();
            dep.CreatedByName = Task.CreatedBy?.NameEnglish;
            dep.UpdatedBy = Task.UpdatedBy?.NameEnglish;
            allTasks.Add(dep);

        }
         allTasks.RemoveAll(p => ids.Contains(p.Id));
        return allTasks;
    }
    private async Task<List<Guid>> GetAllDependenciesAsync(Guid rootId)
    {
        var result = new HashSet<Guid>();   // avoid duplicates
        var queue = new Queue<Guid>();

        queue.Enqueue(rootId);

        while (queue.Any())
        {
            var currentId = queue.Dequeue();

            var dependents = await _taskDependincesRepository
                .FindBy(d => d.DependentId == currentId);

            if (!dependents.Success || dependents.Value is null)
                continue;

            foreach (var dep in dependents.Value)
            {
                // If this TaskItemId is new, continue traversal
                if (result.Add(dep.TaskItemId))
                {
                    queue.Enqueue(dep.TaskItemId);
                }
            }
        }

        return result.ToList();
    }
}