using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Notification;

namespace TaskyRevamp.Services.Notification.Command
{


    public record UpdateNotificationTypeTemplatesCommand(List<NotificationTypeTemplateDto> NotificationTypeTemplate) : IRequest<bool>;

    public class UpdateNotificationTypeTemplatesCommandHandler : IRequestHandler<UpdateNotificationTypeTemplatesCommand, bool>
    {
        private readonly IRepository<NotificationTypeTemplate> _NotificationTypeTemplateRepository;

        public UpdateNotificationTypeTemplatesCommandHandler(IRepository<NotificationTypeTemplate> NotificationTypeTemplateRepository)
        {
            _NotificationTypeTemplateRepository = NotificationTypeTemplateRepository;
        }

        public async Task<bool> Handle(UpdateNotificationTypeTemplatesCommand request, CancellationToken cancellationToken)
        {
            var NotificationTypeTemplateResponse = await _NotificationTypeTemplateRepository.FindBy(k => request.NotificationTypeTemplate.Select(i => i.Id).ToList().Contains(k.Id));
            if (!NotificationTypeTemplateResponse.Success)
            {
                return false;
            }


            if (NotificationTypeTemplateResponse.Value is null)
            {
                throw new Exception("NotificationTypeTemplate not found");
            }


            var updated = NotificationTypeTemplateResponse.Value;
            foreach (var item in updated.ToList())
            {
                item.SetData(request.NotificationTypeTemplate.Where(l => l.Id == item.Id).FirstOrDefault());

                await _NotificationTypeTemplateRepository.Update(item);
                await _NotificationTypeTemplateRepository.SaveChangesAsync();
            }
            return true;
        }
    }
}
