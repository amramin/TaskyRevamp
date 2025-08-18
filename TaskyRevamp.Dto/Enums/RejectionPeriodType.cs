using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum RejectionPeriodType
	{
		[LocalizedDescription("Hours24", typeof(SharedResources))]
		Hours24 = 1,
		[LocalizedDescription("Days3", typeof(SharedResources))]
		Days3 = 3,
		[LocalizedDescription("Week", typeof(SharedResources))]
		Week = 7,
		[LocalizedDescription("Custom", typeof(SharedResources))]
		Custom = 2,
		[LocalizedDescription("Never", typeof(SharedResources))]
		Never = 0,
	}
}
