using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dtos.NotificationDtos;


namespace TaskyRevamp.Services.NotificationService.SystemNotificationService.Command;



public record CreateSystemNotificationCommand(SystemNotificationDto notificationDto) : IRequest<bool>;

public class CreateSystemNotificationHandler : IRequestHandler<CreateSystemNotificationCommand, bool>
{
    private readonly IRepository<NotificationTypeTemplate> _NotificatonRepository;

    public CreateSystemNotificationHandler(IRepository<NotificationTypeTemplate> notificationpository)
    {
        _NotificatonRepository = notificationpository;
    }

    public async Task<bool> Handle(CreateSystemNotificationCommand request, CancellationToken cancellationToken)
    {


        var newnotification = new NotificationTypeTemplate
        {

            SubjectEnglish = request.notificationDto.MessageEnglish,
            SubjectArabic = request.notificationDto.MessageArabic,
            NameEnglish = request.notificationDto.TitleEnglish,
            NameArabic = request.notificationDto.TitleArabic,
            // IsRead = request.notificationDto.IsRead,
            //  = request.notificationDto.CreateDate,
            //CreatedBy = request.notificationDto.CreatedBy,
            //URL = request.notificationDto.URL,
            //UserId = request.notificationDto.UserId,
            //Icon=request.notificationDto.Icon
        };

        await _NotificatonRepository.Insert(newnotification);






        return true;
    }
}