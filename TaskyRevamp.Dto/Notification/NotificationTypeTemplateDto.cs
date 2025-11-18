using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.Notification
{
    public class NotificationTypeTemplateDto
    {
        public Guid Id { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string? TemplateEnglish { get; set; }
        public string? TemplateArabic { get; set; }
        public bool IsEnable { get; set; }
        public ModuleType moduleType { get; set; }
        public string? SubjectEnglish { get; set; }
        public string? SubjectArabic { get; set; }

    }
}
