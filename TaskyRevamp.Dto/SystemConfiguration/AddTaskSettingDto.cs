using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskyRevamp.Dto.SystemConfiguration
{
    public class AddTaskSettingDto
    {
        public Guid Id { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string Name
        {
            get
            {
                var name = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar"
                    ? NameArabic
                    : NameEnglish;

                return  name;
            }
        }
        public bool IsActive { get; set; }
        public bool IsMandatory { get; set; }
        public int? Order { get; set; }
    }
}
