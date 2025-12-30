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
		public static readonly Dictionary<SearchFieldPrivileg, Expression<Func<Privilege, object>>> Map = new()
		{
			{ SearchFieldPrivileg.NameEnglish, x => x.NameEnglish },
			{ SearchFieldPrivileg.NameArabic, x => x.NameArabic },
			{ SearchFieldPrivileg.CreateDate, x => x.CreateDate },
			{ SearchFieldPrivileg.CreatedByEnglish, x => x.CreatedBy.NameEnglish },
			{ SearchFieldPrivileg.CreatedByArabic, x => x.CreatedBy.NameArabic },
			{ SearchFieldPrivileg.UpdateDate, x => x.UpdateDate },
			{ SearchFieldPrivileg.UpdatedByEnglish, x => x.UpdatedBy.NameEnglish },
			{ SearchFieldPrivileg.UpdatedByArabic, x => x.UpdatedBy.NameArabic },
		};
	}
}
