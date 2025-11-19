using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dto.SystemConfiguration;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query
{
    public record GetNotificationTypeTemplateByIdQuery(Guid id) : IRequest<NotificationTypeTemplateDto>;
    public class GetNotificationTypeTemplateByIdHandler : IRequestHandler<GetNotificationTypeTemplateByIdQuery, NotificationTypeTemplateDto>
    {
        private readonly IRepository<NotificationTypeTemplate> _typeRepository;
        public GetNotificationTypeTemplateByIdHandler(IRepository<NotificationTypeTemplate> typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public async Task<NotificationTypeTemplateDto> Handle(GetNotificationTypeTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            NotificationTypeTemplateDto typeDto = new NotificationTypeTemplateDto();
            var res = await _typeRepository.FindByKey(request.id);
            if (res.Success && res.Value != null && res != null)
            {
                typeDto = res.Value.CopyToDto();
            }
            return typeDto;
        }
    }
}
