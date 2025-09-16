using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class WorkingDaysSettings : Entity
	{
		public WeekDays Day {  get; set; }
		public bool IsActive { get; set; }

		public WorkingDaysSettings()
		{
			
		}

		public WorkingDaysSettings(WeekDays day, bool isActive)
		{
			Day = day;	
			IsActive = isActive;
		}

		public WorkingDaysSettingsDto CopyToDto()
		{
			return new WorkingDaysSettingsDto
			{
				Id = Id,
				Day = Day,
				IsActive = IsActive
			};
		}
	}
}
