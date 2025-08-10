using System.Collections.Generic;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskDependencies
{
    private readonly TaskItem _task;
    private readonly List<TaskItem> _items = new();
    public IReadOnlyCollection<TaskItem> Items => _items.AsReadOnly();

    internal TaskDependencies(TaskItem task)
    {
        _task = task;
    }

    public void Add(TaskItem dependency, User by)
    {
        if (dependency.Id == _task.Id) throw new InvalidOperationException("A task cannot depend on itself.");
        if (WouldCreateCircularDependency(dependency)) throw new InvalidOperationException("Adding this dependency would create a circular reference.");
        if (!_items.Contains(dependency))
        {
            _items.Add(dependency);
            _task.AddHistoryEntry(by, $"added dependency on task {dependency.Id}");
        }
    }

    public void Remove(TaskItem dependency, User by)
    {
        if (_items.Remove(dependency))
        {
            _task.AddHistoryEntry(by, $"removed dependency on task {dependency.Id}");
        }
    }

    private bool WouldCreateCircularDependency(TaskItem potentialDependency)
    {
        var queue = new Queue<TaskItem>();
        queue.Enqueue(potentialDependency);

        var visited = new HashSet<Guid> { potentialDependency.Id };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Id == _task.Id)
            {
                return true; // Found a path back to the original task.
            }

            foreach (var dep in current.Dependencies.Items)
            {
                if (visited.Add(dep.Id)) // .Add returns false if the item is already in the set
                {
                    queue.Enqueue(dep);
                }
            }
        }
        return false;
    }
}