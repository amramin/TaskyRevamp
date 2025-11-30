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
    public class DepartmentSearchFieldDepartmentMap
    {
        public static readonly Dictionary<SearchFieldDepartment, Expression<Func<Department, object>>> Map = new()
        {
            { SearchFieldDepartment.NameEnglish, x => x.NameEnglish },
            { SearchFieldDepartment.NameArabic, x => x.NameArabic },
            { SearchFieldDepartment.ParentNameEN, x => x.Parentdepartment.NameEnglish },
            { SearchFieldDepartment.ParentNameAR, x => x.Parentdepartment.NameArabic },
            { SearchFieldDepartment.Level, x => x.Level },
            { SearchFieldDepartment.CreateDate, x => x.CreateDate },
            { SearchFieldDepartment.CreatedBy, x => x.CreatedBy.NameEnglish },
            { SearchFieldDepartment.UpdateDate, x => x.UpdateDate },
            { SearchFieldDepartment.UpdatedBy, x => x.UpdatedBy.NameEnglish }
        };
    }
}
