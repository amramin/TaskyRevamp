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
			[LocalizedDescription("Dashboard", typeof(SharedResources))]
			List_Dashboard = 11,
			[LocalizedDescription("Overall", typeof(SharedResources))]
			List_Overall = 12,
			[LocalizedDescription("Timeline", typeof(SharedResources))]
			List_Timeline = 13,
			[LocalizedDescription("Source", typeof(SharedResources))]
			List_Source = 14,
			[LocalizedDescription("Type", typeof(SharedResources))]
			List_Type = 15,
			[LocalizedDescription("Card", typeof(SharedResources))]
			List_Card = 16,
			[LocalizedDescription("kanbanView", typeof(SharedResources))]
			List_kanban = 17,
			[LocalizedDescription("Calendar", typeof(SharedResources))]
			List_Calendar = 18,
		[LocalizedDescription("Kanban", typeof(SharedResources))]
		Kanban = 2,
		[LocalizedDescription("Gantt", typeof(SharedResources))]
		Gantt = 3
	}

}
