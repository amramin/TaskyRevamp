using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class ViewTaskSettingConsumer
	{
		private readonly TaskyService _taskyService;
		public ViewTaskSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<List<ViewTaskSettingsDto>>> GetViewTaskSetting()
		{
			var url = $"api/ViewTaskSetting/GetViewTaskSettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<ViewTaskSettingsDto>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<List<ViewTaskSettingsDto>>> GetActiveViewTaskSetting()
		{
			var url = $"api/ViewTaskSetting/GetActiveViewTaskSettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<ViewTaskSettingsDto>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateTaskViewSetting(List<ViewTaskSettingsDto> TaskViews)
		{
			var url = $"api/ViewTaskSetting/UpdateTaskViewsActivation";
			var res = await _taskyService.PostJsonAsync<bool>(url, TaskViews);

			return res;
		}
	}
}
