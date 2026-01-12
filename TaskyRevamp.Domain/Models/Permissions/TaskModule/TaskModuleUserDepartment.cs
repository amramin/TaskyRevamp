using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Permissions.TaskModule;

namespace TaskyRevamp.Domain.Models.Permissions.TaskModule
{
	public class TaskModuleUserDepartment : Entity
	{
		public Guid PrivilegeId { get; set; }
		public Privilege Privilege { get; set; }
		public UserDepartmentOptions SelectedOption { get; set; }
		public bool IsActive { get; set; }
		public List<int>? PermissionId { get; set; }
		public bool? IsManagerTasks { get; set; }
		public bool? IsEmployeeTasks { get; set; }
		//public List<Guid>? Status { get; set; }
		//public List<Guid>? Source { get; set; }
		public DirectionType? DirectionType { get; set; }
		public long? DirectionLevel { get; set; }
		public TaskModuleUserDepartmentPermissionDto CopyToDto()
		{
			return new TaskModuleUserDepartmentPermissionDto {
				Id = Id,
				PrivilegeId = PrivilegeId,
				SelectedOption = SelectedOption,
				IsActive = IsActive,
				PermissionId = PermissionId,
				IsManagerTasks = IsManagerTasks,
				IsEmployeeTasks = IsEmployeeTasks,
				//Status = Status,
				//Source = Source,
				DirectionType = DirectionType,
				DirectionLevel = DirectionLevel
			};
		}
	}
}
