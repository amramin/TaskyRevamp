using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskComments : Entity
{
    private readonly TaskItem _task;
    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Items => _comments.AsReadOnly();

    internal TaskComments(TaskItem task)
    {
        _task = task;
    }

    public TaskComments()
    {
    }

    public void Add(string content, User by)
    {
        var comment = new Comment(Guid.NewGuid(), by, content, DateTime.UtcNow);
        _comments.Add(comment);
        _task.AddHistoryEntry(by, $"added a comment");
    }

    public void Edit(Guid commentId, string newContent, User by)
    {
        var c = _comments.FirstOrDefault(x => x.Id == commentId) ?? throw new KeyNotFoundException();
        if (c.Author != by) throw new InvalidOperationException("Cannot edit others' comments.");
        c.UpdateContent(newContent, by);
        _task.AddHistoryEntry(by, $"edited a comment");
    }

    public void Delete(Guid commentId, User by)
    {
        var c = _comments.FirstOrDefault(x => x.Id == commentId) ?? throw new KeyNotFoundException();
        if (c.Author != by) throw new InvalidOperationException("Cannot delete others' comments.");
        _comments.Remove(c);
        _task.AddHistoryEntry(by, $"deleted a comment");
    }
}