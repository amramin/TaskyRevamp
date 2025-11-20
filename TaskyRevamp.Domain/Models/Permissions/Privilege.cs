using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Permissions.GeneralModule;
using TaskyRevamp.Domain.Models.Permissions.ReportModule;
using TaskyRevamp.Domain.Models.Permissions.TaskModule;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Dto.Permissions.GeneralModule;
using TaskyRevamp.Dto.Permissions.ReportModule;
using TaskyRevamp.Dto.Permissions.TaskModule;

namespace TaskyRevamp.Domain.Models.Permissions
{
	public class Privilege : Entity, IHasCreationMetaData, IHasUpdateMetaData
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public List<GeneralModulePermission> GeneralModulePermissions { get; set; }
        public List<ReportModulePermission> ReportModulePermissions { get; set; }
        public List<TaskModuleUserDepartment> TaskModuleUserDepartmentPermissions { get; set; }
		public List<TaskModuleExternalDepartment>? TaskModuleExternalDepartmentPermissions { get; private set; }
		public Guid? UpdatedById { get; set; }
		public DateTime? UpdateDate { get; set; }
		public User? UpdatedBy { get; set; }
		public Guid CreatedById { get; set; }
		public DateTime CreateDate { get; set; }
		public User CreatedBy { get; set; }

		public Privilege()
		{
			
		}
		public Privilege(string nameEnglish, string nameArabic)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
		}

		public void SetData(PrivilegeDto dto)
		{
			NameEnglish = dto.NameEnglish;
			NameArabic = dto.NameArabic;
			GeneralModulePermissions = dto.GeneralModulePermissions?.Select(x => new GeneralModulePermission
			{
				Id = x.Id,
				IsView = x.IsView,
				IsEdit = x.IsEdit,
				IsAdd = x.IsAdd,
				IsDelete = x.IsDelete,
				GeneralModuleId = x.GeneralModuleId,
				DelegationFromUser = x.DelegationFromUser,
				DelegationToUser = x.DelegationToUser,
				DelegationFromUserDepartments = x.DelegationFromUserDepartments,
				DelegationToUserDepartments = x.DelegationToUserDepartments
			}).ToList() ?? new List<GeneralModulePermission>();
			ReportModulePermissions = dto.ReportModulePermissionDtos?.Select(x => new ReportModulePermission
			{
				Id = x.Id,
				IsActive = x.IsActive,
				ReportModuleId = x.ReportModuleId
			}).ToList() ?? new List<ReportModulePermission>();
			TaskModuleUserDepartmentPermissions = dto.TaskModuleUserDepartmentDto?.Select(x => new TaskModuleUserDepartment
			{
				Id = x.Id,
				SelectedOption = x.SelectedOption,
				IsActive = x.IsActive,
				PermissionId = x.PermissionId,
				IsManagerTasks = x.IsManagerTasks,
				IsEmployeeTasks = x.IsEmployeeTasks,
				//Status = x.Status,
				//Source = x.Source,
				DirectionType = x.DirectionType,
				DirectionLevel = x.DirectionLevel
			}).ToList() ?? new List<TaskModuleUserDepartment>();
			TaskModuleExternalDepartmentPermissions = dto.TaskModuleExternalDepartmentDto?.Select(x => new TaskModuleExternalDepartment
			{
				Id = x.Id,
				DepartmentId = x.DepartmentId,
				IsIncludeSubDepartment = x.IsIncludeSubDepartment,
				IsManagerTasks = x.IsManagerTasks,
				IsEmployeeTasks = x.IsEmployeeTasks,
				PermissionId = x.PermissionId,
				Status = x.Status,
				Source = x.Source,
				DirectionType = x.DirectionType,
				DirectionLevel = x.DirectionLevel
			}).ToList() ?? new List<TaskModuleExternalDepartment>();
		}

        public PrivilegeDto CopyToDto()
		{
			return new PrivilegeDto
			{
				Id = Id,
				NameEnglish = NameEnglish,
				NameArabic = NameArabic,
				CreateDate = CreateDate,
				CreatedById = CreatedById,
				UpdateDate = UpdateDate,
				UpdatedById= UpdatedById,
				GeneralModulePermissions = GeneralModulePermissions?
									  .Select(g => g.CopyToDto())
									  .ToList() ?? new List<GeneralModulePermissionDto>(),
				ReportModulePermissionDtos = ReportModulePermissions?
									  .Select(r => r.CopyToDto())
									  .ToList() ?? new List<ReportModulePermissionDto>(),
				TaskModuleUserDepartmentDto = TaskModuleUserDepartmentPermissions?
									  .Select(t => t.CopyToDto())
									  .ToList() ?? new List<TaskModuleUserDepartmentPermissionDto>(),
				TaskModuleExternalDepartmentDto = TaskModuleExternalDepartmentPermissions?
									  .Select(e => e.CopyToDto())
									  .ToList() ?? new List<TaskModuleExternalDepartmentDto>()
			};
		}
	}
}
