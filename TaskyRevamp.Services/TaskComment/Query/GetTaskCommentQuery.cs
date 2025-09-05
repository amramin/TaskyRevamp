
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;


namespace TaskCommentyRevamp.Services.TaskComments.Query;

public record GetTaskCommentQuery(Guid Id) : IRequest<TaskCommentDto>;

public class GetTaskCommentByIdHandler : IRequestHandler<GetTaskCommentQuery, TaskCommentDto>
{
    private readonly IRepository<TaskComment> _TaskCommentRepository;

    public GetTaskCommentByIdHandler(IRepository<TaskComment> TaskCommentRepository)
    {
        _TaskCommentRepository = TaskCommentRepository;
    }

    public async Task<TaskCommentDto> Handle(GetTaskCommentQuery request, CancellationToken cancellationToken)
    {
        var res = await _TaskCommentRepository.FindByKey(request.Id);
        TaskCommentDto TaskCommentModel= res.Value.CopyToDto();
      

        return TaskCommentModel;
    }

 
}