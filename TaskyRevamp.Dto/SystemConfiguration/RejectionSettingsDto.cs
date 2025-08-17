using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class RejectionSettingsDto
	{
		public Guid Id { get; set; }
		public RejectionPeriodType PeriodType { get; set; }
		public int? CustomDays { get; set; }
	}
}
