using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class StatusSettingConsumer
	{
		private readonly TaskyService _taskyService;

		public StatusSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<List<StatusSettingsDto>>> GetStatusConfiguration(bool isload=true)
		{
			var url = $"api/StatusSetting/GetStatusSettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<StatusSettingsDto>>>(url,isload);

			return res;
		}

		public async Task<CommonApiResponse<StatusSettingsDto>> GetStatusById(Guid id)
		{
			var url = $"api/StatusSetting/GetStatusById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<StatusSettingsDto>>(url);
			return res;
		}
		public async Task<CommonApiResponse<bool>> UpdateStatusConfiguration(StatusSettingsDto settingsDto)
		{
			var url = $"api/StatusSetting/UpdateStatusSettings";
			var res = await _taskyService.PostJsonAsync<bool>(url, settingsDto);

			return res;
		}
	}
}
