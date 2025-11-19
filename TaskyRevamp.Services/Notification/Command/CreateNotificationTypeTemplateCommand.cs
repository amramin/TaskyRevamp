using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
namespace NotificationTypeTemplateyRevamp.Services.NotificationTypeTemplates.Commands;

public record CreateNotificationTypeTemplateCommand(NotificationTypeTemplateDto NotificationTypeTemplateDto) : IRequest<Guid>;

public class CreateNotificationTypeTemplateHandler : IRequestHandler<CreateNotificationTypeTemplateCommand, Guid>
{
    private readonly IRepository<NotificationTypeTemplate> _NotificationTypeTemplateRepository;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public CreateNotificationTypeTemplateHandler(IRepository<NotificationTypeTemplate> NotificationTypeTemplateRepository) { _NotificationTypeTemplateRepository = NotificationTypeTemplateRepository; }

    public async Task<Guid> Handle(CreateNotificationTypeTemplateCommand request, CancellationToken cancellationToken)
    {

        NotificationTypeTemplate NotificationTypeTemplate = new NotificationTypeTemplate();
        NotificationTypeTemplate.SetData(request.NotificationTypeTemplateDto);


        await _NotificationTypeTemplateRepository.Update(NotificationTypeTemplate);
        await _NotificationTypeTemplateRepository.SaveChangesAsync();
        return NotificationTypeTemplate.Id;
    }


}
