using System.Collections.Generic;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskAssignees
{
    private readonly TaskItem _task;
    private readonly List<User> _items = new();
    public IReadOnlyCollection<User> Items => _items.AsReadOnly();
    public IEnumerable<Department> Departments => _items.Select(u => u.Department).Distinct();

    internal TaskAssignees(TaskItem task)
    {
        _task = task;
    }

    public void Add(User user, User by)
    {
        if (!_items.Contains(user))
        {
            _items.Add(user);
            _task.AddHistoryEntry(by, $"assigned task to {user.Username}");
        }
    }

    public void Remove(User user, User by)
    {
        if (_items.Remove(user))
        {
            _task.AddHistoryEntry(by, $"removed assignee {user.Username}");
        }
    }
    
    internal void ClearAndAdd(User user, User by)
    {
        _items.Clear();
        _items.Add(user);
        _task.AddHistoryEntry(by, $"reassigned task to {user.Username} upon restore");
    }
}