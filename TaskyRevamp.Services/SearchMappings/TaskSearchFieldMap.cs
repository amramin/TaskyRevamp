using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SearchMappings
{
    public class TaskSearchFieldMap
    {
        public static readonly Dictionary<SearchFieldTask, Expression<Func<TaskItem, object>>> Map = new()
        {
            { SearchFieldTask.TitleEnglish, x => x.TitleEnglish},
            { SearchFieldTask.TitleArabic, x => x.TitleArabic },
             { SearchFieldTask.Level, x => x.Level },
            { SearchFieldTask.CreateDate, x => x.CreateDate },
            { SearchFieldTask.CreatedBy, x => x.CreatedBy.NameEnglish },
            { SearchFieldTask.UpdateDate, x => x.UpdateDate },
            { SearchFieldTask.UpdatedBy, x => x.UpdatedBy.NameEnglish }
        };
    }
}
