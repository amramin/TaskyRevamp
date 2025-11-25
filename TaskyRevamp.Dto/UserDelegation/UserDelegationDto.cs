using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.UserDelegation
{
    public class UserDelegationDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]

        public Guid FromUserId { get; set; }

        public string? FromUserEn { get; set; }
        public string? FromUserAr { get; set; }
        public string? FromUser => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? FromUserAr : FromUserEn;
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]

        public Guid ToUserId { get; set; }
        public string? ToUserEn { get; set; }
        public string? ToUserAr { get; set; }
        public string? ToUser => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? ToUserAr : ToUserEn;
        public DateTime FromDate { get; set; } = DateTime.UtcNow;

        public DelegationOptions DelegationOption { get; set; }
        public DateTime ToDate { get; set; } = DateTime.UtcNow;

        public DateTime? CreateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }

    }
}
