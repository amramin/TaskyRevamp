using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Domain.Models.Permissions
{
    public class ReportModule : Entity
    {
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string HintEnglish { get; set; }
        public string HintArabic { get; set; }

        public List<ReportModulePermission> ReportModulePermissions { get; set; }

        public ReportModuleDto CopyToDto()
        {
            return new ReportModuleDto
            {
                Id = Id,
                NameEnglish = NameEnglish,
                NameArabic = NameArabic,
                HintEnglish = HintEnglish,
                HintArabic = HintArabic,
            };
        }
    }
}
