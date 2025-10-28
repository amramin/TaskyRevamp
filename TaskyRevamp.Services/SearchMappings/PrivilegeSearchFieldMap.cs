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
		public static readonly Dictionary<SearchField, Expression<Func<Privilege, object>>> Map = new()
		{
			{ SearchField.NameEnglish, x => x.NameEnglish },
			{ SearchField.NameArabic, x => x.NameArabic },
			{ SearchField.CreateDate, x => x.CreateDate },
			{ SearchField.CreatedBy, x => x.CreatedBy.NameEnglish },
			{ SearchField.UpdateDate, x => x.UpdateDate },
			{ SearchField.UpdatedBy, x => x.UpdatedBy.NameEnglish },
		};
	}
}
