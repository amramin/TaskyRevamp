using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Services;

/// <summary>
/// Enriches a raw CreateTaskDto with localized names, department info, dependency info, etc.
/// Extracted from GetTasksQuery to reduce the god handler (HI-01).
/// </summary>
public class TaskDtoEnricher
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<TaskDependencies> _taskDependenciesRepository;

    public TaskDtoEnricher(
        IRepository<Department> departmentRepository,
        IRepository<TaskDependencies> taskDependenciesRepository)
    {
        _departmentRepository = departmentRepository;
        _taskDependenciesRepository = taskDependenciesRepository;
    }

    /// <summary>
    /// Enriches a CreateTaskDto with localised display names, department names, dependency info, etc.
    /// </summary>
    public async Task<CreateTaskDto> EnrichAsync(TaskItem task, CreateTaskDto dto, string currentCulture, IRepository<TaskItem> taskRepository)
    {
        // Creator department
        var creatorDepartment = await _departmentRepository.FirstOrDefaultAsNoTrackingAsync(
            k => k.Id == (task.CreatedBy!.DepartmentId ?? Guid.Empty));

        // Localized type/source/priority/status names
        dto.TypeName = currentCulture == "ar" ? task.Type?.NameArabic ?? "" : task.Type?.NameEnglish ?? "";
        dto.SourceName = currentCulture == "ar" ? task.Source?.NameArabic ?? "" : task.Source?.NameEnglish ?? "";
        dto.PriorityName = currentCulture == "ar" ? task.Priority?.NameArabic ?? "" : task.Priority?.NameEnglish ?? "";
        dto.PriorityBackgroundColor = task.Priority?.BackgroundColor;
        dto.PriorityColor = task.Priority?.NameColor;

        // Assigned departments
        var departments = await _departmentRepository.FindBy(k => task.AssignedDepartmentIds.Contains(k.Id));
        var depsName = currentCulture == "ar"
            ? departments.Value!.Select(k => k.NameArabic)
            : departments.Value!.Select(k => k.NameEnglish);
        dto.AssignedDepartmentName = string.Join(", ", depsName);

        // Dependencies
        var dependencies = await _taskDependenciesRepository.FindBy(p => p.TaskItemId == dto.Id);
        if (dependencies is not null)
        {
            var dependencyIds = dependencies.Value!.Select(p => p.DependentId);
            var tasksDependent = await taskRepository.FindBy(p => dependencyIds.Contains(p.Id));
            dto.DependencyNames = string.Join(", ", tasksDependent.Value!.Select(d => d.Title));
            dto.Dependencies = dependencyIds.ToList();
        }

        // Change requests, status, creator, assignees
        dto.ChangeEndDateRequestCount = task.ChangeEndDateRequests?.Count(c => c.Status == ChangeRequestStatus.Pending) ?? 0;
        dto.TaskStatusName = currentCulture == "ar" ? task.status?.NameArabic ?? "" : task.status?.NameEnglish ?? "";
        dto.TaskStatusBackgroundColor = task.status?.BackgroundColor;
        dto.TaskStatusColor = task.status?.NameColor;
        dto.CreatedByName = currentCulture == "ar" ? task.CreatedBy?.NameArabic : task.CreatedBy?.NameEnglish;
        dto.Createdbydepartment = currentCulture == "ar" ? creatorDepartment?.NameArabic! : creatorDepartment?.NameEnglish!;
        dto.UpdatedBy = currentCulture == "ar" ? task.UpdatedBy?.NameArabic : task.UpdatedBy?.NameEnglish;
        dto.AssigneduserNames = string.Join(",",
            task.TaskAssignees!.Select(u => currentCulture == "ar" ? u.User.NameArabic ?? "" : u.User.NameEnglish ?? ""));

        return dto;
    }
}
