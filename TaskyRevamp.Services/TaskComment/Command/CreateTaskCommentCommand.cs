using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Services.TaskComment.Command;

public record CreateTaskCommentCommand(TaskCommentDto TaskCommentDto) : IRequest<Guid>;

public class CreateTaskCommentHandler : IRequestHandler<CreateTaskCommentCommand, Guid>
{
    private readonly IRepository<Domain.Models.Task.TaskComment> _taskCommentRepository;

    public CreateTaskCommentHandler(IRepository<Domain.Models.Task.TaskComment> taskCommentRepository) => _taskCommentRepository = taskCommentRepository;

    public async Task<Guid> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        var taskComment=new Domain.Models.Task.TaskComment( request.TaskCommentDto.TaskItemId,
            request.TaskCommentDto.Content,request.TaskCommentDto.CreatedById);
 
       
        await _taskCommentRepository.Insert(taskComment);
        await _taskCommentRepository.SaveChangesAsync();
        return taskComment.Id;

    }
}
