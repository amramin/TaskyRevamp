using DocumentFormat.OpenXml.Vml.Office;
using MediatR;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query;

public record GetTaskQuery(Guid Id) : IRequest<CreateTaskDto>;

public class GetTaskByIdHandler : IRequestHandler<GetTaskQuery, CreateTaskDto>
{
 
    private readonly ITaskRepository _taskRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Department> _departmentRepository;
	private string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
	public GetTaskByIdHandler(ITaskRepository taskRepository, IRepository<User> userRepository, IRepository<Department> departmentRepository)
	{
		_taskRepository = taskRepository;
		_userRepository = userRepository;
		_departmentRepository = departmentRepository;
	}

	public async Task<CreateTaskDto> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var res =  await _taskRepository.GetTaskById(request.Id);
        if (res is null)
        {
            throw new Exception("Task not found");
        }
        var taskDto = res.CopyToDto();
		taskDto.TypeName = currentCulture == "ar" ? res.Type?.NameArabic : res.Type?.NameEnglish;
		taskDto.SourceName = currentCulture == "ar" ? res.Source?.NameArabic : res.Source?.NameEnglish;
		taskDto.PriorityName = currentCulture == "ar" ? res.Priority?.NameArabic : res.Priority?.NameEnglish;
		taskDto.PriorityBackgroundColor =  res.Priority?.BackgroundColor;
		taskDto.PriorityColor = res.Priority?.NameColor;
		taskDto.TaskStatusName = currentCulture == "ar" ? res.status?.NameArabic : res.status?.NameEnglish;
		taskDto.TaskStatusColor = res.status?.NameColor;
		taskDto.TaskStatusBackgroundColor = res.status?.BackgroundColor;
		taskDto.CreatedByName = currentCulture == "ar" ? res.CreatedBy?.NameArabic : res.CreatedBy?.NameEnglish;
		taskDto.UpdatedBy = currentCulture == "ar" ? res.UpdatedBy?.NameArabic : res.UpdatedBy?.NameEnglish;
		taskDto.CreatorDepartment = currentCulture == "ar" ? res.CreatedBy?.Department?.NameArabic : res.CreatedBy?.Department?.NameEnglish;

		var assignedUserIds = res.AssignedIds ?? res.Assignees?.Select(a => a.User.Id).ToList() ?? new List<Guid>();
		List<User> assignedUsers;
		if (res.Assignees != null && res.Assignees.Any() && res.Assignees.First().User != null)
		{
			assignedUsers = res.Assignees.Select(a => a.User).Where(u => u != null).ToList();
		}
		else
		{
			var findRes = await _userRepository.FindBy(u => assignedUserIds.Contains(u.Id));
			assignedUsers = findRes.Value!.ToList();
		}
		var orderedUsers = assignedUserIds.Select(id => assignedUsers.FirstOrDefault(u => u.Id == id)).Where(u => u != null).ToList();
		var fullNamesList = orderedUsers.Select(u => { 
			var full = currentCulture == "ar"? u!.NameArabic ?? u.NameEnglish: u!.NameEnglish ?? u.NameArabic;
			return full;
		}).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
		taskDto.AssigneduserNames = string.Join(",", fullNamesList);

		var departments = await _departmentRepository.FindBy(d => res.AssignedDepartmentIds.Contains(d.Id));
		var deptDict = departments.Value!.ToDictionary(d => d.Id, d => currentCulture == "ar" ? d.NameArabic : d.NameEnglish);
		var orderedDeptNames = res.AssignedDepartmentIds.Where(id => deptDict.ContainsKey(id)).Select(id => deptDict[id]).Distinct().ToList();
		taskDto.AssignedDepartmentName = string.Join(", ", orderedDeptNames);

		if (res.Dependencies != null)
			taskDto.Dependencies = res.Dependencies is TaskDependencies td? td.Items.Select(i => i.Id).ToList(): taskDto.Dependencies;

		return taskDto;
    }
}