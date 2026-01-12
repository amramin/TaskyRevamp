using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;


namespace TaskyRevamp.Dto.SystemConfiguration
{
    public class PriorityDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public string NameEnglish { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public string NameArabic { get; set; }


        public string Name
        {
            get
            {
                var name = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar"
                    ? NameArabic
                    : NameEnglish;

                return name;
            }
        }

        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public string NameColor { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public string BackgroundColor { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; } = false;

        public static PriorityDto CopyFrom(PriorityDto source)
        {
            return new PriorityDto
            {
                Id = source.Id,
                NameEnglish = source.NameEnglish,
                NameArabic = source.NameArabic,
                NameColor = source.NameColor,
                BackgroundColor = source.BackgroundColor,
                Order = source.Order
            };
        }
    }
}
