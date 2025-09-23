using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
    public class AddTaskSettings : Entity
    {
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public bool IsMandatory { get; set; }
        public int? Order { get; set; }

        public void Update(AddTaskSettingDto addTaskSettingDto)
        {
            NameEnglish = addTaskSettingDto.NameEnglish;
            NameArabic = addTaskSettingDto.NameArabic;
            IsActive = addTaskSettingDto.IsActive;
            IsMandatory = addTaskSettingDto.IsMandatory;
            Order = addTaskSettingDto.Order;
        }

        public AddTaskSettingDto CopyToDto()
        {
            return new AddTaskSettingDto
            {
                Id = Id,
                NameEnglish = NameEnglish,
                NameArabic = NameArabic,
                IsActive = IsActive,
                IsMandatory = IsMandatory,
                Order = Order
            };
        }
    }
}
