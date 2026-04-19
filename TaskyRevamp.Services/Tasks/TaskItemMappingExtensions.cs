using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.TaskAssignees;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks;

/// <summary>
/// Extension methods for mapping between TaskItem domain entity and CreateTaskDto.
/// Extracted from TaskItem to reduce the god class (HI-05) — DTO mapping is not a domain concern.
/// </summary>
public static class TaskItemMappingExtensions
{
    /// <summary>
    /// Maps a TaskItem domain entity to a CreateTaskDto.
    /// This was previously TaskItem.CopyToDto().
    /// </summary>
    public static CreateTaskDto ToDto(this TaskItem task)
    {
        return new CreateTaskDto
        {
            Id = task.Id,
            Description = task.Description,
            Title = task.Title,
            Plannedweight = task.PlannedWeight.Value,
            ActualWeight = task.ActualWeight.Value,
            weight = task.Weight,
            ActualProcess = task.Progress,
            PlannedProgress = task.PlannedProgress.Percentage,
            CreateDate = task.CreateDate,
            UpdateDate = task.UpdateDate,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            AssignedIds = task.TaskAssignees?.Select(u => u.UserId).ToList(),
            AssigneesData = task.TaskAssignees?.Select(u => new TaskAssigneeDataDto { UserId = u.UserId, AssigneeDate = u.AssigneeDate }).ToList(),
            DeletionDate = task.DeleteDate,
            DeletedBy = task.DeletedBy?.Username,
            Priority = task.PriorityId,
            CreatedByName = task.CreatedBy?.Username,
            UpdatedBy = task.UpdatedBy?.Username,
            ReminderDate = task.ReminderDate,
            TaskStatusName = task.status?.NameEnglish,
            TaskStatus = task.StatusId,
            SourceId = task.TaskSourceId,
            TypeId = task.TaskTypeId,
            AssignedDepartmentIds = task.AssignedDepartmentIds?.ToList() ?? new List<Guid>()
        };
    }

    /// <summary>
    /// Applies DTO values onto a TaskItem domain entity.
    /// This was previously TaskItem.SetData().
    /// </summary>
    public static void ApplyDto(this TaskItem task, CreateTaskDto dto)
    {
        task.Id = dto.Id;
        task.Description = dto.Description;
        task.Title = dto.Title;
        task.Progress = dto.ActualProcess;
        task.TaskSourceId = dto.SourceId;
        task.TaskTypeId = dto.TypeId;
        task.StartDate = dto.StartDate;
        task.EndDate = dto.EndDate;
        task.PriorityId = dto.Priority;
        task.Weight = dto.weight;
        if (task.Weight.HasValue)
            task.ActualWeight = new Weight(task.Weight.Value);
        else
            task.ActualWeight = new Weight(0);
        task.AssignedDepartmentIds = dto.AssignedDepartmentIds;
        task.ReminderDate = dto.ReminderDate;
        task.Dependencies = dto.Dependencies?.Select(d => new TaskDependencies { TaskItemId = dto.Id, DependentId = d }).ToList();
    }
}
