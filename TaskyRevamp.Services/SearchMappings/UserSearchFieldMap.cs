using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums.SearchFields;

namespace TaskyRevamp.Services.SearchMappings
{
	public class UserSearchFieldMap
	{
		public static Dictionary<SearchFieldUser, Expression<Func<User, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchFieldUser, Expression<Func<User, object>>>
			{
				{ SearchFieldUser.Name, isArabic ? x => x.NameArabic! : x => x.NameEnglish! },
				//{ SearchFieldUser.CreateDate, x => x.CreateDate },
				{ SearchFieldUser.UpdateDate, x => x.UpdateDate! },
				{ SearchFieldUser.UpdatedBy, isArabic ? x => x.UpdatedBy!.NameArabic! : x => x.UpdatedBy!.NameEnglish! },
				{ SearchFieldUser.ActiveStatus, x => x.IsActive },
				{ SearchFieldUser.Email, x => x.Email! },
				{ SearchFieldUser.Department, isArabic ? x => x.Department!.NameArabic : x => x.Department!.NameEnglish },
				{ SearchFieldUser.Privilege, isArabic ? x => x.Privilege!.NameArabic : x => x.Privilege!.NameEnglish },
				{ SearchFieldUser.IsManager, x => x.IsManager }
			};
		}
	}
}
