using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Localization.Resources;

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
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Themessagecontentcannotbeempty")]

        public string SubjectEnglish { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Themessagecontentcannotbempty")]

        public string SubjectArabic { get; set; }

    }
}
