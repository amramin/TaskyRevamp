using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
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
        private readonly IRepository<TaskAssignee> _taskAssigneeRepository;

        public RejectTaskCommandHandler(ITaskRepository taskRepository, IHttpContextAccessor httpContextAccessor, IRepository<TaskComments> taskCommentRepository,IRepository<RejectionSettings> rejectonRepository, IRepository<TaskAssignee> taskAssigneeRepository)
        {
            _taskRepository = taskRepository;
            _taskCommentRepository = taskCommentRepository;
            _rejectionRepository = rejectonRepository;
            _httpContextAccessor = httpContextAccessor;
            _taskAssigneeRepository = taskAssigneeRepository;
        }
        public async Task<bool> Handle(RejectTaskCommand request, CancellationToken cancellationToken)
        {
			var currentUserId = Guid.Parse(_httpContextAccessor.GetUserId());
			var task = await _taskRepository.GetTaskById(request.TaskCommentDto.TaskItemId);
            var RejectionSettings = await _rejectionRepository.AllAsNoTracking();
            int RejectionPeriod = 0;
            if (RejectionSettings.Value!.FirstOrDefault() is not null)
            {
                if (RejectionSettings.Value!.FirstOrDefault()!.CustomDays != null)
                {
                    RejectionPeriod = (int)RejectionSettings.Value!.FirstOrDefault()!.CustomDays!;
                }
                else
                {
                    RejectionPeriod = (int)RejectionSettings.Value!.FirstOrDefault()!.PeriodType;
                }
            }
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            if (RejectionPeriod!=(int)RejectionPeriodType.Never)
            {
                var taskAssignee = task.TaskAssignees.FirstOrDefault(u => u.UserId == currentUserId);
                if (taskAssignee == null)
                {
                    throw new Exception("User is not an assignee of the task");
                }
                else
                {
                    if (DateOnly.FromDateTime(taskAssignee.AssigneeDate).AddDays(RejectionPeriod) < DateOnly.FromDateTime(DateTime.Now))
                    {
                        throw new Exception("Task Cannot Be Rejected");
                    }
                    else
                    {
                        if (task.TaskAssignees.Count() == 1)
                        {
                            var assignee = new TaskAssignee
                            {
                                TaskItemId = request.TaskCommentDto.TaskItemId,
                                UserId = task.CreatedById,
                                AssigneeDate = DateTime.Now
                            };
                            await _taskAssigneeRepository.Delete(task.TaskAssignees.FirstOrDefault()!.Id);
                            await _taskAssigneeRepository.Insert(assignee);
                            await _taskAssigneeRepository.SaveChangesAsync();
                        }
                        else
                        {
                            await _taskAssigneeRepository.Delete(task.TaskAssignees.FirstOrDefault(u => u.UserId == currentUserId)!.Id);
                            await _taskAssigneeRepository.SaveChangesAsync();
                        }
                        TaskComments taskComment = new TaskComments(request.TaskCommentDto.TaskItemId, request.TaskCommentDto.Content, Guid.Parse(_httpContextAccessor.GetUserId()), request.TaskCommentDto.Type);
                        await _taskCommentRepository.Insert(taskComment);
                    }
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
