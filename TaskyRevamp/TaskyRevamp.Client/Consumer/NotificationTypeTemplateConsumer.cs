using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
    public class NotificationTypeTemplateConsumer
    {
        private readonly TaskyService _taskyService;

        public NotificationTypeTemplateConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

        public async Task<CommonApiResponse<List<NotificationTypeTemplateDto>>> GetNotificationTypeTemplates()
        {
            var url = $"api/NotificationTypeTemplate/GetNotificationTypeTemplates";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<NotificationTypeTemplateDto>>>(url);

            return res;
        }

        public async Task<CommonApiResponse<NotificationTypeTemplateDto>> GetNotificationTypeTemplateById(Guid id)
        {
            var url = $"api/NotificationTypeTemplate/GetNotificationTypeTemplateById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<NotificationTypeTemplateDto>>(url);
            return res;
        }

        public async Task<CommonApiResponse<bool>> AddNotificationTypeTemplate(NotificationTypeTemplateDto NotificationTypeTemplateDto)
        {
            var url = $"api/NotificationTypeTemplate/AddNotificationTypeTemplate";
            var res = await _taskyService.PostJsonAsync<bool>(url, NotificationTypeTemplateDto);

            return res;
        }

        public async Task<CommonApiResponse<bool>> UpdateNotificationTypeTemplate(NotificationTypeTemplateDto NotificationTypeTemplateDto)
        {
            var url = $"api/NotificationTypeTemplate/UpdateNotificationTypeTemplate";
            var res = await _taskyService.PostJsonAsync<bool>(url, NotificationTypeTemplateDto);

            return res;
        }

        public async Task<CommonApiResponse<bool>> UpdateNotificationTypeTemplates(List<NotificationTypeTemplateDto> NotificationTypeTemplates)
        {
            var url = $"api/NotificationTypeTemplate/UpdateNotificationTypeTemplates";
            var res = await _taskyService.PostJsonAsync<bool>(url, NotificationTypeTemplates);

            return res;
        }

        public async Task<CommonApiResponse<bool>> DeleteNotificationTypeTemplate(Guid id)
        {
            var url = $"api/NotificationTypeTemplate/DeleteNotificationTypeTemplate/{id}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
    }
}
