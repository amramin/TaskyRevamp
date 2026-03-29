using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain;
using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.TaskAttachments.Command;
using TaskAttachment = TaskyRevamp.Domain.Models.Task.TaskAttachments;
namespace TaskyRevamp.Services.Tasks.Commands;
using TaskComments = TaskyRevamp.Domain.Models.Task.TaskComment;

public record CreateTaskCommand(CreateTaskDto CreateTaskDto) : IRequest<string>;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, string>
{
	private readonly IRepository<TaskItem> _taskRepository;
	private readonly IRepository<StatusSettings> _statusSettings;
	private readonly IRepository<User> _userRepository;
	private readonly IRepository<Department> _depRepository;
	private readonly IRepository<TaskComments> _taskCommentRepository;
	private readonly IRepository<TaskChecklist> _taskChecklistRepository;

	public CreateTaskHandler(IRepository<TaskItem> taskRepository, IRepository<User> userRepository, IRepository<StatusSettings> statusSettings,
		IRepository<Department> depRepository, IRepository<TaskComments> taskCommentRepository, IRepository<TaskChecklist> taskChecklistRepository)
	{
		_taskRepository = taskRepository;
		_userRepository = userRepository;
		_depRepository = depRepository;
		_statusSettings = statusSettings;
		_taskCommentRepository = taskCommentRepository;
		_taskChecklistRepository = taskChecklistRepository;
	}
	public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.FindByKey(request.CreateTaskDto.CreatedBy.Value);
		if (user is null || user.IsFailure || user.Value is null)
			throw new Exception("UserNotFound");
		var TaskSatuses = await _statusSettings.All();
		var departments = await _depRepository.FindBy(k => request.CreateTaskDto.AssignedDepartmentIds.Contains(k.Id));
		if (departments is null || departments.IsFailure || departments.Value is null)
			throw new Exception("DepartmentsNotFound");
		if (request.CreateTaskDto.ActualProcess == 100 && request.CreateTaskDto.Dependencies is not null && request.CreateTaskDto.Dependencies.Count > 0)
		{
			var dependencies = request.CreateTaskDto.Dependencies?.Select(p => p).ToList();
			var tasksnotcompleted = await _taskRepository.FindBy(d => dependencies.Contains(d.Id));
			if (tasksnotcompleted.Value.Any(p => p.StatusId != TaskStatusConstants.Completed))
			{
				throw new Exception("DependencyError");
			}
		}
		var weight = request.CreateTaskDto.weight ?? 0;
		var task = new TaskItem(request.CreateTaskDto.Id, request.CreateTaskDto.Title,
			 request.CreateTaskDto.Description!,
			 request.CreateTaskDto.TypeId, request.CreateTaskDto.SourceId,
			request.CreateTaskDto.StartDate, request.CreateTaskDto.EndDate,
		 request.CreateTaskDto.Priority
			, new Weight(weight), user.Value.Id, departments.Value.ToList(),
			request.CreateTaskDto.AssignedIds, request.CreateTaskDto.ReminderDate, request.CreateTaskDto.ActualProcess, weight, request.CreateTaskDto.Dependencies);

		if (TaskSatuses is not null)
		{

            if (DateOnly.FromDateTime(request.CreateTaskDto.EndDate?.Date ?? default) < DateOnly.FromDateTime(DateTime.UtcNow.Date) && request.CreateTaskDto.ActualProcess < 100)
			{
				task.StatusId = TaskStatusConstants.Delayed;
			}
			else
			{
				if (request.CreateTaskDto.ActualProcess == 0 && (request.CreateTaskDto.StartDate > DateTime.Now))
				{
					task.StatusId = TaskStatusConstants.NotStarted;
				}
				else if (request.CreateTaskDto.ActualProcess == 0 && (request.CreateTaskDto.StartDate <= DateTime.Now))
				{
					task.StatusId = TaskStatusConstants.InProgress;
				}
				else if (request.CreateTaskDto.ActualProcess > 0 && request.CreateTaskDto.ActualProcess < 100)
				{
					task.StatusId = TaskStatusConstants.PartiallyCompleted;
				}
				else if (request.CreateTaskDto.ActualProcess == 100)
				{
					task.StatusId = TaskStatusConstants.Done;
				}
			}
		}
		await _taskRepository.Insert(task);
		if (!string.IsNullOrEmpty(request.CreateTaskDto.Content))
		{
			TaskComments taskComment = new TaskComments(task.Id, request.CreateTaskDto.Content);
			await _taskCommentRepository.Insert(taskComment);
		}
		var taskChecklists = new List<TaskChecklist>();
		if (request.CreateTaskDto.Checklists is not null && request.CreateTaskDto.Checklists.Count > 0)
		{
			foreach (var checklist in request.CreateTaskDto.Checklists)
			{
				var taskChecklist = new TaskChecklist(task.Id, checklist.Title);
				if (checklist.Items != null && checklist.Items.Any())
				{
					foreach (var item in checklist.Items)
						taskChecklist.items.Add(new ChecklistItem(item.Title, taskChecklist.Id, item.AssignedUserId, item.EndDate, item.IsDone));
				}
				taskChecklists.Add(taskChecklist);
			}
			await _taskChecklistRepository.InsertRange(taskChecklists);
		}
		return task.Id.ToString();
	}
}