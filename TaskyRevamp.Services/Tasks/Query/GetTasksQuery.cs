using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTasksQuery(QueryModel? Query) : IRequest<List<CreateTaskDto>>;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, List<CreateTaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private string _currentLanguage;


    private readonly IRepository<Domain.Models.Users.User> _userRepository;

    public GetTasksHandler(ITaskRepository taskRepository, IRepository<Domain.Models.Users.User> userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }

    public async Task<List<CreateTaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = new List<CreateTaskDto>();
        var userUpdate = new Domain.Models.Users.User();

        Expression<Func<TaskItem, bool>>? whereExpression = null;
        var data = await _taskRepository.GetTasks(request.Query?.Paging, x => x.CreateDate, "desc", whereExpression);
        foreach (var task in data)
        {
            tasks.Add(task.CopyToDto());
        }
        return tasks;
    }
}