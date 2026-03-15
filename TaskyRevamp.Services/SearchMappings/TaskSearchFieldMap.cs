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
            { SearchFieldTask.Title, x =>   x.Title},
            { SearchFieldTask.Priority, x => x.Priority },
             { SearchFieldTask.StartDate, x => x.StartDate },
             { SearchFieldTask.EndDate, x =>   x.EndDate},
            { SearchFieldTask.weight, x => x.ActualWeight },
             { SearchFieldTask.AssigneduserNames, x => x.TaskAssignees.Select(k=>k.User.NameEnglish) },
                         { SearchFieldTask.TaskStatus, x =>   x.status.NameEnglish},
            { SearchFieldTask.CreateDate, x => x.CreateDate },
            { SearchFieldTask.CreatedBy, x => x.CreatedBy.NameEnglish },
            { SearchFieldTask.UpdateDate, x => x.UpdateDate },
            { SearchFieldTask.UpdatedBy, x => x.UpdatedBy.NameEnglish },
            {SearchFieldTask.PlannedProgress,x=>x.PlannedProgress }
        };
    }
}
