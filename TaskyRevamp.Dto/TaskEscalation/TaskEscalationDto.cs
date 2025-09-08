using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskEscalation
{
    public class TaskEscalationDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid EscalatedToId { get;  set; }
        public string Reason { get;  set; }
        public int Level { get;  set; }
        public int TriggerAfter { get;  set; }
        public int TriggerStatus { get;  set; }
        public int EscalationStatus { get;  set; }
        // public User RequestedBy { get;  set; }
        //public DateTime RequestedAt { get;  set; }
        public Guid CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
