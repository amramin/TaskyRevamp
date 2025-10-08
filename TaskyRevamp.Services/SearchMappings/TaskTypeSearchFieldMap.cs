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
        public static readonly Dictionary<SearchField, Expression<Func<Type, object>>> Map = new()
        {
            { SearchField.NameEnglish, x => x.NameEnglish },
            { SearchField.NameArabic, x => x.NameArabic },
            { SearchField.CreateDate, x => x.CreateDate },
            { SearchField.CreatedBy, x => x.CreatedBy.NameEnglish },
            { SearchField.UpdateDate, x => x.UpdateDate },
            { SearchField.UpdatedBy, x => x.UpdatedBy.NameEnglish },
            { SearchField.ActiveStatus, x => x.IsActive }
        };
    }
}
