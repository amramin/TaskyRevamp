using System.ComponentModel.DataAnnotations;
using TaskyRevamp.Dto.Permissions.GeneralModule;
using TaskyRevamp.Dto.Permissions.ReportModule;
using TaskyRevamp.Dto.Permissions.TaskModule;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Permissions
{
	public class PrivilegeDto
	{
		public Guid Id { get; set; }

		[Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
		public string NameEnglish { get; set; }

		[Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
		public string NameArabic { get; set; }
		public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? NameArabic : NameEnglish;
		public Guid CreatedById { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid? UpdatedById { get; set; }
		public DateTime? UpdateDate { get; set; }
		public List<GeneralModulePermissionDto> GeneralModulePermissions { get; set; } = new List<GeneralModulePermissionDto>();
		public List<ReportModulePermissionDto> ReportModulePermissionDtos { get; set; } = new List<ReportModulePermissionDto>();
		public List<TaskModuleUserDepartmentPermissionDto> TaskModuleUserDepartmentDto { get; set; } = new List<TaskModuleUserDepartmentPermissionDto>();
		public List<TaskModuleExternalDepartmentDto> TaskModuleExternalDepartmentDto { get; set; } = new List<TaskModuleExternalDepartmentDto>();
	}
	public class PrivilegeDtoWithName
	{
		public PrivilegeDto PrivilegeDto { get; set; }
		public string CreatedByName { get; set; }
		public string UpdatedByName { get; set; }
	}
}
