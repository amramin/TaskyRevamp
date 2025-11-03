using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Dto.TaskTypeDto;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskType : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public TaskType(Guid id, string name, string? desc, bool isActive, User by)
    {
        Id = id;
        Name = name;
        Description = desc;
        IsActive = isActive;
    }

    public TaskType()
    {
    }
    public TaskTypeDto CopyToDto()
    {
        return new TaskTypeDto
        {
            Id = Id,
            Description = Description,
            IsActive = IsActive,
            Name = Name




        };
    }
    public void Update(string name, string? desc, bool isActive, User by)
    {
        Name = name;
        Description = desc;
        IsActive = isActive;

    }
}