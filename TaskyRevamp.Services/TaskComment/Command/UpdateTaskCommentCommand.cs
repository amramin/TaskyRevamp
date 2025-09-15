using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Services.TaskComment.Command;

public record UpdateTaskCommentCommand(TaskCommentDto TaskComment) : IRequest<bool>;

public class UpdateTaskCommentCommandHandler : IRequestHandler<UpdateTaskCommentCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.TaskComment> _taskCommentRepository;

    public UpdateTaskCommentCommandHandler(IRepository<Domain.Models.Task.TaskComment> taskCommentRepository)
    {
        _taskCommentRepository = taskCommentRepository;
    }

    public async Task<bool> Handle(UpdateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        var taskCommentResponse = await _taskCommentRepository.FindByKey(request.TaskComment.Id);
        if (!taskCommentResponse.Success)
        {
            return false;
        }
        var updated = taskCommentResponse.Value;
        updated.SetData(request.TaskComment);
        await _taskCommentRepository.Update(updated);

        return true;
    }
}