using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskEscalation;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskEscalation : Entity, IHasCreationMetaData
{
    public Guid TaskId { get; set; }
    public Guid EscalatedToId { get; set; }
    public TaskItem Task { get; private set; }
    public User EscalatedTo { get; private set; }
    public string Reason { get; private set; }
    public int Level { get; private set; }
    public  int TriggerAfter { get; private set; }
    public int TriggerStatus { get; private set; }
    public EscalationStatus Status { get; private set; }
   // public User RequestedBy { get; private set; }
    //public DateTime RequestedAt { get; private set; }
    public Guid CreatedById { get ; set ; }
    public DateTime CreateDate { get ; set ; }
    public User CreatedBy { get ; set ; }

    public TaskEscalation(Guid id, TaskItem task, User to, string reason,int level,int tregiAfter,int TrgeStatus, User by)
    {
        Id = id;
        Task = task;
        EscalatedTo = to;
        Reason = reason;
        CreatedBy = by;
        CreateDate = DateTime.UtcNow;
        Level = level;
        Status = EscalationStatus.Active;
        TriggerStatus = TrgeStatus;
        TriggerAfter= TrgeStatus;
    }

    public TaskEscalation()
    {
    }
    public bool SetData(TaskEscalationDto TaskEscalationDto)
    {
        Id = Id;
        TaskId = TaskEscalationDto.TaskId;
        Reason = TaskEscalationDto.Reason;
        CreatedById = TaskEscalationDto.CreatedById;
        CreateDate = TaskEscalationDto.CreateDate;
        EscalatedToId= TaskEscalationDto.EscalatedToId;
        Level = TaskEscalationDto.Level;
        TriggerAfter = TaskEscalationDto.TriggerAfter;
        TriggerStatus= TaskEscalationDto.TriggerStatus;
        Status = (EscalationStatus)TaskEscalationDto.EscalationStatus;
        return true;

    }
    public TaskEscalationDto CopyToDto()
    {
        return new TaskEscalationDto
        {
            Id = Id,
            TaskId= TaskId,
            EscalationStatus = (int)Status,
            Level= Level,
            Reason=Reason,
            EscalatedToId = EscalatedToId,
            TriggerAfter= TriggerAfter,
            TriggerStatus= TriggerStatus,
            CreateDate = CreateDate,
            CreatedById = CreatedById,




        };
    }
    public void Resolve(User by)
    {
        Status = EscalationStatus.Resolved;
    }
}