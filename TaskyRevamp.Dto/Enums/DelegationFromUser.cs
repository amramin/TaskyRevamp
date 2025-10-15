using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum DelegationFromUser
	{
		[Order(1)]
		[LocalizedDescription("SameUser", typeof(SharedResources))]
		SameUser = 1,
		[Order(2)]
		[LocalizedDescription("SameDepartment", typeof(SharedResources))]
		SameDepartment = 2,
		[Order(3)]
		[LocalizedDescription("DifferentDepartments", typeof(SharedResources))]
		DifferentDepartments = 3
	}
}
