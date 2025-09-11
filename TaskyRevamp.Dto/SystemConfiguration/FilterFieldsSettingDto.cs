using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.SystemConfiguration
{
    public class FilterFieldsSettingDto
    {
        public Guid Id { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? NameArabic : NameEnglish;
        public bool IsActive { get; set; }
        public int? Order { get; set; }
    }
}
