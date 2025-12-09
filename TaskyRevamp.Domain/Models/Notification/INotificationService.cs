using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dtos.NotificationDtos;

namespace TaskyRevamp.Domain.Interfaces.Notification
{
    public interface INotificationService
    {
        public Task SendNotification(SendNotificationDto sendNotificationDto);
        public Task PushSystemNotification(Guid receiverId, NotificationDto notification);
        public Task PushRefreshListNotification(Guid receiverId);
    }
}
