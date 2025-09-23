using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
    public class FilterFieldsSettings : Entity
    {
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public int? Order { get; set; }

        public void Update(FilterFieldsSettingDto filterFieldsSettingDto)
        {
            NameEnglish = filterFieldsSettingDto.NameEnglish;
            NameArabic = filterFieldsSettingDto.NameArabic;
            IsActive = filterFieldsSettingDto.IsActive;
            Order = filterFieldsSettingDto.Order;
        }

        public FilterFieldsSettingDto CopyToDto()
        {
            return new FilterFieldsSettingDto
            {
                Id = Id,
                NameEnglish = NameEnglish,
                NameArabic = NameArabic,
                IsActive = IsActive,
                Order = Order
            };
        }
    }
}
