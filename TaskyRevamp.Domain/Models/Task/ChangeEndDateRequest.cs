using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class ChangeEndDateRequest : Entity, IHasCreationMetaData
{
    private ChangeEndDateRequest() { }

    public Guid TaskItemId { get; set; }
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
        TaskItemId = changeEndDateRequestDto.TaskId;
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
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        return new ChangeEndDateRequestDto
        {
            Id = Id,

            CreateDate = CreateDate,
            CreatedById = CreatedById,
            TaskId = TaskItemId,
            Reason = Reason,
            Status = Status,
            Requester = RequesterId,
            IsAproved = IsAproved,
            NewEndDate = NewEndDate,
            RequesterName = currentCulture=="ar" ?CreatedBy.NameArabic??"": CreatedBy.NameEnglish ?? "",
            oldEndDate=Task.EndDate??DateTime.Now,
            TaskTitle=Task.Title
            
        };
    }
    public ChangeEndDateRequest(Guid id, Guid task, DateTime newEnd, string reason, Guid requester)
    {
        Id = id;
        TaskItemId = task;
        NewEndDate = newEnd;
        Reason = reason;
        RequesterId = requester;
        RequestedAt = DateTime.UtcNow;
        CreatedById = requester;
        CreateDate = DateTime.UtcNow;
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