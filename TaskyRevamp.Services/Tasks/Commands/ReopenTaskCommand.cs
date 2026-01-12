using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskComment;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record ReopenTaskCommand(TaskCommentDto TaskCommentDto) : IRequest<bool>;

    public class ReopenTaskCommandHandler : IRequestHandler<ReopenTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<StatusSettings> _statusSettings;
        private readonly IRepository<TaskComments> _taskCommentRepository;
        public ReopenTaskCommandHandler(ITaskRepository taskRepository, IRepository<StatusSettings> statusSettings, IRepository<TaskComments> taskCommentRepository)
        {
            _taskRepository = taskRepository;
            _statusSettings = statusSettings;
            _taskCommentRepository = taskCommentRepository;
        }
        public async Task<bool> Handle(ReopenTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskById(request.TaskCommentDto.TaskItemId);
            var TaskSatuses = await _statusSettings.All();
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            if (task.StatusId == TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Pending review").Id)
            {
                task.StatusId = TaskSatuses.Value.FirstOrDefault(s => s.NameEnglish == "Reopened").Id;
                task.Progress = 50;
                await _taskRepository.UpdateTask(task);
                TaskComments taskComment = new TaskComments(request.TaskCommentDto.TaskItemId, request.TaskCommentDto.Content);
                //var taskComment=new Domain.Models.Task.TaskComment( request.TaskCommentDto.TaskItemId,
                //    request.TaskCommentDto.Content,request.TaskCommentDto.CreatedById);
                await _taskCommentRepository.Insert(taskComment);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
