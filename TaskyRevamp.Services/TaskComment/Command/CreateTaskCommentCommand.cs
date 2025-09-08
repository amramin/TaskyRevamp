using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;

namespace TaskCommentyRevamp.Services.TaskComments.Commands;

public record CreateTaskCommentCommand(TaskCommentDto TaskCommentDto) : IRequest<Guid>;

public class CreateTaskCommentHandler : IRequestHandler<CreateTaskCommentCommand, Guid>
{
    private readonly IRepository<TaskComment> _TaskCommentRepository;

    public CreateTaskCommentHandler(IRepository<TaskComment> TaskCommentRepository) => _TaskCommentRepository = TaskCommentRepository;

    public async Task<Guid> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        TaskComment TaskComment=new TaskComment( request.TaskCommentDto.TaskItemId,
            request.TaskCommentDto.Content,request.TaskCommentDto.CreatedById);
 
       
        await _TaskCommentRepository.Insert(TaskComment);
        await _TaskCommentRepository.SaveChangesAsync();
        return TaskComment.Id;

    }
}
