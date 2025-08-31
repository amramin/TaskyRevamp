using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class ChecklistItem : Entity
{
    protected ChecklistItem() { }

    public TaskChecklist TaskChecklist {  get; set; }
    public string TitleEnglish { get;  set; }
    public string TitleArabic { get; set; }

    public TaskStatus Status { get; set; }
    public bool IsCompleted { get;  set; }
    public DateTime EndDate { get; set; }
    public User CreatedBy { get;  set; }
    public User AssignedUser { get; set; }
    public DateTime CreatedAt { get;  set; }
    public ChecklistItem(Guid id, string titleEN,string titleAR, User by)
    {
        Id = id;
        TitleEnglish = titleEN;
        TitleArabic = titleAR;  
        CreatedBy = by;
        CreatedAt = DateTime.UtcNow;
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