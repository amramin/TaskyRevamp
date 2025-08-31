using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class Department : Entity
{
    public string Name { get;  set; }
    public Department(Guid id, string name, User by)
    {
        Id = id;
        Name = name;
    }

    public Department()
    {
    }

    public void Update(string name, User by)
    {
        Name = name;
        
    }
}