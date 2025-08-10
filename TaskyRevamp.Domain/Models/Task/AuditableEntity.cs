using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; protected set; }
    public User CreatedBy { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public User? UpdatedBy { get; protected set; }

    protected void SetCreated(User by)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = by;
    }

    protected void SetUpdated(User by)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = by;
    }
}