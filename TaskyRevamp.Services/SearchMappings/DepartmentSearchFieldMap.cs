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
        public static Dictionary<SearchFieldDepartment, Expression<Func<Department, object>>> Map(string culture)
        {
            bool isArabic = culture == "ar";
            return new Dictionary<SearchFieldDepartment, Expression<Func<Department, object>>>
            {
                { SearchFieldDepartment.Name, isArabic ? x => x.NameArabic : x => x.NameEnglish },
                { SearchFieldDepartment.ParentName, isArabic ? x => x.Parentdepartment.NameArabic : x => x.Parentdepartment.NameEnglish },
                { SearchFieldDepartment.Level, x => x.Level },
                { SearchFieldDepartment.CreateDate, x => x.CreateDate },
                { SearchFieldDepartment.CreatedBy, isArabic ? x => x.CreatedBy.NameArabic! : x => x.CreatedBy.NameEnglish! },
                { SearchFieldDepartment.UpdateDate, x => x.UpdateDate! },
                { SearchFieldDepartment.UpdatedBy, isArabic ? x => x.UpdatedBy!.NameArabic! : x => x.UpdatedBy!.NameEnglish! },
            };
        }
    }
}
