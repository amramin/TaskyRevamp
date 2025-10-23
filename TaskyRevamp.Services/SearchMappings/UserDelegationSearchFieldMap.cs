using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users.UserDelegations;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SearchMappings
{
    public class UserDelegationSearchFielMap
    {
        public static readonly Dictionary<SearchFieldDelegation, Expression<Func<UserDelegation, object>>> Map = new()
        {
            { SearchFieldDelegation.FromUser, x => x.FromUser.NameEnglish },
            { SearchFieldDelegation.ToUser, x => x.Touser.NameEnglish },
             { SearchFieldDelegation.FromDate, x => x.FromDate },
                          { SearchFieldDelegation.ToDate, x => x.ToDate },

            { SearchFieldDelegation.CreateDate, x => x.CreateDate },
            { SearchFieldDelegation.CreatedBy, x => x.CreatedBy.NameEnglish },
            { SearchFieldDelegation.UpdateDate, x => x.UpdateDate },
            { SearchFieldDelegation.UpdatedBy, x => x.UpdatedBy.NameEnglish }
        };
    }
}
