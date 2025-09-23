using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Department
{
    public  class DepartmentDto
    {
        public Guid Id { get; set; }
        [Required(
     ErrorMessageResourceType = typeof(SharedResources),
     ErrorMessageResourceName = ValidationDto.Required
 )]
        public string NameEnglish { get; set; }
        [Required(
     ErrorMessageResourceType = typeof(SharedResources),
     ErrorMessageResourceName = ValidationDto.Required
 )]
        public string NameArabic { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
