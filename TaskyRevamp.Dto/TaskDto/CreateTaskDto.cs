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
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int Duration => (EndDate.Date - StartDate.Date).Days + 1;
}
