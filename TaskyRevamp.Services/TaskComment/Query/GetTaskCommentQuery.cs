using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Services.TaskComment.Query;

public record GetTaskCommentQuery(Guid Id) : IRequest<TaskCommentDto>;

public class GetTaskCommentByIdHandler : IRequestHandler<GetTaskCommentQuery, TaskCommentDto>
{
    private readonly IRepository<Domain.Models.Task.TaskComment> _taskCommentRepository;

    public GetTaskCommentByIdHandler(IRepository<Domain.Models.Task.TaskComment> taskCommentRepository)
    {
        _taskCommentRepository = taskCommentRepository;
    }

    public async Task<TaskCommentDto> Handle(GetTaskCommentQuery request, CancellationToken cancellationToken)
    {
        var res = await _taskCommentRepository.FindByKey(request.Id);
        var taskCommentModel= res.Value.CopyToDto();
      

        return taskCommentModel;
    }

 
}