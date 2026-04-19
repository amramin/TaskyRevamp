using TaskyRevamp.Domain.Interfaces.Services;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Services.Tasks.Services;

/// <summary>
/// Centralised task-rejection business rules.
/// Extracted from RejectTaskCommand to satisfy HI-04 (business logic in service layer).
/// </summary>
public class TaskRejectionService : ITaskRejectionService
{
    /// <inheritdoc />
    public int GetRejectionPeriodDays(RejectionSettings settings)
    {
        if (settings is null)
            return (int)RejectionPeriodType.Never;

        return settings.CustomDays.HasValue
            ? (int)settings.CustomDays.Value
            : (int)settings.PeriodType;
    }

    /// <inheritdoc />
    public bool CanRejectTask(DateTime referenceDate, int rejectionPeriodDays)
    {
        if (rejectionPeriodDays == (int)RejectionPeriodType.Never)
            return false;

        return DateOnly.FromDateTime(referenceDate).AddDays(rejectionPeriodDays)
            >= DateOnly.FromDateTime(DateTime.Now);
    }
}
