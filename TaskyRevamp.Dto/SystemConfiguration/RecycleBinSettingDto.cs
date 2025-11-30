using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.SystemConfiguration
{
    public class RecycleBinSettingDto
    {
        public Guid Id { get; set; }
        public PeriodType PeriodType { get; set; }
        public long? CustomDays { get; set; }
    }
}
