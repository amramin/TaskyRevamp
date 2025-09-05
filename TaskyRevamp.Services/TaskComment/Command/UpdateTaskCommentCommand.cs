using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;



namespace TaskCommentyRevamp.Services.TaskComments.Commands;

public record UpdateTaskCommentCommand(TaskCommentDto TaskComment) : IRequest<bool>;

public class UpdateTaskCommentCommandHandler : IRequestHandler<UpdateTaskCommentCommand, bool>
{
    private readonly IRepository<TaskComment> _TaskCommentRepository;

    public UpdateTaskCommentCommandHandler(IRepository<TaskComment> TaskCommentRepository)
    {
        _TaskCommentRepository = TaskCommentRepository;
    }

    public async Task<bool> Handle(UpdateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        var TaskCommentResponse = await _TaskCommentRepository.FindByKey(request.TaskComment.Id);
        if (!TaskCommentResponse.Success)
        {
            return false;
        }
        var updated = TaskCommentResponse.Value;
        updated.SetData(request.TaskComment);
        await _TaskCommentRepository.Update(updated);

        return true;
    }
}