using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Client.Consumer
{
    public class TaskConsumer
    {
        private readonly TaskyService _taskyService;

        public TaskConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }


        public async Task<CommonApiResponse<List<CreateTaskDto>>> GetTasksForDDL()
        {


            var url = $"api/Task/GetTasksForDDL";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<CreateTaskDto>>>(url);

            return res;
        }

        public async Task<CommonApiResponse<PagedResult<CreateTaskDto>>> GetTasks(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldTask> searchFields = null, string searchText = null)
        {

            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);

            var url = $"api/Task/GetAllTask{queryString}";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<CreateTaskDto>>>(url);

            return res;
        }



        public async Task<CommonApiResponse<List<CreateTaskDto>>> GetTasksNoPagnation(List<SearchFieldTask> searchFields = null, string searchText = null)
        {

            var queryString = _taskyService.PrepareNoPaginatedSearchQueryString(searchFields, searchText);

            var url = $"api/Task/GetTasksNoPagnation{queryString}";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<CreateTaskDto>>>(url);

            return res;
        }

        public async Task<CommonApiResponse<CreateTaskDto>> GetTaskById(Guid id)
        {
            var url = $"api/Task/GetTaskById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<CreateTaskDto>>(url);
            return res;
        }

        public async Task<CommonApiResponse<bool>> AddTask(CreateTaskDto CreateTaskDto)
        {
            var url = $"api/Task/CreateTask";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, CreateTaskDto);

            return res.Data;
        }

        public async Task<CommonApiResponse<bool>> UpdateTask(CreateTaskDto CreateTaskDto)
        {
            var url = $"api/Task/UpdateTask";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, CreateTaskDto);

            return res.Data;
        }
        public async Task<CommonApiResponse<bool>> UpdateTasksDepartment(Guid oldId, Guid newId)
        {
            var url = $"api/Task/UpdateTasksDepartment/{oldId}/{newId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
        public async Task<CommonApiResponse<bool>> CheckOpenedTaskForUser(Guid userId)
        {
            var url = $"api/Task/CheckOpenedTaskForUser/{userId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }


        public async Task<CommonApiResponse<bool>> DeleteTask(Guid id)
        {
            var url = $"api/Task/{id}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
    }
}
