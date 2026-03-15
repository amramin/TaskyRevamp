using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskAssignees
{
    public class TaskAssigneeDataDto
    {
        public Guid UserId { get; set; }
        public DateTime AssigneeDate { get; set; }
    }
}
