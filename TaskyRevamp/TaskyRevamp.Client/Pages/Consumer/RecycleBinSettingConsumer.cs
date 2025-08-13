using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Pages.Consumer
{
    public class RecycleBinSettingConsumer
    {
        private readonly TaskyService _taskyService;

        public RecycleBinSettingConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

        public async Task<CommonApiResponse<RecycleBinSettingDto>> GetRecycleBinSetting()
        {
            var url = $"api/RecycleBinSetting/GetRecycleBinSetting";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<RecycleBinSettingDto>>(url);

            return res;
        }
    }
}
