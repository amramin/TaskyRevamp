using System.Collections.Generic;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskAttachments : Entity
{
    private readonly TaskItem _task;
    private readonly List<Attachment> _items = new();
    public IReadOnlyCollection<Attachment> Items => _items.AsReadOnly();

    internal TaskAttachments(TaskItem task)
    {
        _task = task;
    }

    public TaskAttachments()
    {
    }

    public void Add(string fileName, byte[] content, User by)
    {
        var attach = new Attachment(Guid.NewGuid(), fileName, content, by);
        _items.Add(attach);
        _task.AddHistoryEntry(by, $"added attachment '{fileName}'");
    }

    public void Delete(Guid attachmentId, User by)
    {
        var a = _items.FirstOrDefault(x => x.Id == attachmentId) ?? throw new KeyNotFoundException();
        if (a.UploadedBy != by && _task.CreatedBy != by) throw new InvalidOperationException("Cannot delete this attachment.");
        _items.Remove(a);
        _task.AddHistoryEntry(by, $"deleted attachment '{a.FileName}'");
    }
}