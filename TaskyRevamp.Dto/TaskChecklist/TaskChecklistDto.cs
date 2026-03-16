using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.ChecklistItem;

namespace TaskyRevamp.Dto.TaskChecklist
{
    public class TaskChecklistDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public List<ChecklistItemDto> Items { get; set; } = new();
	}
}
