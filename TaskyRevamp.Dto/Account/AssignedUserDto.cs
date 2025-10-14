using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Account
{
    public class AssignedUserDto
    {
        public Guid DepartmentId { get; set; }
       public List<Guid> UsrIds { get; set; }

    }
}
