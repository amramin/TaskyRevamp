using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Dtos.NotificationDtos
{
    public class SendNotificationDto
    {
        public NotificationDto Notification { get; set; }
        public Dictionary<string, string> NotificationParameters { get; set; }
        public Dictionary<string, string> NotificationParametersAr { get; set; }
        public Guid? NotificationTemplateId { get; set; }
        public string URL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsSendSystemNotification { get; set; }
        public bool IsSendSms { get; set; }
        public bool IsSendEmail { get; set; }
        public int ReceiverType { get; set; }
        public string ReceiverId { get; set; }
        public bool IsRefreshListRequired { get; set; }
        public UserDto? user { get; set; }
        public string? Icon { get; set; }
    }
}
