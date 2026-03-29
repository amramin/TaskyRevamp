namespace TaskyRevamp.Domain.Interfaces.Services;

/// <summary>
/// Determines the correct task status based on progress, dates, and dependencies.
/// Extracted from CreateTaskCommand, UpdateTaskCommand, and ChangeTaskProgressCommand
/// to eliminate duplicated status-determination logic (HI-01 / HI-02).
/// </summary>
public interface ITaskStatusDeterminer
{
    /// <summary>
    /// Determines the appropriate status GUID for a task based on its current state.
    /// </summary>
    /// <param name="progress">The task's progress percentage (0-100).</param>
    /// <param name="startDate">The task's start date.</param>
    /// <param name="endDate">The task's end date.</param>
    /// <param name="currentStatusId">The task's current status GUID (used for delayed-status preservation).</param>
    /// <returns>The determined status GUID.</returns>
    Guid DetermineStatus(int progress, DateTime? startDate, DateTime? endDate, Guid? currentStatusId = null);

    /// <summary>
    /// Validates that all dependent tasks are completed.
    /// </summary>
    /// <param name="dependencyTaskIds">The IDs of tasks this task depends on.</param>
    /// <returns>True if all dependencies are completed; false otherwise.</returns>
    Task<bool> AreDependenciesCompleted(List<Guid> dependencyTaskIds);
}
