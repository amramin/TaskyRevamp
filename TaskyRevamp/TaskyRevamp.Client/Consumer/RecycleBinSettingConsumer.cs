using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
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
		public async Task<CommonApiResponse<bool>> UpdateRecycleBinSetting(RecycleBinSettingDto _recycleBinSettingDto)
		{
			var url = $"api/RecycleBinSetting/UpdateRecycleBinSetting";
			var ret = await _taskyService.PostJsonAsync<bool>(url, _recycleBinSettingDto);

			return ret;
		}
	}
}
