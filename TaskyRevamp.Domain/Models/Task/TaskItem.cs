using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;


public class TaskItem : Entity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskType Type { get; private set; }
    public TaskSource Source { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int Duration => (EndDate.Date - StartDate.Date).Days + 1;
    public Reminder? Reminder { get; private set; }
    public Priority Priority { get; private set; }
    private Weight _plannedWeight;
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
        private set => _plannedWeight = value;
    }
    private Weight _actualWeight;
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
        private set => _actualWeight = value;
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
            var startDate = StartDate.Date;

            if (today < startDate) return new Progress(0);

            int totalDuration = Duration;
            int timeElapsed = (today - startDate).Days + 1;

            double percentage = (double)Math.Min(timeElapsed, totalDuration) / totalDuration * 100.0;
            int roundedPercentage = (int)Math.Round(percentage, MidpointRounding.AwayFromZero);
            return new Progress(roundedPercentage);
        }
    }
    private Progress _actualProgress;
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
        private set => _actualProgress = value;
    }
    private TaskStatus _status;
    public TaskStatus Status
    {
        get
        {
            if (_status == TaskStatus.Returned) return _status;

            if (_subtasks.Any())
            {
                if (ActualProgress.Percentage == 100) return TaskStatus.Closed;
                if (_subtasks.Any(s => s.Status is TaskStatus.InProgress or TaskStatus.Delayed)) return TaskStatus.InProgress;
                return TaskStatus.NotStarted;
            }

            return _status;
        }
        private set => _status = value;
    }

    public User Creator { get; private set; }

    public TaskAssignees Assignees { get; private set; }
    public IEnumerable<Department> AssignedDepartments => Assignees.Departments;
    public TaskDependencies Dependencies { get; private set; }
    public TaskItem? Parent { get; private set; }
    private readonly List<TaskItem> _subtasks = new();
    public IReadOnlyCollection<TaskItem> Subtasks => _subtasks.AsReadOnly();

    public TaskChecklist Checklist { get; private set; }
    public TaskComments Comments { get; private set; }
    public TaskAttachments Attachments { get; private set; }
    private readonly List<TaskHistoryEntry> _history = new();
    public IReadOnlyCollection<TaskHistoryEntry> History => _history.AsReadOnly();
    private readonly List<ChangeEndDateRequest> _changeRequests = new();
    public IReadOnlyCollection<ChangeEndDateRequest> ChangeRequests => _changeRequests.AsReadOnly();
    private readonly List<TaskEscalation> _escalations = new();
    public IReadOnlyCollection<TaskEscalation> Escalations => _escalations.AsReadOnly();
    public int Level => GetLevel();
    private TaskItem() {  }
    public TaskItem(Guid id, string title, string desc, TaskType type, TaskSource source, DateTime start, DateTime end, Priority priority, Weight plannedWeight, User creator)
    {
        if (end < start) throw new ArgumentException("End date must be after start date.");
        Id = id;
        Title = title;
        Description = desc;
        Type = type;
        Source = source;
        StartDate = start;
        EndDate = end;
        Priority = priority;
        _plannedWeight = plannedWeight;
        _actualProgress = new Progress(0);
        _status = TaskStatus.NotStarted;
        _actualWeight = new Weight(0);
        Creator = creator;
        Checklist = new TaskChecklist(this);
        Comments = new TaskComments(this);
        Attachments = new TaskAttachments(this);
        Assignees = new TaskAssignees(this);
        Dependencies = new TaskDependencies(this);
        AddHistoryEntry(creator, $"created the task");
    }
    private int GetLevel()
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
    public void UpdateTitle(string title, User by)
    {
        Title = title;
        AddHistoryEntry(by, $"updated the title");
    }

    public void UpdateDescription(string desc, User by)
    {
        Description = desc;
        AddHistoryEntry(by, $"updated the description");
    }

    public void ChangeDates(DateTime newStart, DateTime newEnd, User by)
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

    public void UpdatePlannedWeight(int weight, User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot manually update planned weight for a parent task. Weight is calculated from its subtasks.");
        }
        _plannedWeight = new Weight(weight);
        AddHistoryEntry(by, $"updated planned weight to {weight}");
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

    private void SetParent(TaskItem parent, User by)
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
            EnsureAllPrerequisitesAreMet();
        }
        _actualProgress = new Progress(percent);
        _status = percent == 100 ? TaskStatus.Closed : TaskStatus.InProgress;
        AddHistoryEntry(by, $"updated progress to {percent}%");
    }

    public void Complete(User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("Cannot manually complete a parent task. A parent task is completed automatically when all its subtasks are complete.");
        }

        EnsureAllPrerequisitesAreMet();
        _actualProgress = new Progress(100);
        _status = TaskStatus.Closed;
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

        var request = new ChangeEndDateRequest(Guid.NewGuid(), this, newEnd, reason, requester);
        _changeRequests.Add(request);
        AddHistoryEntry(requester, $"requested end-date change to {newEnd:yyyy-MM-dd}");
        Comments.Add($"End date request reason: {reason}", requester);
        return request;
    }

    public void Escalate(User toUser, string reason, User by)
    {
        var esc = new TaskEscalation(Guid.NewGuid(), this, toUser, reason, by);
        _escalations.Add(esc);
        AddHistoryEntry(by, $"escalated task to {toUser.Username} for reason: {reason}");
    }

    public void Reject(User assigneeToReject, string reason, User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("A parent task with subtasks cannot be rejected. Please delete subtasks first.");
        }

        if (!Assignees.Items.Contains(assigneeToReject))
        {
            throw new InvalidOperationException($"User {assigneeToReject.Username} is not an assignee of this task and cannot reject it.");
        }

        Comments.Add($"Task rejection reason: {reason}", by);
        AddHistoryEntry(by, $"Task rejected by {assigneeToReject.Username}.");

        Assignees.Remove(assigneeToReject, by);

        if (!Assignees.Items.Any())
        {
            Assignees.Add(Creator, by);
        }
    }

    public void SoftDelete(User by)
    {
        if (_subtasks.Any())
        {
            throw new InvalidOperationException("A parent task with subtasks cannot be deleted. Please delete all subtasks first.");
        }

        _status = TaskStatus.Returned; 
        AddHistoryEntry(by, $"deleted the task");
    }

    public void Restore(bool reassignToCreator, User by)
    {
        if (Parent is { Status: TaskStatus.Closed or TaskStatus.Returned })
        {
            Parent = null;
        }

        _status = TaskStatus.NotStarted;
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

    private void EnsureAllPrerequisitesAreMet()
    {
        if (Dependencies.Items.Any(d => d.Status != TaskStatus.Closed))
        {
            throw new InvalidOperationException("This task cannot be marked as Closed because one or more prerequisite tasks are still in progress, Delayed, or Returned.");
        }
    }
}