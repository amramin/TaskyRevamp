using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskChecklist : Entity
{
    public Guid TaskId { get; set; }
    public  TaskItem _task { set; get; }
    public string TitleEnglish { get; set; }
    public string TitleArabic { get; set; }
    private readonly List<ChecklistItem> _items = new();
    public IReadOnlyCollection<ChecklistItem> Items => _items.AsReadOnly();
    public bool SetData(TaskChecklistDto taskChecklistDto)
    {
        Id = Id;
        TitleEnglish = taskChecklistDto.TitleEnglish;
        TitleArabic = taskChecklistDto.TitleArabic;
        TaskId = taskChecklistDto.TaskId;
        return true;

    }
    public TaskChecklistDto CopyToDto()
    {
        return new TaskChecklistDto
        {
            Id = Id,
            TaskId = TaskId,
            TitleEnglish = TitleEnglish,
            TitleArabic = TitleArabic,
        




        };
    }
    internal TaskChecklist(TaskItem task)
    {
        _task = task;
    }
    public TaskChecklist(Guid taskid,string titleEN,string titleAR)
    {
        TaskId = taskid;
        TitleEnglish= titleEN;
        TitleArabic= titleAR;
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