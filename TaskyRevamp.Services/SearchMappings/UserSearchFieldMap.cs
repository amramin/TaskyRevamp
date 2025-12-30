using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums.SearchFields;

namespace TaskyRevamp.Services.SearchMappings
{
	public class UserSearchFieldMap
	{
		public static readonly Dictionary<SearchFieldUser, Expression<Func<User, object>>> Map = new()
		{
			{ SearchFieldUser.NameEnglish, x => x.NameEnglish },
			{ SearchFieldUser.NameArabic, x => x.NameArabic },
			//{ SearchFieldUser.CreateDate, x => x.CreateDate },
			{ SearchFieldUser.UpdateDate, x => x.UpdateDate },
			{ SearchFieldUser.UpdatedByEnglish, x => x.UpdatedBy.NameEnglish },
			{ SearchFieldUser.UpdatedByArabic, x => x.UpdatedBy.NameArabic },
			{ SearchFieldUser.ActiveStatus, x => x.IsActive },
			{ SearchFieldUser.Email, x => x.Email },
			{ SearchFieldUser.Department, x => x.Department.NameEnglish },
			{ SearchFieldUser.Privilege, x => x.Privilege.NameEnglish },
			{ SearchFieldUser.IsManager, x => x.IsManager }
		};
	}
}
