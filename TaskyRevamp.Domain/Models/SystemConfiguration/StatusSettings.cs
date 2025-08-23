using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class StatusSettings : Entity
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public string NameColor { get; set; }
		public string BackgroundColor { get; set; }
		public StatusSettings()
		{
			
		}

		public StatusSettings(string nameEnglish, string nameArabic, string nameColor, string backgroundColor)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
			NameColor = nameColor;
			BackgroundColor = backgroundColor;
		}
		public StatusSettingsDto CopyToDto()
		{
			return new StatusSettingsDto
			{
				Id = Id,
				NameEnglish = NameEnglish,
				NameArabic = NameArabic,
				NameColor = NameColor,
				BackgroundColor = BackgroundColor
			};
		}
	}
}
