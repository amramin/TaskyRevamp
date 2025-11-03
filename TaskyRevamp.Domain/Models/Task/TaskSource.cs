using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskSourceDto;
using TaskyRevamp.Dto.TaskTypeDto;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskSource : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
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
    public TaskSourceDto CopyToDto()
    {
        return new TaskSourceDto
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