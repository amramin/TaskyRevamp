using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Services;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.Enums;
using Type = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Domain.Models.Task;
public class TaskItem : Entity, IHasCreationMetaData, IHasUpdateMetaData
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public Guid? TaskTypeId { set; get; }
	public Type Type { get; set; }
	public Guid? TaskSourceId { set; get; }
	public Guid PriorityId { set; get; }
	public Guid? StatusId { set; get; }
	public Source Source { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public DateTime? ReminderDate { get; set; }
	public List<TaskChecklist> taskChecklists { get; set; }
	public int Duration => StartDate.HasValue && EndDate.HasValue ? (EndDate.Value.Date - StartDate.Value.Date).Days + 1 : 0;
	public Reminder? Reminder { get; set; }
	public PrioritySettings Priority { get; set; }
	public int? Weight { get; set; }
	public bool IsDeleted { get; set; }
	public int Progress { get; set; }
	Weight _plannedWeight;
	public Weight PlannedWeight
	{
		get => TaskWeightCalculator.CalculatePlannedWeight(_subtasks.AsReadOnly(), _plannedWeight);
		set => _plannedWeight = TaskWeightCalculator.ComputePlannedWeightValue(Weight, PlannedProgress);
	}
	Weight _actualWeight;
	public Weight ActualWeight
	{
		get => TaskWeightCalculator.CalculateActualWeight(_subtasks.AsReadOnly(), _actualWeight);
		set => _actualWeight = TaskWeightCalculator.ComputeActualWeightValue(Weight, Progress);
	}
	public Progress PlannedProgress
	{
		get => TaskWeightCalculator.CalculatePlannedProgress(_subtasks.AsReadOnly(), StartDate, Duration);
	}
	Progress _actualProgress;
	public Progress ActualProgress
	{
		get => TaskWeightCalculator.CalculateActualProgress(_subtasks.AsReadOnly(), _actualProgress);
		set => _actualProgress = value;
	}
	public StatusSettings status { set; get; }
	public ICollection<TaskAssignee> TaskAssignees { get; set; } = new List<TaskAssignee>();
	public List<Guid> AssignedDepartmentIds { set; get; }
	public List<TaskDependencies>? Dependencies { get; set; }
	public TaskItem? Parent { get; set; }
	readonly List<TaskItem> _subtasks = new();
	public IReadOnlyCollection<TaskItem> Subtasks => _subtasks.AsReadOnly();
	public TaskChecklist? Checklist { get; set; }
	public TaskComment? Comments { get; set; }
	public TaskAttachments? Attachments { get; set; }
	readonly List<TaskHistoryEntry> _history = new();
	public IReadOnlyCollection<TaskHistoryEntry> History => _history.AsReadOnly();
	//readonly List<ChangeEndDateRequest> _changeRequests = new();
	//public IReadOnlyCollection<ChangeEndDateRequest> ChangeRequests => _changeRequests.AsReadOnly();
	public List<ChangeEndDateRequest> ChangeEndDateRequests=new List<ChangeEndDateRequest>();
	readonly List<TaskEscalation> _escalations = new();
	public IReadOnlyCollection<TaskEscalation> Escalations => _escalations.AsReadOnly();
	public int Level => GetLevel();
	public Guid CreatedById { get; set; }
	public DateTime CreateDate { get; set; }
	public User CreatedBy { get; set; }
	public Guid? UpdatedById { get; set; }
	public DateTime? UpdateDate { get; set; }
	public User? UpdatedBy { get; set; }
	public Guid? DeletedById { get; set; }
	public DateTime? DeleteDate { get; set; }
	public User? DeletedBy { get; set; }
	public Guid FileId { get; set; }
	public TaskItem() { }
	public TaskItem(Guid id, string title, string desc, Guid? type, Guid? source, DateTime? start, DateTime? end, Guid priority, Weight plannedWeight, Guid creatorid, List<Department> assgndep, List<Guid> assigids, DateTime? rmind, int actualprocess, int? wight, List<Guid> dependcy)
	{
		if (end < start) throw new ArgumentException("End date must be after start date.");
		Id = id;
		Title = title;
		//AssignedDepartments = assgndep;
		AssignedDepartmentIds = assgndep.Select(k => k.Id).ToList();
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
		_actualWeight = new Weight(wight ?? 0);
		CreatedById = creatorid;
		Reminder = rmind == null ? null : new Reminder(rmind.Value);
		TaskAssignees=assigids.Select(userid=> new TaskAssignee { TaskItemId=id,UserId=userid,AssigneeDate=DateTime.Now }).ToList();
		if (dependcy is not null)
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

		if (ChangeEndDateRequests.Any(r => r.Status == ChangeRequestStatus.Pending))
		{
			throw new InvalidOperationException("An end date change request is already pending for this task.");
		}

		if (newEnd.Date < DateTime.UtcNow.Date)
		{
			throw new InvalidOperationException("The new end date should be equal to or greater than the current date.");
		}

		var request = new ChangeEndDateRequest(Guid.NewGuid(), Id, newEnd, reason, requester.Id);
        ChangeEndDateRequests.Add(request);
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

	public void SoftDelete(Guid deletedbyId)
	{
		IsDeleted = true;
		DeleteDate = DateTime.UtcNow;
		DeletedById = deletedbyId;
	}
	public void AddHistoryEntry(User by, string action)
	{
		var entry = new TaskHistoryEntry(Guid.NewGuid(), this, by, action, DateTime.UtcNow);
		_history.Add(entry);
	}


}