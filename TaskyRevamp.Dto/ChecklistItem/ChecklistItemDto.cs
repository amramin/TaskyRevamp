using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.ChecklistItem
{
    public class ChecklistItemDto
    {
        public Guid Id { get; set; }
        public Guid TaskChecklistId { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? AssignedUserId { get; set; }
		public Guid CreatedById { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
