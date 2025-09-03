using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Domain.Models.Task;

public class ChecklistItem : Entity, IHasCreationMetaData
{
    protected ChecklistItem() { }

    public Guid TaskChecklistId { set; get; }
    public TaskChecklist TaskChecklist {  get; set; }
    public string TitleEnglish { get;  set; }
    public string TitleArabic { get; set; }

    public TaskStatus Status { get; set; }
    public bool IsCompleted { get;  set; }
    public DateTime EndDate { get; set; }
    public User CreatedBy { get;  set; }
    public User AssignedUser { get; set; }
    public Guid CreatedById { get ; set; }
    public DateTime CreateDate { get; set; }

    public bool SetData(ChecklistItemDto checklistItemDto)
    {
        Id= checklistItemDto.Id;
        TitleEnglish = checklistItemDto.TitleEnglish;
        TitleArabic= checklistItemDto.TitleArabic;
        IsCompleted = checklistItemDto.IsCompleted;
        EndDate = checklistItemDto.EndDate;
       
        return true;

    }
    public ChecklistItemDto CopyToDto()
    {
        return new ChecklistItemDto
        {
       Id= Id,
       TitleEnglish= TitleEnglish,
       TitleArabic= TitleArabic,
       AssignedUserId=AssignedUser.Id,
       
       EndDate= EndDate,
       IsCompleted= IsCompleted,
       TaskChecklistId= TaskChecklist.Id,

        };
    }
    public ChecklistItem(Guid id, string titleEN,string titleAR, User by)
    {
        Id = id;
        TitleEnglish = titleEN;
        TitleArabic = titleAR;  
        CreatedBy = by;
        CreateDate = DateTime.UtcNow;
    }
    public void UpdateText(string titleEN, string titleAR, User by)
    {
        TitleEnglish = titleEN;
        TitleArabic = titleAR;
    }
    public void UpdateStatus(TaskStatus status)
    {
        Status = status;
    }
    public void MarkComplete(User by)
    {
        IsCompleted = true;
    }
}