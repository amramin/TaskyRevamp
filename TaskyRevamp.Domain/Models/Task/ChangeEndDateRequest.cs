using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class ChangeEndDateRequest : Entity
{
    private ChangeEndDateRequest() { }

    public TaskItem Task { get; private set; }
    public DateTime NewEndDate { get; private set; }
    public string Reason { get; private set; }
    public ChangeRequestStatus Status { get; private set; }
    public User Requester { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public ChangeEndDateRequest(Guid id, TaskItem task, DateTime newEnd, string reason, User requester)
    {
        Id = id;
        Task = task;
        NewEndDate = newEnd;
        Reason = reason;
        Requester = requester;
        RequestedAt = DateTime.UtcNow;
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