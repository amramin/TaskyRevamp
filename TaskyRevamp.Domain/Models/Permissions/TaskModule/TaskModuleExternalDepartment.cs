using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Permissions.TaskModule;

namespace TaskyRevamp.Domain.Models.Permissions.TaskModule
{
	public class TaskModuleExternalDepartment : Entity
	{
		public Guid PrivilegeId { get; set; }
		public Privilege Privilege { get; set; }
		public Guid DepartmentId { get; set; }
		public Department Department { get; set; }
		public bool IsIncludeSubDepartment { get; set; }
		public bool IsManagerTasks { get; set; }
		public bool IsEmployeeTasks { get; set; }
		public List<int>? PermissionId { get; set; }
		public List<Guid> Status { get; set; }
		public List<Guid> Source { get; set; }
		public DirectionType? DirectionType { get; set; }
		public long? DirectionLevel { get; set; }
		public TaskModuleExternalDepartmentDto CopyToDto()
		{
			return new TaskModuleExternalDepartmentDto
			{
				Id = Id,
				PrivilegeId = PrivilegeId,
				DepartmentId = DepartmentId,
				IsIncludeSubDepartment = IsIncludeSubDepartment,
				IsManagerTasks = IsManagerTasks,
				IsEmployeeTasks = IsEmployeeTasks,
				PermissionId = PermissionId,
				Status = Status,
				Source = Source,
				DirectionType = DirectionType,
				DirectionLevel = DirectionLevel
			};
		}
	}


}
