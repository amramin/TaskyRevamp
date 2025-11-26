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
		public static readonly Dictionary<SearchFieldUserDepartment, Expression<Func<User, object>>> Map = new()
		{
			{ SearchFieldUserDepartment.NameEnglish, x => x. NameEnglish},
			{ SearchFieldUserDepartment.NameArabic, x => x.NameArabic },
			{ SearchFieldUserDepartment.Email, x => x.Email },
			{ SearchFieldUserDepartment.Privilage, x => x.Privilege.NameEnglish },
			{ SearchFieldUserDepartment.IsManager, x => x.IsManager},
		};
	}
}
