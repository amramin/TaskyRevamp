using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskSourceDto;

namespace TaskyRevamp.Client.Consumer
{
    public class TaskSourceConsumer
    {
        private readonly TaskyService _taskyService;

        public TaskSourceConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }


        public async Task<CommonApiResponse<List<TaskSourceDto>>> GetTaskSourcesForDDL()
        {


            var url = $"api/TaskSource/GetTaskSourcesForDDL";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<TaskSourceDto>>>(url);

            return res;
        }







        public async Task<CommonApiResponse<TaskSourceDto>> GetTaskSourceById(Guid id)
        {
            var url = $"api/TaskSource/GetTaskSourceById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<TaskSourceDto>>(url);
            return res;
        }

        public async Task<CommonApiResponse<bool>> AddTaskSource(TaskSourceDto TaskSourceDto)
        {
            var url = $"api/TaskSource/CreateTaskSource";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, TaskSourceDto);

            return res.Data;
        }

        public async Task<CommonApiResponse<bool>> UpdateTaskSource(TaskSourceDto TaskSourceDto)
        {
            var url = $"api/TaskSource/UpdateTaskSource";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, TaskSourceDto);

            return res.Data;
        }


        public async Task<CommonApiResponse<bool>> DeleteTaskSource(Guid id)
        {
            var url = $"api/TaskSource/{id}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
    }
}
