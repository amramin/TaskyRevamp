using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum WeekDays
	{
		[LocalizedDescription("Sunday", typeof(SharedResources))]
		[Order(1)]
		Sunday = 1,
		[LocalizedDescription("Monday", typeof(SharedResources))]
		[Order(2)]
		Monday = 2,
		[LocalizedDescription("Tuesday", typeof(SharedResources))]
		[Order (3)]
		Tuesday = 3,
		[LocalizedDescription("Wednesday", typeof(SharedResources))]
		[Order(4)]
		Wednesday = 4,
		[LocalizedDescription("Thursday", typeof(SharedResources))]
		[Order(5)]
		Thursday = 5,
		[LocalizedDescription("Friday", typeof(SharedResources))]
		[Order(6)]
		Friday = 6,
		[LocalizedDescription("Saturday", typeof(SharedResources))]
		[Order(7)]
		Saturday = 7,
	}
}
