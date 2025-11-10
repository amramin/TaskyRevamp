using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTasksForDDLQuery() : IRequest<List<CreateTaskDto>>;

public class GetTasksForDDLHandler : IRequestHandler<GetTasksForDDLQuery, List<CreateTaskDto>>
{
    private readonly IRepository<TaskItem> _TaskRepository;


    public GetTasksForDDLHandler(IRepository<TaskItem> TaskRepository)
    {
        _TaskRepository = TaskRepository;
    }

    public async Task<List<CreateTaskDto>> Handle(GetTasksForDDLQuery request, CancellationToken cancellationToken)
    {
        List<CreateTaskDto> allTasks = new List<CreateTaskDto>();


        var data = await _TaskRepository.FindBy(K => K.Id != null, includeProperties: $"{nameof(TaskItem.CreatedBy)}");



        foreach (var Task in data.Value)
        {
            CreateTaskDto dep = Task.CopyToDto();
            dep.CreatedByName = Task.CreatedBy?.NameEnglish;
            dep.UpdatedBy = Task.UpdatedBy?.NameEnglish;
            allTasks.Add(dep);

        }

        return allTasks;
    }

}