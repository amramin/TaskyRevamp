using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.Permissions.TaskModule
{
	public class TaskModuleExternalDepartmentDto
	{
		public Guid Id { get; set; }
		public Guid PrivilegeId { get; set; }
		public Guid DepartmentId { get; set; }
		public bool IsIncludeSubDepartment { get; set; }
		public bool IsManagerTasks { get; set; }
		public bool IsEmployeeTasks { get; set; }
		public List<int>? PermissionId { get; set; }
		public List<Guid> Status { get; set; }
		public List<Guid> Source { get; set; }
		public DirectionType? DirectionType { get; set; }
		public long? DirectionLevel { get; set; }

	}
}
