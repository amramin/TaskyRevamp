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
        public Privilege()
		{
			
		}
		public Privilege(string nameEnglish, string nameArabic)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
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
