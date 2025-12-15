using System.Collections.Generic;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskAttachment;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskAttachments : Entity
{
    public Guid TaskItemId { get; set; }
	public TaskItem TaskItem { get; set; }
    private List<Attachment> _items = new();
    public IReadOnlyCollection<Attachment> Items {
        get => _items;
        set => _items = value.ToList();
    }

    public TaskAttachments()
    {
        _items = new List<Attachment>();
    }

    public TaskAttachmentDto CopyToDto()
    {
        return new TaskAttachmentDto()
        {
            Id = Id,
            TaskItemId = TaskItemId,
        };
	}
    
}