using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums.SearchFields;

namespace TaskyRevamp.Services.SearchMappings
{
	public class UserPrivilegeSearchFieldMap
	{
		public static readonly Dictionary<SearchFieldUserPrivilege, Expression<Func<User, object>>> Map = new()
		{
			{ SearchFieldUserPrivilege.NameEnglish, x => x.NameEnglish },
			{ SearchFieldUserPrivilege.NameArabic, x => x.NameArabic },
			{ SearchFieldUserPrivilege.Email, x => x.Email },
			{ SearchFieldUserPrivilege.DepartmentEnglish, x => x.Department.NameEnglish },
			{ SearchFieldUserPrivilege.PrivilegeEnglish, x => x.Privilege.NameEnglish },
			{ SearchFieldUserPrivilege.DepartmentArabic, x => x.Department.NameArabic },
			{ SearchFieldUserPrivilege.PrivilegeArabic, x => x.Privilege.NameArabic },
		};
	}
}
