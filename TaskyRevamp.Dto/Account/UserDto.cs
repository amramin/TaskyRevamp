using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Account
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string userNameAR { get; set; }
        public string userNameEN { get; set; }
        public string UserName { get; set; }
        public string DisplayedName => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? userNameAR : userNameEN;
        public string Email { get; set; }
        public string? GivenName { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public bool IsManager { get; set; }
        public Guid? PrivilegeId { get; set; }
        public string? PrivilegeName { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }

    }
    public class UserDtoWithName
    {
        public UserDto user { get; set; }
        public string UpdatedByName { get; set; }
        public string PrivilegeName { get; set; }
        public string DepartmentName { get; set; }

    }
}
