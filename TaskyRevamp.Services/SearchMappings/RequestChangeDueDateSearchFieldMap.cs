using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Enums.SearchFields;

namespace TaskyRevamp.Services.SearchMappings
{
    public class RequestChangeDueDateSearchFieldMap
    {
        public static Dictionary<SearchFieldChangeDueDate, Expression<Func<ChangeEndDateRequest, object>>> Map(string culture)
        {
            bool isArabic = culture == "ar";
            return new Dictionary<SearchFieldChangeDueDate, Expression<Func<ChangeEndDateRequest, object>>>
            {
                { SearchFieldChangeDueDate.TaskTitle, x=>x.Task.Title},
                { SearchFieldChangeDueDate.TaskStatus, x=>x.Task.status},
                { SearchFieldChangeDueDate.EndDate,  x=>x.Task.EndDate },
                { SearchFieldChangeDueDate.RequestEdendDate, x => x.NewEndDate },
                { SearchFieldChangeDueDate.Requester, isArabic?  x => x.CreatedBy!.NameArabic! : x => x.CreatedBy!.NameArabic! },
                { SearchFieldChangeDueDate.RequestReason, x => x.Reason },
               // { SearchFieldChangeDueDate.ActionStatus, x => x.Status.ToString() }

            };
        }
    }
}
