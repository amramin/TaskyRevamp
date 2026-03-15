using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskAssignee : Entity, IHasCreationMetaData
{
    [ForeignKey(nameof(TaskItem))]
    public Guid TaskItemId {  get; set; }
    public TaskItem TaskItem { get; set; }
    [ForeignKey(nameof(User))]

    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool AllowComplete { get; set; }
    //private readonly List<User> _items = new();
    //public IReadOnlyCollection<User> Items => _items.AsReadOnly();
    //public IEnumerable<Department> Departments => _items.Select(u => u.Department).Distinct();
    public bool IsRejected { get; set; }
    public string? RejectReason { get; set; }
    public Guid CreatedById { get ; set ; }
    public DateTime CreateDate { get ; set ; }
    public DateTime AssigneeDate { get; set; }

    public User CreatedBy { get ; set ; }

    internal TaskAssignee(TaskItem tsk)
    {
        TaskItem = tsk;
    }

    public TaskAssignee()
    {
    }

    public bool SetData(TaskAssigneesDto TaskAssigneesDto)
    {
        Id = Id;
        TaskItemId = TaskAssigneesDto.taskId;
        UserId = TaskAssigneesDto.UserId;
        CreatedById = TaskAssigneesDto.CreatedById;
        CreateDate = TaskAssigneesDto.CreateDate;
        AllowComplete = TaskAssigneesDto.AllowComplete;
        IsRejected = TaskAssigneesDto.IsRejected;
        RejectReason = TaskAssigneesDto.RejectReason;
        AssigneeDate=DateTime.Now;
        return true;

    }
    public TaskAssigneesDto CopyToDto()
    {
        return new TaskAssigneesDto
        {
            Id = Id,
            taskId = TaskItemId,
            IsRejected = IsRejected,
            CreateDate = CreateDate,
            CreatedById = CreatedById,
            AllowComplete = AllowComplete,
            RejectReason= RejectReason,
            UserId = UserId,



        };
    }

    //public void Add(User user, User by)
    //{
    //    if (!_items.Contains(user))
    //    {
    //        _items.Add(user);
    //        _task.AddHistoryEntry(by, $"assigned task to {user.Username}");
    //    }
    //}

    //public void Remove(User user, User by)
    //{
    //    if (_items.Remove(user))
    //    {
    //        _task.AddHistoryEntry(by, $"removed assignee {user.Username}");
    //    }
    //}

    //internal void ClearAndAdd(User user, User by)
    //{
    //    _items.Clear();
    //    _items.Add(user);
    //    _task.AddHistoryEntry(by, $"reassigned task to {user.Username} upon restore");
    //}
}