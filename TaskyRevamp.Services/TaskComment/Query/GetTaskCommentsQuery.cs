using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Services.TaskComment.Query;

public record GetTaskCommentsQuery(QueryModel? Query) : IRequest<List<TaskCommentDto>>;

public class GetTaskCommentsHandler : IRequestHandler<GetTaskCommentsQuery, List<TaskCommentDto>>
{
    private readonly IRepository<Domain.Models.Task.TaskComment> _taskCommentRepository;


    public GetTaskCommentsHandler(IRepository<Domain.Models.Task.TaskComment> taskCommentRepository)
    {
        _taskCommentRepository = taskCommentRepository;
      
    }

    public async Task<List<TaskCommentDto>> Handle(GetTaskCommentsQuery request, CancellationToken cancellationToken)
    {
        var taskCommentss = new List<TaskCommentDto>();
       

        var data = await _taskCommentRepository.All();



        foreach (var taskComment in data.Value)
        {
        ;
            taskCommentss.Add(taskComment.CopyToDto());
        }

       // return TaskComments.ToList();

        return  taskCommentss;
    }

   
}