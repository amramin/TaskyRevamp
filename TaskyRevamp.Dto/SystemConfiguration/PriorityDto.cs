using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;


namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class PriorityDto
	{
		public Guid Id { get; set; }
		//[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(TaskyRevamp.Localization.Resources.SharedResources))]
		[Required]
		public string NameEnglish { get; set; }
		[Required]
		public string NameArabic { get; set; }
		[Required]
		public string NameColor { get; set; }
		[Required]
		public string BackgroundColor { get; set; }
		public int Order { get; set; }
	}
}
