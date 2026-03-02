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
		[LocalizedDescription("Dashboard", typeof(SharedResources))]
		[Order(1)]
		Dashboard = 1,
		[LocalizedDescription("List_Overall", typeof(SharedResources))]
		[Order(2)]
		List_Overall = 11,
		[LocalizedDescription("List_Timeline", typeof(SharedResources))]
		[Order(3)]
		List_Timeline = 12,
		[LocalizedDescription("List_Source", typeof(SharedResources))]
		[Order(4)]
		List_Source = 13,
		[LocalizedDescription("List_Type", typeof(SharedResources))]
		[Order(5)]
		List_Type = 14,
		[LocalizedDescription("List_Status", typeof(SharedResources))]
		[Order(6)]
        List_Status = 15,
		[LocalizedDescription("Kanban", typeof(SharedResources))]
		[Order(7)]
		Kanban = 2,
		[LocalizedDescription("Gantt", typeof(SharedResources))]
		[Order(8)]
		Gantt = 3
	}

}
