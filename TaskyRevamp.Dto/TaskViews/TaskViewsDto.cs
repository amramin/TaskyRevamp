using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskViews
{
	public class TaskViewsDto
	{
		public Guid Id { get; set; }
		public Guid TaskItemId { get; set; }
		public Guid UserId { get; set; }
		public DateTime ViewdAt { get; set; }
		public string FullName { get; set; }
		public bool IsActive { get; set; }
	}
}
