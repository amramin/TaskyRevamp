using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class DefaultViewSettingConsumer
	{
		private readonly TaskyService _taskyService;
		public DefaultViewSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<DefaultViewSettingsDto>> GetDefaultViewSetting()
		{
			var url = $"api/DefaultViewSettings/GetDefaultViewSettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<DefaultViewSettingsDto>>(url);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateDefaultViewSetting(DefaultViewSettingsDto defaultViewDto)
		{
			var url = $"api/DefaultViewSettings/UpdateDefaultViewSetting";
			var res = await _taskyService.PostJsonAsync<bool>(url, defaultViewDto);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateSubTaskLevelDefaultViewSetting(DefaultViewSettingsDto defaultViewDto)
		{
			var url = $"api/DefaultViewSettings/UpdateSubTaskLevelDefaultViewSetting";
			var res = await _taskyService.PostJsonAsync<bool>(url, defaultViewDto);

			return res;
		}
	}
}
