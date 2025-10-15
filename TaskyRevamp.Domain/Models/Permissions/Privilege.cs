using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Domain.Models.Permissions
{
	public class Privilege : Entity
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public List<GeneralModulePermission> GeneralModulePermissions { get; set; }
        public List<ReportModulePermission> ReportModulePermissions { get; set; }
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
				GeneralModuleId = x.GeneralModuleId
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
			};
		}
	}
}
