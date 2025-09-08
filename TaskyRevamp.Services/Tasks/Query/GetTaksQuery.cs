
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTaksQuery(QueryModel? Query) : IRequest<List<CreateTaskDto>>;

public class GetTasksHandler : IRequestHandler<GetTaksQuery, List<CreateTaskDto>>
{
    private readonly IRepository<TaskItem> _TaskRepository;
    private string currLang;


    private readonly IRepository<Domain.Models.Users.User> _userRepository;
    public GetTasksHandler(IRepository<TaskItem> TaskRepository, IRepository<Domain.Models.Users.User> userRepository)
    {
        _TaskRepository = TaskRepository;
        _userRepository = userRepository;
        currLang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }

    public async Task<List<CreateTaskDto>> Handle(GetTaksQuery request, CancellationToken cancellationToken)
    {
        List<CreateTaskDto> Taskss = new List<CreateTaskDto>();
        Domain.Models.Users.User userupdate = new Domain.Models.Users.User();

        Expression<Func<TaskItem, bool>> whereExp = null;
      

        var data = await _TaskRepository.AllInclude(request.Query?.Paging, x => x.CreateDate, "desc",whereExp, x => x.CreatedBy);



        foreach (var Task in data.Value)
        {
        ;
            Taskss.Add(Task.CopyToDto());
        }

       // return Tasks.ToList();

        return  Taskss;
    }

   
}