using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class WorkingDaysSettingsDto
	{
		public Guid Id { get; set; }
		public WeekDays Day { get; set; }
		public bool IsActive { get; set; }
	}
}
