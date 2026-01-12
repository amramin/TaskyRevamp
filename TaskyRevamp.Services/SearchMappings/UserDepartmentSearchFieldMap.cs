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
	public class UserDepartmentSearchFieldMap
	{
		public static Dictionary<SearchFieldUserDepartment, Expression<Func<User, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchFieldUserDepartment, Expression<Func<User, object>>>
			{
				{ SearchFieldUserDepartment.Name, isArabic ? x => x.NameArabic! : x => x.NameEnglish!},
				{ SearchFieldUserDepartment.Email, x => x.Email },
				{ SearchFieldUserDepartment.PrivilageName, isArabic ? x => x.Privilege!.NameArabic : x => x.Privilege!.NameEnglish },
				{ SearchFieldUserDepartment.IsManager, x => x.IsManager},
			};
		}
	}
}
