using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
    public class RecycleBinSettings : Entity
    {
        public PeriodType PeriodType { get; private set; }
        public long? CustomDays { get; private set; }

        public RecycleBinSettings()
        {
        }

        public RecycleBinSettings(PeriodType periodType, long? customDays)
        {
            PeriodType = periodType;
            CustomDays = customDays;
        }

        public void Update(PeriodType periodType, long? customDays)
        {
            PeriodType = periodType;
            CustomDays = customDays;
        }

        public RecycleBinSettingDto CopyToDto()
        {
            return new RecycleBinSettingDto
            {
                Id = Id,
                PeriodType = PeriodType,
                CustomDays = CustomDays
            };
        }

    }
}
