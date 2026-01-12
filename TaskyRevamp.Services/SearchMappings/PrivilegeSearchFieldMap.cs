using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Dto.Enums.SearchFields;

namespace TaskyRevamp.Services.SearchMappings
{
	public class PrivilegeSearchFieldMap
	{
		public static Dictionary<SearchFieldPrivileg, Expression<Func<Privilege, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchFieldPrivileg, Expression<Func<Privilege, object>>>
			{
				{ SearchFieldPrivileg.Name, isArabic? x => x.NameArabic : x => x.NameEnglish },
				{ SearchFieldPrivileg.CreateDate, x => x.CreateDate },
				{ SearchFieldPrivileg.CreatedBy, isArabic? x => x.CreatedBy.NameArabic! : x => x.CreatedBy.NameEnglish! },
				{ SearchFieldPrivileg.UpdateDate, x => x.UpdateDate! },
				{ SearchFieldPrivileg.UpdatedBy, isArabic? x => x.UpdatedBy!.NameArabic! : x => x.UpdatedBy!.NameEnglish! },
			};
		}
	}
}
