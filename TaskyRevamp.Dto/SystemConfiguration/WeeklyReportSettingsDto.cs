using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class WeeklyReportSettingsDto
	{
		public Guid Id { get; set; }
		public WeekDays Day { get; set; }
		public TimeSpan Time { get; set; }
		public Language Language { get; set; }
	}
}
