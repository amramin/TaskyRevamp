using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class WeeklyReportSettingConsumer
	{
		private readonly TaskyService _taskyService;
		public WeeklyReportSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<WeeklyReportSettingsDto>> GetWeeklyReportSetting()
		{
			var url = $"api/WeeklyReportSetting/GetWeeklyReportSettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<WeeklyReportSettingsDto>>(url);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateWeeklyReportSetting(WeeklyReportSettingsDto settingsDto)
		{
			var url = $"api/WeeklyReportSetting/UpdateWeeklyReportSetting";
			var res = await _taskyService.PostJsonAsync<bool>(url, settingsDto);

			return res;
		}
	}
}
