using DocumentFormat.OpenXml.Office2010.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Interfaces.Services;
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
        private readonly ITaskRejectionService _rejectionService;

        public RejectTaskCommandHandler(ITaskRepository taskRepository, IHttpContextAccessor httpContextAccessor,
            IRepository<TaskComments> taskCommentRepository, IRepository<RejectionSettings> rejectonRepository,
            IRepository<TaskAssignee> taskAssigneeRepository, ITaskRejectionService rejectionService)
        {
            _taskRepository = taskRepository;
            _taskCommentRepository = taskCommentRepository;
            _rejectionRepository = rejectonRepository;
            _httpContextAccessor = httpContextAccessor;
            _taskAssigneeRepository = taskAssigneeRepository;
            _rejectionService = rejectionService;
        }
        public async Task<bool> Handle(RejectTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskById(request.TaskCommentDto.TaskItemId);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            var rejectionSettingsResult = await _rejectionRepository.AllAsNoTracking();
            var settings = rejectionSettingsResult.Value?.FirstOrDefault();
            int rejectionPeriod = _rejectionService.GetRejectionPeriodDays(settings);

            if (!_rejectionService.CanRejectTask(task.CreateDate, rejectionPeriod))
            {
                if (rejectionPeriod == (int)RejectionPeriodType.Never)
                    return false;

                throw new Exception("Task Cannot Be Rejected");
            }

            if (task.TaskAssignees.Count() == 1)
            {
                var assignee = new TaskAssignee
                {
                    TaskItemId = request.TaskCommentDto.TaskItemId,
                    UserId = task.CreatedById,
                    AssigneeDate = DateTime.Now
                };
                await _taskAssigneeRepository.Delete(task.TaskAssignees.FirstOrDefault().Id);
                await _taskAssigneeRepository.Insert(assignee);
                await _taskAssigneeRepository.SaveChangesAsync();
            }
            else
            {
                var currentUserId = Guid.Parse(_httpContextAccessor.GetUserId());
                _taskAssigneeRepository.Delete(task.TaskAssignees.FirstOrDefault(u => u.UserId == currentUserId).Id);
                await _taskAssigneeRepository.SaveChangesAsync();
            }
            TaskComments taskComment = new TaskComments(request.TaskCommentDto.TaskItemId, request.TaskCommentDto.Content, Guid.Parse(_httpContextAccessor.GetUserId()), request.TaskCommentDto.Type);
            await _taskCommentRepository.Insert(taskComment);

            return true;
        }
    }
}
