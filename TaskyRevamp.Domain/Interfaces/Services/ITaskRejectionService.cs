using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Domain.Interfaces.Services;

/// <summary>
/// Encapsulates task rejection business rules that were previously inline in RejectTaskCommand (HI-04).
/// Determines whether a task can be rejected based on the configured rejection period.
/// </summary>
public interface ITaskRejectionService
{
    /// <summary>
    /// Calculates the rejection period in days from the given settings.
    /// Returns 0 (Never) when rejection is disabled.
    /// </summary>
    int GetRejectionPeriodDays(RejectionSettings settings);

    /// <summary>
    /// Returns true if the task can still be rejected based on the assignee date
    /// and the configured rejection period.
    /// </summary>
    bool CanRejectTask(DateTime referenceDate, int rejectionPeriodDays);
}
