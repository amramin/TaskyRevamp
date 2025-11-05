using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Permissions.ReportModule
{
    public class ReportModuleDto
    {
        public Guid Id { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? NameArabic : NameEnglish;
        public string HintEnglish { get; set; }
        public string HintArabic { get; set; }
        public string Hint => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? HintArabic : HintEnglish;
    }
}
