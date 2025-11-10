using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.Permissions.TaskModule
{
	public class TaskModuleUserDepartmentPermissionDto
	{
		public Guid Id { get; set; }
		public Guid PrivilegeId { get; set; }
		public UserDepartmentOptions SelectedOption { get; set; }
		public bool IsActive { get; set; }
		public List<int>? PermissionId { get; set; }
		public bool? IsManagerTasks { get; set; }
		public bool? IsEmployeeTasks { get; set; }
		//public List<Guid>? Status { get; set; }
		//public List<Guid>? Source { get; set; }
		public DirectionType? DirectionType { get; set; }
		public int? DirectionLevel { get; set; }
	}
}
