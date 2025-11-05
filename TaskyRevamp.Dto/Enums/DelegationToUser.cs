using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum DelegationToUser
	{
		[Order(1)]
		[LocalizedDescription("SameDepartment", typeof(SharedResources))]
		SameDepartment = 1,
		[Order(2)]
		[LocalizedDescription("DifferentDepartment", typeof(SharedResources))]
		DifferentDepartment = 2,
	}
}
