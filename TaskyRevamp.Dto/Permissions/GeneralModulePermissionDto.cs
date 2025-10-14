using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Permissions
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
		public int? DelegationFromUser { get; set; }
		public int? DelegationToUser { get; set; }
		public string? DelegationDepartments { get; set; }
	}
}
