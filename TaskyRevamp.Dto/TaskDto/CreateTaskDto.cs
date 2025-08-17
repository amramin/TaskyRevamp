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
    public string TitleEnglish { get; private set; }
    [Required(
    ErrorMessageResourceType = typeof(SharedResources),
    ErrorMessageResourceName = ValidationDto.Required
)]
    public string TitleArabic { get; private set; }

    public string DescriptionEnglish { get; private set; }
    public string DescriptionArabic { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Guid TypeId { get; private set; }
    public Guid SourceId { get; private set; }
    public Guid? ReminderId { get; private set; }
    public int Priority { get; private set; }
    public int weight { get; private set; }
    public int Duration => (EndDate.Date - StartDate.Date).Days + 1;
    public int TaskStatus { get; private set; }
    public List<Guid> AssignedDepartmentIds { set; get; }
    public List<Guid> AssignedIds { set; get; }
    public Guid CreatedBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdateDate { get; set; }
}
