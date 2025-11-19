using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Notification;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Departments.Query;

namespace TaskyRevamp.Services.Notification.Query
{
    public record GetNotificationTypeTemplatesQuery() : IRequest<List<NotificationTypeTemplateDto>>;

    public class GetNotificationTypeTemplatesHandler : IRequestHandler<GetNotificationTypeTemplatesQuery, List<NotificationTypeTemplateDto>>
    {
        private readonly IRepository<NotificationTypeTemplate> _NotificationTypeTemplateRepository;
        public GetNotificationTypeTemplatesHandler(IRepository<NotificationTypeTemplate> NotificationTypeTemplateRepository)
        {
            _NotificationTypeTemplateRepository = NotificationTypeTemplateRepository;
        }
        public async Task<List<NotificationTypeTemplateDto>> Handle(GetNotificationTypeTemplatesQuery request, CancellationToken cancellationToken)
        {
            var NotificationTypeTemplateDto = new List<NotificationTypeTemplateDto>();
            var priortiyQuieriesResponse = await _NotificationTypeTemplateRepository.AllAsNoTracking();
            if (priortiyQuieriesResponse.Success && priortiyQuieriesResponse.Value != null && priortiyQuieriesResponse.Value.Any())
            {
                NotificationTypeTemplateDto = priortiyQuieriesResponse.Value.Select(q => q.CopyToDto()).ToList();
            }
            return NotificationTypeTemplateDto;
        }




    }
}
