using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Domain.Models.Permissions
{
	public class Privilege : Entity, IHasCreationMetaData, IHasUpdateMetaData
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public List<GeneralModulePermission> GeneralModulePermissions { get; set; }
        public List<ReportModulePermission> ReportModulePermissions { get; set; }
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
			};
		}
	}
}
