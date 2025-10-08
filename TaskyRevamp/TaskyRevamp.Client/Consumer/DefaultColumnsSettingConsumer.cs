using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
    public class DefaultColumnsSettingConsumer
    {
        private readonly TaskyService _taskyService;

        public DefaultColumnsSettingConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

        public async Task<CommonApiResponse<List<DefaultColumnsSettingDto>>> GetDefaultColumnsSettings()
        {
            var url = $"api/DefaultColumnsSetting/GetDefaultColumnsSettings";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<DefaultColumnsSettingDto>>>(url);

            return res;
        }
        public async Task<CommonApiResponse<bool>> UpdateDefaultColumnsSettings(List<DefaultColumnsSettingDto> defaultColumnsSettingsDto)
        {
            var url = $"api/DefaultColumnsSetting/UpdateDefaultColumnsSettings";
            var ret = await _taskyService.PostJsonAsync<bool>(url, defaultColumnsSettingsDto);

            return ret;
        }
    }
}
