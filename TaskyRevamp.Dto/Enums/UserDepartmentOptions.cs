using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum UserDepartmentOptions
	{
		[Order(1)]
		[LocalizedDescription("CreateTaskOnHim", typeof(SharedResources))]
		CreateTaskOnHim,
		[Order(2)]
		[LocalizedDescription("SameDepartment", typeof(SharedResources))]
		SameDepartment,
		[Order(3)]
		[LocalizedDescription("PeersLevel", typeof(SharedResources))]
		PeersLevel,
		[Order(4)]
		[LocalizedDescription("BelowUserDepartmentLevel", typeof(SharedResources))]
		BelowUserDepartmentLevel,
		[Order(5)]
		[LocalizedDescription("AboveUserDepartmentLevel", typeof(SharedResources))]
		AboveUserDepartmentLevel
	}
}
