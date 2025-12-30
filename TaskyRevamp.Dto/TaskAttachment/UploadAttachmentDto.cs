using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskAttachment
{
	public class UploadAttachmentDto
	{
		public string FileName { get; set; } = default!;
		public byte[] Bytes { get; set; } = default!;
		public long Size { get; set; }
	}
}
