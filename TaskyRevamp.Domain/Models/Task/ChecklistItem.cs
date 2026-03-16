using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Domain.Models.Task;

public class ChecklistItem : Entity, IHasCreationMetaData
{
	
	public Guid TaskChecklistId { set; get; }
	public TaskChecklist taskChecklist { get; set; }
	public string Title { get; set; }
	public DateTime? EndDate { get; set; }
	public User? AssignedUser { get; set; }
	public Guid? AssignedUserId { get; set; }
	public bool IsDone { get; set; }
	public Guid CreatedById { get; set; }
	public DateTime CreateDate { get; set; }
	public User CreatedBy { get; set; }

	protected ChecklistItem() { }
	public ChecklistItem(string title, Guid taskChecklistId, Guid? assignedId, DateTime? endDate, bool isDone)
	{
		Id = Guid.NewGuid();
		Title = title;
		AssignedUserId = assignedId;
		EndDate = endDate;
		TaskChecklistId = taskChecklistId;
		IsDone = isDone;
	}
	public void SetData(ChecklistItemDto checklistItemDto)
	{
		Id = checklistItemDto.Id;
		Title = checklistItemDto.Title;
		AssignedUserId = checklistItemDto.AssignedUserId;
		EndDate = checklistItemDto.EndDate;
		IsDone = checklistItemDto.IsDone;
	}
	public ChecklistItemDto CopyToDto()
	{
		return new ChecklistItemDto
		{
			Id = Id,
			Title = Title,
			AssignedUserId = AssignedUserId,
			EndDate = EndDate,
			TaskChecklistId = TaskChecklistId,
			IsDone = IsDone,
			CreateDate = CreateDate,
			CreatedById = CreatedById
		};
	}
	
}