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

    public string DescriptionEnglish { get; set; }
    public string DescriptionArabic { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid TypeId { get; set; }
    public Guid SourceId { get; set; }
    public int Priority { get; set; }
    public int weight { get; set; }
    public int Duration => (EndDate.Date - StartDate.Date).Days + 1;
    public int TaskStatus { get; set; }
    public List<Guid> AssignedDepartmentIds { set; get; }
    public List<Guid> AssignedIds { set; get; }
    public Guid CreatedBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}