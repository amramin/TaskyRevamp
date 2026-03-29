using TaskyRevamp.Domain.Models.Task;

namespace TaskyRevamp.Domain.Services;

/// <summary>
/// Encapsulates weight and progress computation that was previously embedded in TaskItem properties.
/// Extracted from TaskItem to reduce the god class (HI-05).
/// This is a pure domain calculation — no dependencies on infrastructure.
/// </summary>
public static class TaskWeightCalculator
{
    /// <summary>
    /// Calculates the planned weight. For parent tasks, averages subtask planned weights.
    /// For leaf tasks, returns the stored planned weight.
    /// </summary>
    public static Weight CalculatePlannedWeight(IReadOnlyCollection<TaskItem> subtasks, Weight currentPlannedWeight)
    {
        if (subtasks.Any())
        {
            double averageWeight = subtasks.Average(st => st.PlannedWeight.Value);
            int roundedWeight = (int)Math.Round(averageWeight, MidpointRounding.AwayFromZero);
            int finalWeight = Math.Min(100, roundedWeight);
            return new Weight(finalWeight);
        }

        return currentPlannedWeight;
    }

    /// <summary>
    /// Computes the value to store for planned weight based on progress and weight.
    /// Used when setting PlannedWeight on a leaf task.
    /// </summary>
    public static Weight ComputePlannedWeightValue(int? weight, Progress plannedProgress)
    {
        if (!weight.HasValue)
            return new Weight(0);

        var countWeight = (double)(plannedProgress.Percentage * weight) / 100;
        var roundedValue = (int)Math.Round(countWeight, MidpointRounding.AwayFromZero);
        return new Weight(roundedValue);
    }

    /// <summary>
    /// Calculates the actual weight. For parent tasks, averages subtask actual weights.
    /// For leaf tasks, returns the stored actual weight.
    /// </summary>
    public static Weight CalculateActualWeight(IReadOnlyCollection<TaskItem> subtasks, Weight currentActualWeight)
    {
        if (subtasks.Any())
        {
            double averageWeight = subtasks.Average(st => st.ActualWeight.Value);
            int roundedWeight = (int)Math.Round(averageWeight, MidpointRounding.AwayFromZero);
            int finalWeight = Math.Min(100, roundedWeight);
            return new Weight(finalWeight);
        }

        return currentActualWeight;
    }

    /// <summary>
    /// Computes the value to store for actual weight based on progress and weight.
    /// Used when setting ActualWeight on a leaf task.
    /// </summary>
    public static Weight ComputeActualWeightValue(int? weight, int progress)
    {
        if (!weight.HasValue)
            return new Weight(0);

        var countWeight = (double)(progress * weight) / 100;
        var roundedValue = (int)Math.Round(countWeight, MidpointRounding.AwayFromZero);
        return new Weight(roundedValue);
    }

    /// <summary>
    /// Calculates planned progress. For parent tasks, averages subtask planned progress.
    /// For leaf tasks, calculates time-based progress from start date and duration.
    /// </summary>
    public static Progress CalculatePlannedProgress(IReadOnlyCollection<TaskItem> subtasks, DateTime? startDate, int duration)
    {
        if (subtasks.Any())
        {
            double averageProgress = subtasks.Average(st => st.PlannedProgress.Percentage);
            int roundedProgress = (int)Math.Round(averageProgress, MidpointRounding.AwayFromZero);
            int finalProgress = Math.Min(100, roundedProgress);
            return new Progress(finalProgress);
        }

        var today = DateTime.UtcNow.Date;
        var start = startDate!.Value.Date;

        if (today < start) return new Progress(0);

        int timeElapsed = (today - start).Days + 1;
        double percentage = (double)Math.Min(timeElapsed, duration) / duration * 100.0;
        int roundedPercentage = (int)Math.Round(percentage, MidpointRounding.AwayFromZero);
        return new Progress(roundedPercentage);
    }

    /// <summary>
    /// Calculates actual progress. For parent tasks, averages subtask actual progress.
    /// For leaf tasks, returns the stored actual progress.
    /// </summary>
    public static Progress CalculateActualProgress(IReadOnlyCollection<TaskItem> subtasks, Progress currentActualProgress)
    {
        if (!subtasks.Any())
            return currentActualProgress;

        double averageProgress = subtasks.Average(st => st.ActualProgress.Percentage);
        int roundedProgress = (int)Math.Round(averageProgress, MidpointRounding.AwayFromZero);
        int finalProgress = Math.Min(100, roundedProgress);
        return new Progress(finalProgress);
    }
}
