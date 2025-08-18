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
        [Order(1)]
        Never = 0,
        [LocalizedDescription("Custom", typeof(SharedResources))]
        [Order(6)]
        Custom = 1,
        [LocalizedDescription("Days7", typeof(SharedResources))]
        [Order(2)]
        Days7 = 7,
        [LocalizedDescription("Days15", typeof(SharedResources))]
        [Order(3)]
        Days15 = 15,
        [LocalizedDescription("Days30", typeof(SharedResources))]
        [Order(4)]
        Days30 = 30,
        [LocalizedDescription("Days60", typeof(SharedResources))]
        [Order(5)]
        Days60 = 60
    }
}
