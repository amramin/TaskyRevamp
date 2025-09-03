using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum ViewType
	{
		[LocalizedDescription("List", typeof(SharedResources))]
		List = 1,
			[LocalizedDescription("List_Dashboard", typeof(SharedResources))]
			List_Dashboard = 11,
			[LocalizedDescription("List_Overall", typeof(SharedResources))]
			List_Overall = 12,
			[LocalizedDescription("List_Timeline", typeof(SharedResources))]
			List_Timeline = 13,
			[LocalizedDescription("List_Source", typeof(SharedResources))]
			List_Source = 14,
			[LocalizedDescription("List_Type", typeof(SharedResources))]
			List_Type = 15,
			[LocalizedDescription("List_Card", typeof(SharedResources))]
			List_Card = 16,
			[LocalizedDescription("List_Kanban", typeof(SharedResources))]
			List_Kanban = 17,
			[LocalizedDescription("List_Calendar", typeof(SharedResources))]
			List_Calendar = 18,
		[LocalizedDescription("Kanban", typeof(SharedResources))]
		Kanban = 2,
		[LocalizedDescription("Gantt", typeof(SharedResources))]
		Gantt = 3
	}

}
