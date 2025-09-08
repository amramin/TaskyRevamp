
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Dto.GeneralDto;


namespace TaskCommentyRevamp.Services.TaskComments.Query;

public record GetTaskCommentsQuery(QueryModel? Query) : IRequest<List<TaskCommentDto>>;

public class GetTaskCommentsHandler : IRequestHandler<GetTaskCommentsQuery, List<TaskCommentDto>>
{
    private readonly IRepository<TaskComment> _TaskCommentRepository;


    public GetTaskCommentsHandler(IRepository<TaskComment> TaskCommentRepository)
    {
        _TaskCommentRepository = TaskCommentRepository;
      
    }

    public async Task<List<TaskCommentDto>> Handle(GetTaskCommentsQuery request, CancellationToken cancellationToken)
    {
        List<TaskCommentDto> TaskCommentss = new List<TaskCommentDto>();
       

        var data = await _TaskCommentRepository.All();



        foreach (var TaskComment in data.Value)
        {
        ;
            TaskCommentss.Add(TaskComment.CopyToDto());
        }

       // return TaskComments.ToList();

        return  TaskCommentss;
    }

   
}