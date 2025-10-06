using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskComment : Entity, IHasCreationMetaData, IHasUpdateMetaData
{
    public Guid TaskItemId { get; set; }
    public  TaskItem taskItem { get; set; }
    public string Content { get;  set; }
    public Guid CreatedById { get; set ; }
    public DateTime CreateDate { get; set; }
    public User CreatedBy { get; set; }
    public Guid? UpdatedById { get; set; }
    public DateTime? UpdateDate { get; set; }
    public User? UpdatedBy { get; set; }

    //private readonly List<Comment> _comments = new();
    //public IReadOnlyCollection<Comment> Items => _comments.AsReadOnly();

    internal TaskComment(TaskItem task)
    {
        taskItem = task;
    }

    public TaskComment()
    {
    }
    public TaskComment(Guid taskid,string content,Guid createdid)
    {
        TaskItemId= taskid;
        Content = content;
        CreateDate= DateTime.Now;
        CreatedById= createdid;
    }
    public void Add(Guid taskid, string content, User by)
    {
        var comment = new TaskComment(taskid, content,by.Id);
       // comment.Add(comment);
        taskItem.AddHistoryEntry(by, $"added a comment");
    }
    public bool SetData(TaskCommentDto taskCommentDto)
    {
        Id = Id;
        TaskItemId = taskCommentDto.TaskItemId;
        Content = taskCommentDto.Content;
        CreatedById = taskCommentDto.CreatedById;
        CreateDate = taskCommentDto.CreateDate;
        return true;

    }
    public TaskCommentDto CopyToDto()
    {
        return new TaskCommentDto
        {
            Id = Id,
            TaskItemId = TaskItemId,
            Content = Content,
            CreateDate = CreateDate,
            CreatedById= CreatedById,




        };
    }

    //public void Edit(Guid commentId, string newContent, User by)
    //{
    //    var c = _comments.FirstOrDefault(x => x.Id == commentId) ?? throw new KeyNotFoundException();
    //    if (c.Author != by) throw new InvalidOperationException("Cannot edit others' comments.");
    //    c.UpdateContent(newContent, by);
    //    _task.AddHistoryEntry(by, $"edited a comment");
    //}

    //public void Delete(Guid commentId, User by)
    //{
    //    var c = _comments.FirstOrDefault(x => x.Id == commentId) ?? throw new KeyNotFoundException();
    //    if (c.Author != by) throw new InvalidOperationException("Cannot delete others' comments.");
    //    _comments.Remove(c);
    //    _task.AddHistoryEntry(by, $"deleted a comment");
    //}
}