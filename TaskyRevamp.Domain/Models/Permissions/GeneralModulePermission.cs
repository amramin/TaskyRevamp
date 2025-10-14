using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Domain.Models.Permissions
{
	public class GeneralModulePermission : Entity
	{
		public Guid PrivilegeId { get; set; }
		public Privilege Privilege { get; set; }
		public Guid GeneralModuleId { get; set; }
		public GeneralModule GeneralModule { get; set; }
		public bool IsView { get; set; }
		public bool IsEdit { get; set; }
		public bool IsAdd { get; set; }
		public bool IsDelete { get; set; }
		public int? DelegationFromUser { get; set; }
		public int? DelegationToUser { get; set; }
		public string? DelegationDepartments { get; set; }
	}
}
