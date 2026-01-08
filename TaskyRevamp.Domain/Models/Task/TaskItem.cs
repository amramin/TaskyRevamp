using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;
using Type = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Domain.Models.Task;
public class TaskItem : Entity, IHasCreationMetaData, IHasUpdateMetaData
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid TaskTypeId { set; get; }
    public Type Type { get; set; }
    public Guid TaskSourceId { set; get; }
    public Guid PriorityId { set; get; }
    public Guid? StatusId { set; get; }
    public Source Source { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? ReminderDate { get; set; }
    public List<TaskChecklist> taskChecklists { get; set; }
	public int Duration => StartDate.HasValue && EndDate.HasValue? (EndDate.Value.Date - StartDate.Value.Date).Days + 1: 0;
	public Reminder? Reminder { get; set; }
    public PrioritySettings Priority { get; set; }
    public int Weight { get; set; }
    public int Progress { get; set; }
	Weight _plannedWeight;
    public Weight PlannedWeight
    {
        get
        {
            if (!_subtasks.Any())
                return _plannedWeight;

            double averageWeight = _subtasks.Average(st => st.PlannedWeight.Value);
            int roundedWeight = (int)Math.Round(averageWeight, MidpointRounding.AwayFromZero);
            int finalWeight = Math.Min(100, roundedWeight);

            return new Weight(finalWeight);
        }
                set
        {
            var CountWeight =(double) (PlannedProgress.Percentage * Weight) / 100;
            var RoundedValue = (int)Math.Round(CountWeight, MidpointRounding.AwayFromZero);
            _plannedWeight = new Weight(RoundedValue);
        }
    }
    Weight _actualWeight;
    public Weight ActualWeight
    {
        get
        {
            if (!_subtasks.Any())
                return _actualWeight;

            double averageWeight = _subtasks.Average(st => st.ActualWeight.Value);
            int roundedWeight = (int)Math.Round(averageWeight, MidpointRounding.AwayFromZero);
            int finalWeight = Math.Min(100, roundedWeight);

            return new Weight(finalWeight);
        }
        set
        {
            var CountWeight =(double) (Progress * Weight) / 100;
            var RoundedValue = (int)Math.Round(CountWeight, MidpointRounding.AwayFromZero);
            _actualWeight = new Weight(RoundedValue);
        }
    }
    public Progress PlannedProgress
    {
        get
        {
            if (_subtasks.Any())
            {
                double averageProgress = _subtasks.Average(st => st.PlannedProgress.Percentage);
                int roundedProgress = (int)Math.Round(averageProgress, MidpointRounding.AwayFromZero);
                int finalProgress = Math.Min(100, roundedProgress);
                return new Progress(finalProgress);
            }

            var today = DateTime.UtcNow.Date;
            var startDate = StartDate!.Value.Date;

            if (today < startDate) return new Progress(0);

            int totalDuration = Duration;
            int timeElapsed = (today - startDate).Days + 1;

            double percentage = (double)Math.Min(timeElapsed, totalDuration) / totalDuration * 100.0;
            int roundedPercentage = (int)Math.Round(percentage, MidpointRounding.AwayFromZero);
            return new Progress(roundedPercentage);
        }
    }
    Progress _actualProgress;
    public Progress ActualProgress
    {
        get
        {
            if (!_subtasks.Any())
                return _actualProgress;

            double averageProgress = _subtasks.Average(st => st.ActualProgress.Percentage);
            int roundedProgress = (int)Math.Round(averageProgress, MidpointRounding.AwayFromZero);
            int finalProgress = Math.Min(100, roundedProgress);

            return new Progress(finalProgress);
        }
        set => _actualProgress = value;
    }
    public StatusSettings status { set; get; }
    public List<TaskAssignees> Assignees { get; set; }
    public List<Guid> AssignedDepartmentIds { set; get; }
    public List<Guid> AssignedIds { set; get; }
    public List<TaskDependencies>? Dependencies { get; set; }
    public TaskItem? Parent { get; set; }
    readonly List<TaskItem> _subtasks = new();
    public IReadOnlyCollection<TaskItem> Subtasks => _subtasks.AsReadOnly();
    public TaskChecklist? Checklist { get; set; }
    public TaskComment? Comments { get; set; }
    public TaskAttachments? Attachments { get; set; }
    readonly List<TaskHistoryEntry> _history = new();
    public IReadOnlyCollection<TaskHistoryEntry> History => _history.AsReadOnly();
    readonly List<ChangeEndDateRequest> _changeRequests = new();
    public IReadOnlyCollection<ChangeEndDateRequest> ChangeRequests => _changeRequests.AsReadOnly();
    readonly List<TaskEscalation> _escalations = new();
    public IReadOnlyCollection<TaskEscalation> Escalations => _escalations.AsReadOnly();
    public int Level => GetLevel();
    public Guid CreatedById { get; set; }
    public DateTime CreateDate { get; set; }
    public User CreatedBy { get; set; }
    public Guid? UpdatedById { get; set; }
    public DateTime? UpdateDate { get; set; }
    public User? UpdatedBy { get; set; }
    public Guid FileId { get; set; }
    public TaskItem() { }
    public TaskItem(Guid id, string title, string desc, Guid type, Guid source, DateTime? start, DateTime? end, Guid priority, Weight plannedWeight, Guid creatorid, List<Department> assgndep, List<Guid> assigids, DateTime? rmind, int actualprocess, int wight, List<Guid> dependcy)
    {
        if (end < start) throw new ArgumentException("End date must be after start date.");
        Id = id;
        Title = title;
        //AssignedDepartments = assgndep;
        AssignedDepartmentIds = assgndep.Select(k => k.Id).ToList();
        AssignedIds = assigids;
        Description = desc;
        TaskTypeId = type;
        TaskSourceId = source;
        StartDate = start;
        EndDate = end;
        PriorityId = priority;
        Weight = wight;
        Progress = actualprocess;
        _plannedWeight = plannedWeight;
        _actualProgress = new Progress(actualprocess);
        ReminderDate = rmind;
        _actualWeight = new Weight(wight);
        CreatedById = creatorid;
        Reminder = rmind == null ? null : new Reminder(rmind.Value);
        if(dependcy is not null)
        {
            Dependencies = dependcy.Select(d => new TaskDependencies { TaskItemId = id, DependentId = d }).ToList();
        }
        //  Dependencies = new TaskDependencies(dependcy.Select(id => new TaskItem { Id = id }).ToList()
        //);
        //Creator = creator;

        // Checklist = new TaskChecklist(this);
        //Comments = new TaskComments(this);
        //Attachments = new TaskAttachments(this);
        //AddHistoryEntry(CreatedBy, $"created the task");
    }
    int GetLevel()
    {
        int level = 0;
        var current = Parent;
        while (current != null)
        {
            level++;
            current = current.Parent;
        }
        return level;
    }
    public bool SetData(CreateTaskDto tsakdto)
    {
        Id = tsakdto.Id;
        Description = tsakdto.Description;
        Title = tsakdto.Title;
        Progress = tsakdto.ActualProcess;
        TaskSourceId = tsakdto.SourceId;
        TaskTypeId = tsakdto.TypeId;
        StartDate= tsakdto.StartDate;
        EndDate= tsakdto.EndDate;
        PriorityId = tsakdto.Priority;
        Weight = tsakdto.weight;
        AssignedDepartmentIds = tsakdto.AssignedDepartmentIds;
        AssignedIds = tsakdto.AssignedIds;
        ReminderDate= tsakdto.ReminderDate;
        Dependencies = tsakdto.Dependencies?.Select(d => new TaskDependencies { TaskItemId = tsakdto.Id, DependentId = d }).ToList();
        return true;

    }
    public void UpdateTitle(string title, User by)
    {
        Title = title;
        AddHistoryEntry(by, $"updated the title");
    }
    public void UpdateStatus(StatusSettings taskStus, User by)
    {
        status = taskStus;
        AddHistoryEntry(by, $"updated the TaskStatus");
    }
    public CreateTaskDto CopyToDto()
    {
        return new CreateTaskDto
        {
            Id = Id,
            Description = Description,
            Title = Title,
            Plannedweight = PlannedWeight.Value,
            ActualWeight = ActualWeight.Value,
            weight = Weight,
            ActualProcess = Progress,
            PlannedProgress = PlannedProgress.Percentage,
			//SourceId=Source.Id,
			CreateDate = CreateDate,
            UpdateDate = UpdateDate,
            StartDate = StartDate,
            EndDate = EndDate,
            Priority = PriorityId,
            CreatedByName = CreatedBy?.Username,
            UpdatedBy = UpdatedBy?.Username,
            ReminderDate = ReminderDate,
            TaskStatusName = status?.NameEnglish,
            TaskStatus = StatusId,
            SourceId=TaskSourceId,
            TypeId=TaskTypeId,
			AssignedIds = AssignedIds?.ToList() ?? new List<Guid>(),
			AssignedDepartmentIds = AssignedDepartmentIds?.ToList() ?? new List<Guid>()
		};
    }
    public void UpdateDescription(string desc, User by)
    {
        Description = desc;
        AddHistoryEntry(by, $"updated the description");
    }

    public void ChangeDates(DateTime? newStart, DateTime newEnd, User by)
    {
        if (newEnd < newStart) throw new ArgumentException("End date must be after start date.");
        StartDate = newStart;
        EndDate = newEnd;
        AddHistoryEntry(by, $"changed schedule to {newStart} - {newEnd}");
    }

    public void SetReminder(DateTime remindAt, User by)
    {
        Reminder = new Reminder(remindAt);
        AddHistoryEntry(by, $"set a reminder for {remindAt}");
    }

    public void UpdatePlannedWeight(int wght, User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot manually update planned weight for a parent task. Weight is calculated from its subtasks.");
        }
        _plannedWeight = new Weight(wght);
        AddHistoryEntry(by, $"updated planned weight to {_plannedWeight}");
    }

    public void UpdateActualWeight(int weight, User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot manually update actual weight for a parent task. Weight is calculated from its subtasks.");
        }
        _actualWeight = new Weight(weight);
        AddHistoryEntry(by, $"updated actual weight to {weight}");
    }

    public void AddSubtask(TaskItem subtask, User by)
    {
        if (subtask.Id == Id) throw new InvalidOperationException("Cannot add itself as subtask.");
        _subtasks.Add(subtask);
        subtask.SetParent(this, by);
        AddHistoryEntry(by, $"added subtask {subtask.Id}");
    }

    void SetParent(TaskItem parent, User by)
    {
        Parent = parent;
        AddHistoryEntry(by, $"set parent task to {parent.Id}");
    }

    public void UpdateProgress(int percent, User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot manually update progress for a parent task. Progress is calculated from its subtasks.");
        }

        if (percent == 100)
        {
            //  EnsureAllPrerequisitesAreMet();
        }
        _actualProgress = new Progress(percent);
        // _status = percent == 100 ? TaskStatus.Closed : TaskStatus.InProgress;
        AddHistoryEntry(by, $"updated progress to {percent}%");
    }

    public void Complete(User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot manually complete a parent task. A parent task is completed automatically when all its subtasks are complete.");
        }

        // EnsureAllPrerequisitesAreMet();
        _actualProgress = new Progress(100);
        //_status = TaskStatus.Closed;
        AddHistoryEntry(by, $"completed the task");
    }

    public ChangeEndDateRequest RequestEndDateChange(DateTime newEnd, string reason, User requester)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot request an end date change for a parent task. End dates for parent tasks are typically derived from their subtasks.");
        }

        if (_changeRequests.Any(r => r.Status == ChangeRequestStatus.Pending))
        {
            throw new InvalidOperationException("An end date change request is already pending for this task.");
        }

        if (newEnd.Date < DateTime.UtcNow.Date)
        {
            throw new InvalidOperationException("The new end date should be equal to or greater than the current date.");
        }

        var request = new ChangeEndDateRequest(Guid.NewGuid(), Id, newEnd, reason, requester.Id);
        _changeRequests.Add(request);
        AddHistoryEntry(requester, $"requested end-date change to {newEnd:yyyy-MM-dd}");
        // Comments.Add($"End date request reason: {reason}", requester);
        return request;
    }

    public void Escalate(User toUser, string reason, User by, int level, int tregrAfter, int trgerstatus)
    {
        var esc = new TaskEscalation(Guid.NewGuid(), this, toUser, reason, level, tregrAfter, trgerstatus, by);
        _escalations.Add(esc);
        AddHistoryEntry(by, $"escalated task to {toUser.Username} for reason: {reason}");
    }

    public void Reject(User assigneeToReject, string reason, User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("A parent task with subtasks cannot be rejected. Please delete subtasks first.");
        }

        //if (!Assignees.Items.Contains(assigneeToReject))
        //{
        //    throw new InvalidOperationException($"User {assigneeToReject.Username} is not an assignee of this task and cannot reject it.");
        //}

        //Comments.Add($"Task rejection reason: {reason}", by);
        AddHistoryEntry(by, $"Task rejected by {assigneeToReject.Username}.");

        //Assignees.Remove(assigneeToReject, by);

        //if (!Assignees.Items.Any())
        //{
        //    Assignees.Add(Creator, by);
        //}
    }

    public void SoftDelete(User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("A parent task with subtasks cannot be deleted. Please delete all subtasks first.");
        }

        // _status = TaskStatus.Returned;
        AddHistoryEntry(by, $"deleted the task");
    }

    public void Restore(bool reassignToCreator, User by)
    {
        //if (Parent is { Status: TaskStatus.Closed or TaskStatus.Returned })
        //{
        //    Parent = null;
        //}

        //_status = TaskStatus.NotStarted;
        if (reassignToCreator)
        {
            // Assignees.ClearAndAdd(CreatedBy, by);
        }
        AddHistoryEntry(by, $"restored the task");
    }

    public void PermanentlyDelete(User by)
    {
        AddHistoryEntry(by, $"permanently deleted the task");
    }

    public void AddHistoryEntry(User by, string action)
    {
        var entry = new TaskHistoryEntry(Guid.NewGuid(), this, by, action, DateTime.UtcNow);
        _history.Add(entry);
    }


}