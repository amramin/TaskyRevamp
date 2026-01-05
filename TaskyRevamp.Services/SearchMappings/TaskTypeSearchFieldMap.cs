using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.SystemConfiguration;
using Type = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SearchMappings
{
    public class TaskTypeSearchFieldMap
    {
		public static Dictionary<SearchField, Expression<Func<Type, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchField, Expression<Func<Type, object>>>
			{
				{ SearchField.Name, isArabic?  x => x.NameArabic : x => x.NameEnglish },
				{ SearchField.CreateDate, x => x.CreateDate },
				{ SearchField.CreatedBy,  isArabic?  x => x.CreatedBy!.NameArabic! : x => x.CreatedBy!.NameEnglish! },
				{ SearchField.UpdateDate, x => x.UpdateDate! },
				{ SearchField.UpdatedBy, isArabic?  x => x.UpdatedBy!.NameArabic! : x => x.UpdatedBy!.NameEnglish! },
				{ SearchField.ActiveStatus, x => x.IsActive }
			};
		}
	}
}
