using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
    public enum PeriodType
    {
        [LocalizedDescription("Never", typeof(SharedResources))]
        Never = 0,
        [LocalizedDescription("Custom", typeof(SharedResources))]
        Custom = 1,
        [LocalizedDescription("Days7", typeof(SharedResources))]
        Days7 = 7,
        [LocalizedDescription("Days15", typeof(SharedResources))]
        Days15 = 15,
        [LocalizedDescription("Days30", typeof(SharedResources))]
        Days30 = 30,
        [LocalizedDescription("Days60", typeof(SharedResources))]
        Days60 = 60
    }
}
