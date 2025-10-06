using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class WeeklyReportSettings : Entity
	{
		public WeekDays Day { get; set; }
		public TimeSpan Time {  get; set; }
		public Language Language { get; set; }

		public WeeklyReportSettings()
		{
			
		}
		public WeeklyReportSettings(WeekDays day, TimeSpan time, Language language)
		{
			Day = day;
			Time = time;
			Language = language;
		}

		public WeeklyReportSettingsDto CopyToDto()
		{
			return new WeeklyReportSettingsDto
			{
				Id = Id,
				Day = Day,
				Time = Time,
				Language = Language,
			};
		}
	}
}
