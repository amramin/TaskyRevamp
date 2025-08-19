using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class PriorityDto
	{
		public Guid Id { get; set; }
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public string NameColor { get; set; }
		public string BackgroundColor { get; set; }
		public int Order { get; set; }
	}
}
