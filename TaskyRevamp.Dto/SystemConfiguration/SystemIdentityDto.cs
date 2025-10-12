using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class SystemIdentityDto
	{
		public Guid Id { get; set; }
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public string PrimaryColor { get; set; }
		public string PrimaryActiveColor { get; set; }
		public string MainTitle { get; set; }
		public string SubTitle { get; set; }
		public string NavigationBackground { get; set; }
		public string BorderColor { get; set; }
		public byte[] Logo { get; set; }
	}
}
