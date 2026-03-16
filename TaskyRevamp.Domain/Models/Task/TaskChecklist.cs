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
    public  TaskItem TaskItem { set; get; }
    public string Title { get; set; }
    public  List<ChecklistItem> items = new();
	public TaskChecklist(Guid taskid, string title)
	{
		TaskItemId = taskid;
		Title = title;
	}
	public TaskChecklist()
	{
	}
	public void SetData(TaskChecklistDto taskChecklistDto)
    {
        Title = taskChecklistDto.Title;
        TaskItemId = taskChecklistDto.TaskId;
    }
    public TaskChecklistDto CopyToDto()
    {
        return new TaskChecklistDto
        {
            Id = Id,
            TaskId = TaskItemId,
            Title = Title,
        };
    }
}