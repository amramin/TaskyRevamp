using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class ChangeEndDateRequest : Entity, IHasCreationMetaData
{
    private ChangeEndDateRequest() { }

    public Guid TaskId { get; set; }
    public Guid RequesterId { get; set; }
    public TaskItem Task { get; private set; }
    public DateTime NewEndDate { get; private set; }
    public string Reason { get; private set; }
    public ChangeRequestStatus Status { get; private set; }
    public User Requester { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public bool IsAproved { get; private set; }
    public Guid CreatedById { get; set; }
    public DateTime CreateDate { get; set; }
    public User CreatedBy { get; set; }

    public bool SetData(ChangeEndDateRequestDto changeEndDateRequestDto)
    {
        Id = Id;
        TaskId = changeEndDateRequestDto.TaskId;
        Reason = changeEndDateRequestDto.Reason;
        CreateDate=DateTime.Now;
        NewEndDate=changeEndDateRequestDto.NewEndDate;
        IsAproved = changeEndDateRequestDto.IsAproved;
        CreatedById = changeEndDateRequestDto.CreatedById;
        Status = (ChangeRequestStatus)changeEndDateRequestDto.Status;
        CreateDate = changeEndDateRequestDto.CreateDate;
        return true;

    }
    public ChangeEndDateRequestDto CopyToDto()
    {
        return new ChangeEndDateRequestDto
        {
            Id = Id,

            CreateDate = CreateDate,
            CreatedById = CreatedById,
            TaskId = TaskId,
            Reason = Reason,    
            Status=(int)Status,

            IsAproved= IsAproved,
            NewEndDate = NewEndDate,


        };
    }
    public ChangeEndDateRequest(Guid id, Guid task, DateTime newEnd, string reason, Guid requester)
    {
        Id = id;
        TaskId = task;
        NewEndDate = newEnd;
        Reason = reason;
        RequesterId = requester;
        RequestedAt = DateTime.UtcNow;
        CreatedById = requester;
        CreateDate = DateTime.Now;
        Status = ChangeRequestStatus.Pending;
    }
    public void Approve(User by)
    {
        if (by.Id != Task.CreatedById)
        {
            throw new InvalidOperationException("Only the task creator can approve end date change requests.");
        }
        Task.ChangeDates(Task.StartDate, NewEndDate, by);
        Status = ChangeRequestStatus.Approved;
        Task.AddHistoryEntry(by, $"approved end-date change request to {NewEndDate:yyyy-MM-dd}");
    }
    public void Reject(User by)
    {
        if (by.Id != Task.CreatedById)
        {
            throw new InvalidOperationException("Only the task creator can reject end date change requests.");
        }
        Status = ChangeRequestStatus.Rejected;
        Task.AddHistoryEntry(by, $"rejected end-date change request.");
    }
}