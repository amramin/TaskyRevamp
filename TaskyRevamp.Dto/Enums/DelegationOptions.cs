using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
    public enum DelegationOptions
    {
        [LocalizedDescription("UserTasks", typeof(SharedResources))]

        UserTasks=1,
        [LocalizedDescription("Privilegesandusertasks", typeof(SharedResources))]

        Privilegesandusertasks=2
    }
}
