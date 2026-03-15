using DocumentFormat.OpenXml.Vml.Office;
using MediatR;
using Microsoft.AspNetCore.Http;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Dto.TaskViews;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTaskQuery(Guid Id, Guid currentUserId) : IRequest<CreateTaskDto>;

public class GetTaskByIdHandler : IRequestHandler<GetTaskQuery, CreateTaskDto>
{

    private readonly IRepository<TaskDependencies> _taskDependincesRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<TaskViews> _taskViewsRepository;
    private readonly IRepository<TaskItem> _taskitemRepository;
    private string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
	public GetTaskByIdHandler(ITaskRepository taskRepository, IRepository<User> userRepository,IRepository<TaskDependencies> taskDependincesRepository, IRepository<Department> departmentRepository, IRepository<TaskViews> taskViewsRepository, IRepository<TaskItem> taskitemrepo)
	{
		_taskRepository = taskRepository;
		_userRepository = userRepository;
		_departmentRepository = departmentRepository;
		_taskViewsRepository = taskViewsRepository;
		_taskDependincesRepository = taskDependincesRepository;
		_taskitemRepository = taskitemrepo;
    }

	public async Task<CreateTaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var res =  await _taskRepository.GetTaskById(request.Id);
        if (res is null)
        {
            throw new Exception("Task not found");
        }
		if (request.currentUserId != Guid.Empty && request.currentUserId != res.CreatedById)
		{
			await TrackTaskView(request.Id, request.currentUserId);
		}
        res.ActualWeight = new Weight(res.Weight ?? 0);
        res.PlannedWeight = new Weight(res.Weight ?? 0);
        var taskDto = res.CopyToDto();
        var dependencies = await _taskDependincesRepository.FindBy(p => p.TaskItemId == taskDto.Id);
        if (dependencies is not null)
        {
            var dependenciesids = dependencies.Value!.Select(p => p.DependentId);
            var tasksdependent = await _taskitemRepository.FindBy(p => dependenciesids.Contains(p.Id));
            var DependencyNames = string.Join(", ", tasksdependent.Value!.Select(d => d.Title));
            taskDto.DependencyNames = DependencyNames;
            taskDto.Dependencies = dependenciesids.ToList();
        }
        taskDto.TypeName = currentCulture == "ar" ? res.Type?.NameArabic : res.Type?.NameEnglish;
		taskDto.SourceName = currentCulture == "ar" ? res.Source?.NameArabic : res.Source?.NameEnglish;
		taskDto.PriorityName = currentCulture == "ar" ? res.Priority?.NameArabic : res.Priority?.NameEnglish;
		taskDto.PriorityBackgroundColor =  res.Priority?.BackgroundColor;
		taskDto.PriorityColor = res.Priority?.NameColor;
		taskDto.TaskStatusName = currentCulture == "ar" ? res.status?.NameArabic : res.status?.NameEnglish;
		taskDto.TaskStatusColor = res.status?.NameColor;
		taskDto.TaskStatusBackgroundColor = res.status?.BackgroundColor;
		taskDto.CreatedBy = res.CreatedById;
		taskDto.CreatedByName = currentCulture == "ar" ? res.CreatedBy?.NameArabic : res.CreatedBy?.NameEnglish;
		taskDto.UpdatedBy = currentCulture == "ar" ? res.UpdatedBy?.NameArabic : res.UpdatedBy?.NameEnglish;
		taskDto.CreatorDepartment = currentCulture == "ar" ? res.CreatedBy?.Department?.NameArabic : res.CreatedBy?.Department?.NameEnglish;
		taskDto.ChangeEndDateRequestCount = res.ChangeEndDateRequests?.Count(c => c.Status == ChangeRequestStatus.Pending)??0;
		//      var assignedUserIds = res.AssignedIds ?? res.Assignees?.Select(a => a.User.Id).ToList() ?? new List<Guid>();
		//List<User> assignedUsers;
		//if (res.Assignees != null && res.Assignees.Any() && res.Assignees.First().User != null)
		//{
		//	assignedUsers = res.Assignees.Select(a => a.User).Where(u => u != null).ToList();
		//}
		//else
		//{
		//	var findRes = await _userRepository.FindBy(u => assignedUserIds.Contains(u.Id));
		//	assignedUsers = findRes.Value!.ToList();
		//}
		//var orderedUsers = assignedUserIds.Select(id => assignedUsers.FirstOrDefault(u => u.Id == id)).Where(u => u != null).ToList();
		//var fullNamesList = orderedUsers.Select(u => { 
		//	var full = currentCulture == "ar"? u!.NameArabic ?? u.NameEnglish: u!.NameEnglish ?? u.NameArabic;
		//	return full;
		//}).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
		//taskDto.AssigneduserNames = string.Join(",", fullNamesList);
		taskDto.AssigneduserNames = string.Join(",", res?.TaskAssignees?.Select(u => currentCulture == "ar" ? u.User?.NameArabic ?? "" : u.User?.NameEnglish ?? "").ToList());

		var departments = await _departmentRepository.FindBy(d => res.AssignedDepartmentIds.Contains(d.Id));
		var deptDict = departments.Value!.ToDictionary(d => d.Id, d => currentCulture == "ar" ? d.NameArabic : d.NameEnglish);
		var orderedDeptNames = res.AssignedDepartmentIds.Where(id => deptDict.ContainsKey(id)).Select(id => deptDict[id]).Distinct().ToList();
		taskDto.AssignedDepartmentName = string.Join(", ", orderedDeptNames);

		//if (res.Dependencies != null)
		//	taskDto.Dependencies = res.Dependencies is TaskDependencies td? td.Items.Select(i => i.Id).ToList(): taskDto.Dependencies;

		if (request.currentUserId != Guid.Empty && request.currentUserId == res.CreatedById)
		{
			var taskViews = await _taskViewsRepository.FindBy(tv => tv.TaskItemId == request.Id, $"{nameof(TaskViews.User)}");
			if (taskViews.Success && taskViews.Value != null)
			{
				taskDto.ViewdByNames = taskViews.Value
					.Select(tv => new TaskViewsDto
					{
						Id = tv.Id,
						TaskItemId = tv.TaskItemId,
						UserId = tv.UserId,
						ViewdAt = tv.ViewedAt,
						FullName = currentCulture == "ar" ? (tv.User.NameArabic ?? tv.User.NameEnglish) : (tv.User.NameEnglish ?? tv.User.NameArabic),
						IsActive = tv.User.IsActive
					}).OrderByDescending(v => v.ViewdAt).ToList();
			}
		}
		return taskDto;
    }
	private async Task TrackTaskView(Guid taskId, Guid userId)
	{
		var existingView = await _taskViewsRepository.FindBy(
			tv => tv.TaskItemId == taskId && tv.UserId == userId
		);

		if (!existingView.Success || !existingView.Value!.Any())
		{
			var taskView = new TaskViews(taskId, userId);
			await _taskViewsRepository.Insert(taskView);
		}
		else
		{
			var view = existingView.Value!.First();
			view.ViewedAt = DateTime.UtcNow;
			await _taskViewsRepository.Update(view);
		}
	}
}