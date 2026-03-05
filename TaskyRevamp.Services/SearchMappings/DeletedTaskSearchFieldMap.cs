using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Enums.SearchFields;

namespace TaskyRevamp.Services.SearchMappings
{
	public class DeletedTaskSearchFieldMap
	{
		public static Dictionary<SearchFieldDeletedTask, Expression<Func<TaskItem, object>>> Map(string culture)
		{
			bool isArabic = culture == "ar";
			return new Dictionary<SearchFieldDeletedTask, Expression<Func<TaskItem, object>>>
			{
				{ SearchFieldDeletedTask.Title,  x => x.Title },
				{ SearchFieldDeletedTask.StartDate, x => x.StartDate! },
				{ SearchFieldDeletedTask.EndDate, x => x.EndDate! },
				{ SearchFieldDeletedTask.DeletionDate, x => x.DeleteDate! },
				{ SearchFieldDeletedTask.DeletedBy, isArabic? x => x.DeletedBy!.NameArabic! : x => x.DeletedBy!.NameEnglish! },
				{ SearchFieldDeletedTask.Priority, isArabic? x => x.Priority!.NameArabic! : x => x.Priority!.NameEnglish! },
			};
		}
	}
}
