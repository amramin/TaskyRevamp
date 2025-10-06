using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class SourceDto
	{
		public Guid Id { get; set; }
		[Required]
		public string NameEnglish { get; set; }
		[Required]
		public string NameArabic { get; set; }

		public string DisplayedName { 
			get 
			{ 
				return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? NameArabic : NameEnglish; 
			}
		}
		public bool IsActive { get; set; }
		public Guid CreatedById { get; set; }
		public DateTime CreateDate { get; set; }
		//public string CreatedByName { get; set; }
		public Guid? UpdatedById { get; set; }
		public DateTime? UpdateDate { get; set; }
		//public string? UpdatedByName { get; set; }
	}

	public class SourceDtoWithName
	{
		public SourceDto Source { get; set; }
		public string CreatedByName { get; set; }
		public string UpdatedByName { get; set; }

	}
}
