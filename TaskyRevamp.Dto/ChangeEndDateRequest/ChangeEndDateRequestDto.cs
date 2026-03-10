using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.ChangeEndDateRequest
{
    public class ChangeEndDateRequestDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public DateTime NewEndDate { get;  set; }
        public DateTime oldEndDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public string Reason { get;  set; }
        public string RequesterName { get; set; }=string.Empty;
        public ChangeRequestStatus Status { get;  set; }
        public Guid Requester { get;  set; }
        public DateTime RequestedAt { get;  set; }
        public bool IsAproved { get;  set; }
        public Guid CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
       

    }
}
