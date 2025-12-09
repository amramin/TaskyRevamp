using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dtos.NotificationDtos
{
    public class EmailDto : NotificationDto
    {
        public string EmailAddress { get; set; }
    }
}
