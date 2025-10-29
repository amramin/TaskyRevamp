using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.Permissions.GeneralModule
{
	public class GeneralModulePermissionDto
	{
		public Guid Id { get; set; }
		public Guid PrivilegeId { get; set; }
		public Guid GeneralModuleId { get; set; }
		public bool IsView { get; set; }
		public bool IsEdit { get; set; }
		public bool IsAdd { get; set; }
		public bool IsDelete { get; set; }
		public List<int>? DelegationFromUser { get; set; }
		public DelegationToUser? DelegationToUser { get; set; }
		public List<Guid>? DelegationToUserDepartments { get; set; }
		public List<Guid>? DelegationFromUserDepartments { get; set; }

	}
}
