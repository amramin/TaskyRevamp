using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Permissions
{
    public class ReportModulePermissionDto
    {
        public Guid Id { get; set; }
        public Guid PrivilegeId { get; set; }
        public PrivilegeDto Privilege { get; set; }
        public Guid ReportModuleId { get; set; }
        public ReportModuleDto ReportModule { get; set; }
        public bool IsActive { get; set; }
    }
}
