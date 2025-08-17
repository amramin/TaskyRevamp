using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class RejectionSettings : Entity
	{
		public RejectionPeriodType PeriodType { get; private set; }
		public int? CustomDays { get; private set; }
		public RejectionSettings() { }
		public RejectionSettings(RejectionPeriodType periodType, int? customDays)
		{
			PeriodType = periodType;
			CustomDays = customDays;
		}
		public void Update(RejectionPeriodType periodType, int? customDays)
		{
			PeriodType = periodType;
			CustomDays = customDays;
		}

		public RejectionSettingsDto CopyToDto()
		{
			return new RejectionSettingsDto
			{
				Id = Id,
				PeriodType = PeriodType,
				CustomDays = CustomDays,
			};
		}
	}
}
