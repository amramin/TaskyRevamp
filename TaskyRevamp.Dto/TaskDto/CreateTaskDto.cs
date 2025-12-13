using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.TaskDto;

public class CreateTaskDto
{
    public Guid Id { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(SharedResources),
        ErrorMessageResourceName = ValidationDto.Required
    )]
    public string TitleEnglish { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(SharedResources),
        ErrorMessageResourceName = ValidationDto.Required
    )]
    public string TitleArabic { get; set; }

    public string Title => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? TitleArabic : TitleEnglish;

    public string? DescriptionEnglish { get; set; }
    public string? DescriptionArabic { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid TypeId { get; set; }
    public Guid SourceId { get; set; }
    public Guid Priority { get; set; }
    public Guid Photo { get; set; }
    public string? Extention { get; set; }

    public string PhotoName { get; set; }
    public string? PhotoBase64 { get; set; }
    public int weight { get; set; } = 0;
    public int ActualProcess { get; set; }
    public int Duration => (EndDate.Date - StartDate.Date).Days + 1;
    public Guid? TaskStatus { get; set; }
    public string? TaskStatusName { get; set; }
    public List<Guid> AssignedDepartmentIds { set; get; }
    public string? AssignedDepartmentName { set; get; }
    public List<Guid> AssignedIds { set; get; }

    public List<Guid>? Dependencies { set; get; }

    public Guid CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime? CreateDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? Content { get; set; }
    public string? SourceName { get; set; }
    public string? TypeName { get; set; }
    public string? PriorityName { get; set; }

    public DateTime? ReminderDate { get; set; }
    public string? AssigneduserNames { get; set; }
}