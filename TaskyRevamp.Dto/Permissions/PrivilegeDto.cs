using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Permissions
{
	public class PrivilegeDto
	{
		public Guid Id { get; set; }
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
        public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? NameArabic : NameEnglish;
		public List<GeneralModulePermissionDto> GeneralModulePermissions { get; set; } = new List<GeneralModulePermissionDto>();
        public List<ReportModulePermissionDto> ReportModulePermissionDtos { get; set; } = new List<ReportModulePermissionDto>();
    }
}
