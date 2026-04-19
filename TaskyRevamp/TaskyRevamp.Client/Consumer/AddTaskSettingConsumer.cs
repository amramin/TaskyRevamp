using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
    public class AddTaskSettingConsumer
    {
        private readonly TaskyService _taskyService;

        public AddTaskSettingConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

        public async Task<CommonApiResponse<List<AddTaskSettingDto>>> GetAddTaskSettings(bool isload = true)
        {
            var url = $"api/AddTaskSetting/GetAddTaskSettings";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<AddTaskSettingDto>>>(url, isload);

            return res;
        }
        public async Task<CommonApiResponse<bool>> UpdateAddTaskSettings(List<AddTaskSettingDto> addTaskSettingsDto)
        {
            var url = $"api/AddTaskSetting/UpdateAddTaskSettings";
            var ret = await _taskyService.PostJsonAsync<bool>(url, addTaskSettingsDto);

            return ret;
        }
    }
}
