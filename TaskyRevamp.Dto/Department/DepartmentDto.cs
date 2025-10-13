using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Department
{
    public  class DepartmentDto
    {
        public Guid Id { get; set; }
        [Required]
        public string NameEnglish { get; set; }
        [Required]
        public string NameArabic { get; set; }

        public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ar") ? NameArabic : NameEnglish;

        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }
        public Guid ParentdepartmentId { get; set; }
        public string? ParentdepartmentArabic { get; set; }
        public string? ParentdepartmentEnglish { get; set; }
        public string? ParentdepartmentName => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ar") ? ParentdepartmentArabic : ParentdepartmentEnglish;
        public DateTime? CreateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
       public List<UserDto>? AssignedUsers { get; set; } 

    }
}
