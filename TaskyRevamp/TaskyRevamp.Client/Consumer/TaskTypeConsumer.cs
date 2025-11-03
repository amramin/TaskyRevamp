using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskTypeDto;

namespace TaskyRevamp.Client.Consumer
{
    public class TaskTypeConsumer
    {
        private readonly TaskyService _taskyService;

        public TaskTypeConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }


        public async Task<CommonApiResponse<List<TaskTypeDto>>> GetTaskTypesForDDL()
        {


            var url = $"api/TaskType/GetTaskTypesForDDL";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<TaskTypeDto>>>(url);

            return res;
        }







        public async Task<CommonApiResponse<TaskTypeDto>> GetTaskTypeById(Guid id)
        {
            var url = $"api/TaskType/GetTaskTypeById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<TaskTypeDto>>(url);
            return res;
        }

        public async Task<CommonApiResponse<bool>> AddTaskType(TaskTypeDto TaskTypeDto)
        {
            var url = $"api/TaskType/CreateTaskType";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, TaskTypeDto);

            return res.Data;
        }

        public async Task<CommonApiResponse<bool>> UpdateTaskType(TaskTypeDto TaskTypeDto)
        {
            var url = $"api/TaskType/UpdateTaskType";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, TaskTypeDto);

            return res.Data;
        }


        public async Task<CommonApiResponse<bool>> DeleteTaskType(Guid id)
        {
            var url = $"api/TaskType/{id}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
    }
}
