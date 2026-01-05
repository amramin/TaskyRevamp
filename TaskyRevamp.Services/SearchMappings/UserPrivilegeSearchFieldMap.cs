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
		public static  Dictionary<SearchFieldUserPrivilege, Expression<Func<User, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchFieldUserPrivilege, Expression<Func<User, object>>>
			{
				{ SearchFieldUserPrivilege.Name, isArabic ? x => x.NameArabic! : x => x.NameEnglish! },
				{ SearchFieldUserPrivilege.Email, x => x.Email! },
				{ SearchFieldUserPrivilege.Department, isArabic ? x => x.Department!.NameArabic : x => x.Department!.NameEnglish },
				{ SearchFieldUserPrivilege.Privilege, isArabic ? x => x.Privilege!.NameArabic : x => x.Privilege!.NameEnglish },
			};
		}
	}
}
