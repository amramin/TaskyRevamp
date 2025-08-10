using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskEscalation : Entity
{
    public TaskItem Task { get; private set; }
    public User EscalatedTo { get; private set; }
    public string Reason { get; private set; }
    public EscalationStatus Status { get; private set; }
    public User RequestedBy { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public TaskEscalation(Guid id, TaskItem task, User to, string reason, User by)
    {
        Id = id;
        Task = task;
        EscalatedTo = to;
        Reason = reason;
        RequestedBy = by;
        RequestedAt = DateTime.UtcNow;
        Status = EscalationStatus.Active;
    }
    public void Resolve(User by)
    {
        Status = EscalationStatus.Resolved;
    }
}