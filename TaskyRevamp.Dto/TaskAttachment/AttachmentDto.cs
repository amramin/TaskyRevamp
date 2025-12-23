using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskAttachment
{
	public class AttachmentDto
	{
		public Guid Id { get; set; }
		public Guid TaskAttachmentId { get; set; }
		public Guid FileId { get; set; }
		public string FileName { get; set; }
		public long Size { get; set; }
		public Guid CreatedById { get; set; }
		public DateTime CreateDate { get; set; }

	}
	public class AttachmentWithNameDto
	{
		public AttachmentDto AttachmentDto { get; set; }
		public string CreatedByName { get; set; }
		
	}
}
