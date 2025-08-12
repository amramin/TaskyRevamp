using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskSource : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public TaskSource(Guid id, string name, string? desc, bool isActive, User by)
    {
        Id = id;
        Name = name;
        Description = desc;
        IsActive = isActive;
    }

    public TaskSource()
    {
    }

    public void Update(string name, string? desc, bool isActive, User by)
    {
        Name = name;
        Description = desc;
        IsActive = isActive;
        
    }
}