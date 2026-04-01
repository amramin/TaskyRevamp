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

		public async Task<CommonApiResponse<RejectionSettingsDto>> GetRejectionSetting(bool isload = true)
		{
			var url = $"api/RejectionSetting/GetRejectionSetting";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<RejectionSettingsDto>>(url,isload);

			return res;
		}
		public async Task<CommonApiResponse<bool>> UpdateRejectionSetting(RejectionSettingsDto rejectionSettingDto)
		{
			var url = $"api/RejectionSetting/UpdateRejectionSetting";
			var ret = await _taskyService.PostJsonAsync<bool>(url, rejectionSettingDto);

			return ret;
		}
	}
}
