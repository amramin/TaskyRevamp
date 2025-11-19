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
		[Order(1)]
        Hours24 = 1,
		[LocalizedDescription("Days3", typeof(SharedResources))]
		[Order(2)]
        Days3 = 3,
		[LocalizedDescription("Week", typeof(SharedResources))]
		[Order(3)]
        Week = 7,
		[LocalizedDescription("Custom", typeof(SharedResources))]
		[Order(4)]
        Custom = 2,
		[LocalizedDescription("Never", typeof(SharedResources))]
		[Order(5)]
        Never = 0,
	}
}
