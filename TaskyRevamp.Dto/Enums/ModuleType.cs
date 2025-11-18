using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
    public enum ModuleType
    {
        [LocalizedDescription("SystemNotification", typeof(SharedResources))]

        SystemNotification,
        [LocalizedDescription("Email", typeof(SharedResources))]

        Email,
        [LocalizedDescription("History", typeof(SharedResources))]

        History,

    }
}
