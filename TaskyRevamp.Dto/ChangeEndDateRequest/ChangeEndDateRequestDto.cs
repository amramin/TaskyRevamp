using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.ChangeEndDateRequest
{
    public class ChangeEndDateRequestDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public DateTime NewEndDate { get;  set; }
        public string Reason { get;  set; }
        public int Status { get;  set; }
        public Guid Requester { get;  set; }
        public DateTime RequestedAt { get;  set; }
        public bool IsAproved { get;  set; }
        public Guid CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
