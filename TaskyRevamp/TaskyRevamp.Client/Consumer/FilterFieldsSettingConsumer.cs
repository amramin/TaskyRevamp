using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
    public class FilterFieldsSettingConsumer
    {
        private readonly TaskyService _taskyService;

        public FilterFieldsSettingConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

        public async Task<CommonApiResponse<List<FilterFieldsSettingDto>>> GetFilterFieldsSettings()
        {
            var url = $"api/FilterFieldsSetting/GetFilterFieldsSettings";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<FilterFieldsSettingDto>>>(url);

            return res;
        }
        public async Task<CommonApiResponse<bool>> UpdateFilterFieldsSettings(List<FilterFieldsSettingDto> filterFieldsSettingsDto)
        {
            var url = $"api/FilterFieldsSetting/UpdateFilterFieldsSettings";
            var ret = await _taskyService.PostJsonAsync<bool>(url, filterFieldsSettingsDto);

            return ret;
        }
    }
}
