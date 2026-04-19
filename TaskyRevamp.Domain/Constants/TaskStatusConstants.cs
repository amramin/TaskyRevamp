namespace TaskyRevamp.Domain.Constants;

/// <summary>
/// Well-known task status GUIDs matching the seeded StatusSettings records.
/// Use these constants instead of hardcoding Guid.Parse("...") throughout the codebase.
/// </summary>
public static class TaskStatusConstants
{
    /// <summary>Task has not started yet and the start date is in the future.</summary>
    public static readonly Guid NotStarted = Guid.Parse("547022EA-EF8C-4FBC-2236-08DE3318A61C");

    /// <summary>Task work has begun (progress 0, start date reached).</summary>
    public static readonly Guid InProgress = Guid.Parse("9843AF9D-1389-4740-B428-08DE3D8A77AB");

    /// <summary>Task has partial progress (1-99%).</summary>
    public static readonly Guid PartiallyCompleted = Guid.Parse("753404A6-8B18-43F7-2238-08DE3318A61C");

    /// <summary>Task end date has passed without full completion.</summary>
    public static readonly Guid Delayed = Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C");

    /// <summary>Task progress reached 100% (pending final approval/completion).</summary>
    public static readonly Guid Done = Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C");

    /// <summary>Task has been reopened after being Done.</summary>
    public static readonly Guid Reopened = Guid.Parse("e1319fc1-8cb8-495c-223b-08de3318a61c");

    /// <summary>Task has been fully completed and approved.</summary>
    public static readonly Guid Completed = Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C");

    /// <summary>Task has been soft-deleted (moved to recycle bin).</summary>
    public static readonly Guid Deleted = Guid.Parse("D8E94CCE-586A-46D3-223D-08DE3318A61C");
}
