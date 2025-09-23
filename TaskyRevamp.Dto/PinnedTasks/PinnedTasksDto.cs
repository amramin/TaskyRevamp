using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.PinnedTasks
{
    public class PinnedTasksDto
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }
        //public DateTime PinnedAt { get; set; }
        // public User PinnedBy { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
