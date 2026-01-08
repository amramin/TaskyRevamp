using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SearchMappings
{
	public class TaskSourceSearchFieldMap
	{
		public static Dictionary<SearchField, Expression<Func<Source, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchField, Expression<Func<Source, object>>>
			{
				{ SearchField.Name, isArabic?  x => x.NameArabic : x => x.NameEnglish },
				{ SearchField.CreateDate, x => x.CreateDate },
				{ SearchField.CreatedBy,  isArabic?  x => x.CreatedBy.NameArabic! : x => x.CreatedBy.NameEnglish! },
				{ SearchField.UpdateDate, x => x.UpdateDate! },
				{ SearchField.UpdatedBy, isArabic?  x => x.UpdatedBy!.NameArabic! : x => x.UpdatedBy!.NameEnglish! },
				{ SearchField.ActiveStatus, x => x.IsActive }
			};
		}
	}
}
