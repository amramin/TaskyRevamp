using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum Language
	{
		[LocalizedDescription("English", typeof(SharedResources))]
		[Order(1)]
		English = 0,
		[LocalizedDescription("Arabic", typeof(SharedResources))]
		[Order(2)]
		Arabic = 1,
		
	}
}
