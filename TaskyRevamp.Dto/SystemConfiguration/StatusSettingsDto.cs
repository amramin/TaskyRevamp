using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class StatusSettingsDto
	{
		public Guid Id { get; set; }
		[Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
		public string NameEnglish { get; set; }
		[Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
		public string NameArabic { get; set; }
		[Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
		public string NameColor { get; set; }
		[Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
		public string BackgroundColor { get; set; }
	}
}
