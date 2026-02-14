using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.SystemConfiguration
{
    public class TaskFilterComponent
    {
        public string? Title { get; set; }
        public List<Guid>? Priority { get; set; }
        public List<Guid?>? Status { get; set; }
        public DateTime? FromStartDate { get; set; }
        public DateTime? ToStartDate { get; set; }
        public DateTime? FromEndDate { get; set; }
        public DateTime? ToEndDate { get; set; }
        public List<Guid>? AssignedTo { get; set; }
        public List<Guid>? AssignedToDepartment { get; set; }
        public List<Guid>? CreatedBy { get; set; }
        public List<Guid>? CreatedByDepartment { get; set; }
        public DateTime? FromCreationDate { get; set; }
        public DateTime? ToCreationDate { get; set; }
        public List<Guid?>? Type { get; set; }
        public List<Guid?>? Source { get; set; }

    }
}
