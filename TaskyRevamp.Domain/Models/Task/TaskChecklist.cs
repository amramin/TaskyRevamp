using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskChecklist
{
    private readonly TaskItem _task;
    private readonly List<ChecklistItem> _items = new();
    public IReadOnlyCollection<ChecklistItem> Items => _items.AsReadOnly();

    internal TaskChecklist(TaskItem task)
    {
        _task = task;
    }

    public void AddItem(string text, User by)
    {
        var item = new ChecklistItem(Guid.NewGuid(), text, by);
        _items.Add(item);
        _task.AddHistoryEntry(by, $"added checklist item '{text}'");
    }

    public void EditItem(Guid itemId, string newText, User by)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId) ?? throw new KeyNotFoundException();
        item.UpdateText(newText, by);
        _task.AddHistoryEntry(by, $"edited checklist item '{newText}'");
    }

    public void DeleteItem(Guid itemId, User by)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
            _task.AddHistoryEntry(by, $"deleted checklist item '{item.Description}'");
        }
    }
}