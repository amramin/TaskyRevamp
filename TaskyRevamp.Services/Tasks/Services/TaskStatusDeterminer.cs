using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Interfaces.Services;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Services;

/// <summary>
/// Centralised task-status determination logic.
/// Replaces the duplicated if/else chains that were copy-pasted across
/// CreateTaskCommand, UpdateTaskCommand, and ChangeTaskProgressCommand.
/// </summary>
public class TaskStatusDeterminer : ITaskStatusDeterminer
{
    private readonly IRepository<TaskItem> _taskRepository;

    public TaskStatusDeterminer(IRepository<TaskItem> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <inheritdoc />
    public Guid DetermineStatus(int progress, DateTime? startDate, DateTime? endDate, Guid? currentStatusId = null)
    {
        // Delayed: end date is past and task is not complete
        bool isPastEndDate = endDate.HasValue
            && DateOnly.FromDateTime(endDate.Value.Date) < DateOnly.FromDateTime(DateTime.UtcNow.Date);

        if (progress == 0)
        {
            if (isPastEndDate)
                return TaskStatusConstants.Delayed;

            if (startDate > DateTime.UtcNow)
                return TaskStatusConstants.NotStarted;

            return TaskStatusConstants.InProgress;
        }

        if (progress > 0 && progress < 100)
        {
            if (isPastEndDate)
                return TaskStatusConstants.Delayed;

            // Preserve existing Delayed status when changing progress (ChangeTaskProgressCommand behaviour)
            if (currentStatusId == TaskStatusConstants.Delayed)
                return TaskStatusConstants.Delayed;

            return TaskStatusConstants.PartiallyCompleted;
        }

        // progress == 100
        return TaskStatusConstants.Done;
    }

    /// <inheritdoc />
    public async Task<bool> AreDependenciesCompleted(List<Guid> dependencyTaskIds)
    {
        if (dependencyTaskIds is null || dependencyTaskIds.Count == 0)
            return true;

        var result = await _taskRepository.FindBy(d => dependencyTaskIds.Contains(d.Id));
        if (result.Value is null)
            return false;

        return result.Value.All(p => p.StatusId == TaskStatusConstants.Completed);
    }
}
