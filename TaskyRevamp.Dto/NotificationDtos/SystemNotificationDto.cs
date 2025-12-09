using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dtos.NotificationDtos
{
    public class SystemNotificationDto : NotificationDto
    {
        public string URL { get; set; }
        public bool IsRead { get; set; }
        //  public string? Icon { get; set; }
    }
}
