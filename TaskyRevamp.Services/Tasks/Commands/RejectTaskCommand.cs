using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.TaskComment;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record RejectTaskCommand(TaskCommentDto TaskCommentDto) : IRequest<bool>;

    public class RejectTaskCommandHandler : IRequestHandler<RejectTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<TaskComments> _taskCommentRepository;
        private readonly IRepository<RejectionSettings> _rejectionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RejectTaskCommandHandler(ITaskRepository taskRepository, IHttpContextAccessor httpContextAccessor, IRepository<TaskComments> taskCommentRepository,IRepository<RejectionSettings> rejectonRepository)
        {
            _taskRepository = taskRepository;
            _taskCommentRepository = taskCommentRepository;
            _rejectionRepository = rejectonRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(RejectTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskById(request.TaskCommentDto.TaskItemId);
            var RejectionSettings = await _rejectionRepository.AllAsNoTracking();
            int RejectionPeriod = 0;
            if (RejectionSettings.Value.FirstOrDefault() is not null)
            {
                if (RejectionSettings.Value.FirstOrDefault().CustomDays != null)
                {
                    RejectionPeriod = (int)RejectionSettings.Value.FirstOrDefault().CustomDays;
                }
                else
                {
                    RejectionPeriod = (int)RejectionSettings.Value.FirstOrDefault().PeriodType;
                }
            }
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            if (RejectionPeriod!=(int)RejectionPeriodType.Never)
            {
                if (DateOnly.FromDateTime(task.CreateDate).AddDays(RejectionPeriod) < DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new Exception("Task Cannot Be Rejected");
                }
                else
                {
                    if (task.AssignedIds.Count() == 1)
                    {
                        task.AssignedIds = new List<Guid> { task.CreatedById };
                    }
                    else
                    {
                        var currentUserId = Guid.Parse(_httpContextAccessor.GetUserId());
                        task.AssignedIds.Remove(currentUserId);
                    }
                     await _taskRepository.UpdateTask(task);
                    TaskComments taskComment = new TaskComments(request.TaskCommentDto.TaskItemId, request.TaskCommentDto.Content, Guid.Parse(_httpContextAccessor.GetUserId()),request.TaskCommentDto.Type);
                    //var taskComment=new Domain.Models.Task.TaskComment( request.TaskCommentDto.TaskItemId,
                    //    request.TaskCommentDto.Content,request.TaskCommentDto.CreatedById);
                    await _taskCommentRepository.Insert(taskComment);
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
