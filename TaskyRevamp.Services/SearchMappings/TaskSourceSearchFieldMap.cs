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
        public static readonly Dictionary<SearchField, Expression<Func<Source, object>>> Map = new()
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
