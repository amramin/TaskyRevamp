using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Permissions.ReportModule;

namespace TaskyRevamp.Domain.Models.Permissions.ReportModule
{
    public class ReportModulePermission : Entity
    {
        public Guid PrivilegeId { get; set; }
        public Privilege Privilege { get; set; }
        public Guid ReportModuleId { get; set; }
        public ReportModule ReportModule { get; set; }
        public bool IsActive { get; set; }

        public ReportModulePermissionDto CopyToDto()
        {
            return new ReportModulePermissionDto
            {
                Id = Id,
                PrivilegeId = PrivilegeId,
                ReportModuleId = ReportModuleId,
                IsActive = IsActive
            };
        }
    }
}
