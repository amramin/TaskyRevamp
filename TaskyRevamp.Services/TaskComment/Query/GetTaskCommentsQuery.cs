using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;

namespace TaskyRevamp.Services.TaskComment.Query;

public record GetTaskCommentsQuery(Guid id) : IRequest<List<TaskCommentWithNameDto>>;

public class GetTaskCommentsHandler : IRequestHandler<GetTaskCommentsQuery, List<TaskCommentWithNameDto>>
{
    private readonly IRepository<TaskComments> _taskCommentRepository;
    public GetTaskCommentsHandler(IRepository<TaskComments> taskCommentRepository)
    {
        _taskCommentRepository = taskCommentRepository;
      
    }
    public async Task<List<TaskCommentWithNameDto>> Handle(GetTaskCommentsQuery request, CancellationToken cancellationToken)
    {
        var taskComments = new List<TaskCommentWithNameDto>();
        var res = await _taskCommentRepository.FindBy(k => k.TaskItemId == request.id, includeProperties: $"{nameof(TaskComments.CreatedBy)},{nameof(TaskComments.UpdatedBy)}");

		if(res.Success && res.Value != null && res.Value.Any())
        {
            foreach (var comment in res.Value)
            {
                var commentDto = new TaskCommentWithNameDto
                {
                    TaskComment = comment.CopyToDto(),
                    CreatedByName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar"
                        ? comment.CreatedBy?.NameArabic
                        : comment.CreatedBy?.NameEnglish,
                    UpdatedByName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar"
                        ? comment.UpdatedBy?.NameArabic
                        : comment.UpdatedBy?.NameEnglish
				};
                taskComments.Add(commentDto);
			}
		}
		return  taskComments;
    }
}