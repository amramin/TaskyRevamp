using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskChecklist : Entity
{
    public Guid TaskItemId { get; set; }
    public  TaskItem taskItem { set; get; }
    public string TitleEnglish { get; set; }
    public string TitleArabic { get; set; }
    public  List<ChecklistItem> items = new();
    public bool SetData(TaskChecklistDto taskChecklistDto)
    {
        Id = Id;
        TitleEnglish = taskChecklistDto.TitleEnglish;
        TitleArabic = taskChecklistDto.TitleArabic;
        TaskItemId = taskChecklistDto.TaskId;
        return true;

    }
    public TaskChecklistDto CopyToDto()
    {
        return new TaskChecklistDto
        {
            Id = Id,
            TaskId = TaskItemId,
            TitleEnglish = TitleEnglish,
            TitleArabic = TitleArabic,
        




        };
    }
    internal TaskChecklist(TaskItem tsk)
    {
        taskItem = tsk;
    }
    public TaskChecklist(Guid taskid,string titleEN,string titleAR)
    {
        TaskItemId = taskid;
        TitleEnglish= titleEN;
        TitleArabic= titleAR;
    }
    public TaskChecklist()
    {
    }

    public void AddItem(string textEN,string textAR,User by)
    {
        var item = new ChecklistItem(Guid.NewGuid(), textEN,textAR, by);
        items.Add(item);
        taskItem.AddHistoryEntry(by, $"added checklist item '{textEN}''{textEN}'");
    }

    public void EditItem(Guid itemId, string newTextEN,string newtextAR, User by)
    {
        var item = items.FirstOrDefault(x => x.Id == itemId) ?? throw new KeyNotFoundException();
        item.UpdateText(newTextEN,newtextAR, by);
        taskItem.AddHistoryEntry(by, $"edited checklist item '{newTextEN}''{newtextAR}'");
    }

    public void DeleteItem(Guid itemId, User by)
    {
        var item = items.FirstOrDefault(x => x.Id == itemId);
        if (item != null)
        {
            items.Remove(item);
            taskItem.AddHistoryEntry(by, $"deleted checklist item '{item.TitleEnglish}'");
        }
    }
}