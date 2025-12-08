using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dtos.NotificationDtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string TitleEnglish { get; set; }
        public string TitleArabic { get; set; }
        public string? Icon { get; set; }
        public string DisplayTitle => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? TitleArabic : TitleEnglish;
        public string MessageEnglish { get; set; }
        public string MessageArabic { get; set; }
        public string DisplayMessage => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? MessageArabic : MessageEnglish;
        public DateTime CreateDate { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
