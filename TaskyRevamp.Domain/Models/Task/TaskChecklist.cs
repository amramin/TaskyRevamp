using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskChecklist : Entity
{
    private readonly TaskItem _task;
    public string TitleEnglish { get; set; }
    public string TitleArabic { get; set; }
    private readonly List<ChecklistItem> _items = new();
    public IReadOnlyCollection<ChecklistItem> Items => _items.AsReadOnly();

    internal TaskChecklist(TaskItem task)
    {
        _task = task;
    }

    public TaskChecklist()
    {
    }

    public void AddItem(string textEN,string textAR, User by)
    {
        var item = new ChecklistItem(Guid.NewGuid(), textEN,textAR, by);
        _items.Add(item);
        _task.AddHistoryEntry(by, $"added checklist item '{textEN}''{textEN}'");
    }

    public void EditItem(Guid itemId, string newTextEN,string newtextAR, User by)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId) ?? throw new KeyNotFoundException();
        item.UpdateText(newTextEN,newtextAR, by);
        _task.AddHistoryEntry(by, $"edited checklist item '{newTextEN}''{newtextAR}'");
    }

    public void DeleteItem(Guid itemId, User by)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
            _task.AddHistoryEntry(by, $"deleted checklist item '{item.TitleEnglish}'");
        }
    }
}