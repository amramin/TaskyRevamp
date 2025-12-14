using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskAttachment
{
	public class TaskAttachmentDto
	{
		public Guid Id { get; set; }
		public Guid TaskItemId { get; set; }
	}
}
