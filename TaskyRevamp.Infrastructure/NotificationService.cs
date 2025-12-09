using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Notification;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Email;
using TaskyRevamp.Dtos.NotificationDtos;
using TaskyRevamp.Infrastructure.Hubs;
using TaskyRevamp.Services.Email.Command;
using TaskyRevamp.Services.NotificationService.NotificationTemplate.Queries;
using TaskyRevamp.Services.NotificationService.SystemNotificationService.Command;


namespace TaskyRevamp.Infrastructure.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IMediator _mediator;
        private IHubContext<NotificationHub> _hubContext;

        public NotificationService(IMediator mediator, IHubContext<NotificationHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        public async Task SendNotification(SendNotificationDto sendNotificationDto)
        {
            if (sendNotificationDto.ReceiverType <= 0 || sendNotificationDto.ReceiverId == null)
                return;
            await BuildNotificationFromTemplate(sendNotificationDto);
            var notification = sendNotificationDto.Notification;
            List<UserDto> receiversUsers = new List<UserDto>();

            UserDto userDto = new UserDto()
            {
                Id = Guid.Parse(sendNotificationDto.ReceiverId),
                Email = sendNotificationDto.Email
            };
            receiversUsers.Add(userDto);


            foreach (var receiver in receiversUsers)
            {

                if (sendNotificationDto.IsSendSystemNotification)
                {
                    var systemNotitifcation = new SystemNotificationDto()
                    {
                        TitleEnglish = notification.TitleEnglish,
                        TitleArabic = notification.TitleArabic,
                        CreateDate = notification.CreateDate,
                        CreatedBy = notification.CreatedBy,
                        MessageArabic = notification.MessageArabic,
                        MessageEnglish = notification.MessageEnglish,
                        UserId = receiver.Id,
                        URL = string.IsNullOrEmpty(sendNotificationDto.URL) ? "" : sendNotificationDto.URL
                  ,
                        Icon = sendNotificationDto.Icon
                    };

                    await CreateSystemNotification(systemNotitifcation);

                    await PushSystemNotification(receiver.Id, systemNotitifcation);

                    if (sendNotificationDto.IsRefreshListRequired)
                        await PushRefreshListNotification(receiver.Id);
                }
                if (sendNotificationDto.IsSendSms)
                {
                    //Send sms
                }
                if (sendNotificationDto.IsSendEmail)
                {
                    if (!string.IsNullOrEmpty(sendNotificationDto.Email))
                    {
                        //TODO GetUserMailData
                        SendEmailDto sending = new SendEmailDto()
                        {
                            EmailBody = notification.MessageEnglish,
                            EmailSubject = notification.TitleEnglish,
                            EmailToId = receiver.Email,
                            EmailToName = receiver.DisplayedName
                        };
                        await _mediator.Send(new SendMailCommand(sending));
                    }

                }
            }

        }

        public async Task BuildNotificationFromTemplate(SendNotificationDto sendNotification)
        {
            var notification = sendNotification.Notification;
            if (!sendNotification.NotificationTemplateId.HasValue)
                return;

            var result = await _mediator.Send(new GetNotificationTemplateQuery(sendNotification.NotificationTemplateId.Value));


            //  notification.MessageEnglish = PopulateNotificationMessage(template.MessageEnglish, sendNotification.NotificationParameters);
            //notification.MessageArabic = PopulateNotificationMessage(template.MessageArabic, sendNotification.NotificationParametersAr);
            notification.TitleEnglish = string.IsNullOrEmpty(notification.TitleEnglish) ? "New Task" : notification.TitleEnglish;
            notification.TitleArabic = string.IsNullOrEmpty(notification.TitleArabic) ? "مهمه جديده" : notification.TitleArabic;
            notification.Icon = string.IsNullOrEmpty(notification.Icon) ? "@Icons.Material.Filled.Task" : notification.Icon;

        }
        //  }
        public string PopulateNotificationMessage(string message, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(message))
                return "";

            if (parameters == null || parameters.Count == 0)
                return message;

            foreach (var param in parameters)
                message = message.Replace("@" + param.Key, param.Value);

            return message;
        }

        public async Task CreateSystemNotification(SystemNotificationDto notificationDto)
        {
            await _mediator.Send(new CreateSystemNotificationCommand(notificationDto));
        }

        public async Task PushSystemNotification(Guid receiverId, NotificationDto notification)
        {
            await _hubContext.Clients.User(receiverId.ToString().ToLower()).SendAsync("ReceiveNotification", notification);
        }

        public async Task PushRefreshListNotification(Guid receiverId)
        {
            await _hubContext.Clients.User(receiverId.ToString().ToLower()).SendAsync("RefreshList");
        }

    }
}
