using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;

namespace TaskyRevamp.Services.TaskComment.Command;

public record CreateTaskCommentCommand(TaskCommentDto TaskCommentDto) : IRequest<bool>;

public class CreateTaskCommentHandler : IRequestHandler<CreateTaskCommentCommand, bool>
{
    private readonly IRepository<TaskComments> _taskCommentRepository;
    public CreateTaskCommentHandler(IRepository<TaskComments> taskCommentRepository) => _taskCommentRepository = taskCommentRepository;
    public async Task<bool> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        var dto = request.TaskCommentDto;
        TaskComments taskComment = new TaskComments(dto.TaskItemId, dto.Content);
        //var taskComment=new Domain.Models.Task.TaskComment( request.TaskCommentDto.TaskItemId,
        //    request.TaskCommentDto.Content,request.TaskCommentDto.CreatedById);
        await _taskCommentRepository.Insert(taskComment);
        return true;

    }
}
