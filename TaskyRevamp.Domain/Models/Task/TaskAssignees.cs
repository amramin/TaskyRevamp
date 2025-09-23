using System.Collections.Generic;
using System.Linq;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskAssignees : Entity, IHasCreationMetaData
{
    public Guid taskId {  get; set; }
    public  TaskItem task;

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
    public User CreatedBy { get ; set ; }

    internal TaskAssignees(TaskItem tsk)
    {
        task = tsk;
    }

    public TaskAssignees()
    {
    }

    public bool SetData(TaskAssigneesDto TaskAssigneesDto)
    {
        Id = Id;
        taskId = TaskAssigneesDto.taskId;
        UserId = TaskAssigneesDto.UserId;
        CreatedById = TaskAssigneesDto.CreatedById;
        CreateDate = TaskAssigneesDto.CreateDate;
        AllowComplete = TaskAssigneesDto.AllowComplete;
        IsRejected = TaskAssigneesDto.IsRejected;
        RejectReason = TaskAssigneesDto.RejectReason;
        return true;

    }
    public TaskAssigneesDto CopyToDto()
    {
        return new TaskAssigneesDto
        {
            Id = Id,
            taskId = taskId,
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