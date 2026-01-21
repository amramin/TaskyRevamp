using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Enums
{
    public enum TimeLineView
    {
        [Description("3f7c6e1a-9d42-4b8f-9c1e-8b9a2f6c4d21")]
        Delayed = 1,

        [Description("a1e4b5c9-72f6-4d03-8f9b-6d2c1a7e5b44")]
        Today,

        [Description("e9d2c7b4-1f6a-4c88-9a3e-0b5f8d1c2e77")]
        ThisWeek,

        [Description("5b8a1d4e-3c72-4f9b-9e61-2a7c6d0f4b93")]
        ThisMonth,

        [Description("c4e6a8b1-9f53-4d27-b2c8-7a1e5d3f609e")]
        NextMonths
    }
}
