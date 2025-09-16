using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class WorkingDaysSettingConsumer
	{
		private readonly TaskyService _taskyService;
		public WorkingDaysSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<List<WorkingDaysSettingsDto>>> GetWorkingDaysSetting()
		{
			var url = $"api/WorkingDaySettings/GetWorkingDaysSettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<WorkingDaysSettingsDto>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateTaskViewSetting(List<WorkingDaysSettingsDto> days)
		{
			var url = $"api/WorkingDaySettings/UpdateWorkingDaysSetting";
			var res = await _taskyService.PostJsonAsync<bool>(url, days);

			return res;
		}

	}
}
