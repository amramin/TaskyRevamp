using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class ChecklistItem : Entity
{
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public User CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ChecklistItem(Guid id, string desc, User by)
    {
        Id = id;
        Description = desc;
        CreatedBy = by;
        CreatedAt = DateTime.UtcNow;
    }
    public void UpdateText(string newDesc, User by)
    {
        Description = newDesc;
    }
    public void MarkComplete(User by)
    {
        IsCompleted = true;
    }
}