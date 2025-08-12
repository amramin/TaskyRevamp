using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskHistoryEntry : Entity
{
    public TaskItem Task { get; private set; }
    public User By { get; private set; }
    public string Action { get; private set; }
    public DateTime Timestamp { get; private set; }
    public TaskHistoryEntry(Guid id, TaskItem task, User by, string action, DateTime ts)
    {
        Id = id;
        Task = task;
        By = by;
        Action = action;
        Timestamp = ts;
    }

    public TaskHistoryEntry()
    {
    }
}