using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class RejectionSettingConsumer
	{
		private readonly TaskyService _taskyService;

		public RejectionSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<RejectionSettingsDto>> GetRejectionSetting()
		{
			var url = $"api/RejectionSetting/GetRejectionSetting";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<RejectionSettingsDto>>(url);

			return res;
		}
		public async Task<CommonApiResponse<bool>> UpdateRejectionSetting(RejectionSettingsDto _rejectionSettingDto)
		{
			var url = $"api/RejectionSetting/UpdateRejectionSetting";
			var ret = await _taskyService.PostJsonAsync<bool>(url, _rejectionSettingDto);

			return ret;
		}
	}
}
