using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Notification;


namespace TaskyRevamp.Services.NotificationService.NotificationTemplate.Queries;


public record GetNotificationTemplateQuery(Guid Id) : IRequest<NotificationTypeTemplateDto>;


public class GetmailTemplateHandler : IRequestHandler<GetNotificationTemplateQuery, NotificationTypeTemplateDto>
{
    private readonly IRepository<NotificationTypeTemplate> _Notificationtemplate;

    public GetmailTemplateHandler(IRepository<NotificationTypeTemplate> Notificationtemplate)
    {
        _Notificationtemplate = Notificationtemplate;
    }

    public async Task<NotificationTypeTemplateDto> Handle(GetNotificationTemplateQuery request, CancellationToken cancellationToken)
    {

        var data = await _Notificationtemplate.FindBy(x => x.Id == request.Id);

        var dat = data.Value.FirstOrDefault();
        NotificationTypeTemplateDto mailTemplate = new NotificationTypeTemplateDto()
        {
            Id = dat.Id,
            NameEnglish = dat.NameEnglish,
            NameArabic = dat.NameArabic,
            //MessageArabic = dat.MessageArabic,
            //MessageEnglish = dat.MessageEnglish,
            //CreateDate = dat.CreateDate,
            //CreatedById = dat.CreatedById,
            //IsHtml = dat.IsHtml,
        };


        return (mailTemplate);
    }







}