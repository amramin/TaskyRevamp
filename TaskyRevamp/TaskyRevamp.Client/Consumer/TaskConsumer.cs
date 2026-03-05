using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components.Forms;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Dto.TaskDto;


namespace TaskyRevamp.Client.Consumer
{
    public class TaskConsumer
    {
        private readonly TaskyService _taskyService;
        private readonly FileManagementService _fileManagementService;
		public TaskConsumer(TaskyService taskyService, FileManagementService fileManagementService)
		{
			_taskyService = taskyService;
			_fileManagementService = fileManagementService;
		}
		public async Task<CommonApiResponse<List<CreateTaskDto>>> GetTasksForDDL(Guid tskid)
        {
            var url = $"api/Task/GetTasksForDDL/{tskid}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<CreateTaskDto>>>(url);
            return res;
        }
        public async Task<CommonApiResponse<PagedResult<CreateTaskDto>>> GetTasks(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldTask> searchFields = null, string searchText = null,int ViewType=1,Guid? ViewTypeId=null, bool IsCompleted = false,TaskFilterComponent taskFilter=null)
        {
            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText,ViewType,ViewTypeId,IsCompleted);
            var url = $"api/Task/GetAllTask{queryString}";
            var res = await _taskyService.PostJsonAsyncWithJsonConvert<PagedResult<CreateTaskDto>,TaskFilterComponent>(url,taskFilter);
            return res;
        }
        public async Task<CommonApiResponse<PagedResult<CreateTaskDto>>> GetDeletedTasks(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDeletedTask> searchFields = null, string searchText = null)
        {
            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);
            var url = $"api/Task/GetDeletedTasks{queryString}";
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
        public async Task<CommonApiResponse<CreateTaskDto>> GetTaskById(Guid id, Guid currentUserId)
        {
            var url = $"api/Task/GetTaskById/{id}/{currentUserId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<CreateTaskDto>>(url);
            return res;
        }
        public async Task<CommonApiResponse<CreateTaskDto>> GetTaskById(Guid id)
        {
            var url = $"api/Task/GetTaskById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<CreateTaskDto>>(url);
            return res;
        }
        public async Task<CommonApiResponse<List<CreateTaskDto>>> GetMainAndParentTasks()
        {
            var url = $"api/Task/GetMainAndParentTasks";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<CreateTaskDto>>>(url);
            return res;
        }

        public async Task<CommonApiResponse<string>> AddTask(CreateTaskDto CreateTaskDto, List<IBrowserFile>? browserFilesAddedTask = null,HashSet<string>? allowedExtensions = null)
        {
            var url = $"api/Task/CreateTask";
            var res = await _taskyService.PostJsonAsync<string>(url, CreateTaskDto);
			if (!res.Success) return res;
            if (browserFilesAddedTask != null && browserFilesAddedTask.Any())
            {
                var taskId = Guid.Parse(res.Data!);
				await _fileManagementService.UploadStreamFiles(taskId, browserFilesAddedTask, allowedExtensions);
            }
			return res;
        }

        public async Task<CommonApiResponse<bool>> UpdateTask(CreateTaskDto CreateTaskDto, List<IBrowserFile>? browserFiles = null, HashSet<string>? allowedExtensions = null)
        {
            var url = $"api/Task/UpdateTask";
            var res = await _taskyService.PostJsonAsync<bool>(url, CreateTaskDto);
            if(!res.Success) return res;
            if(browserFiles != null && browserFiles.Any())
				await _fileManagementService.UploadStreamFiles(CreateTaskDto.Id ,browserFiles, allowedExtensions);
			return res;
        }
        public async Task<CommonApiResponse<bool>> CompleteTask(Guid TaskId)
        {
            var url = $"api/Task/CompleteTask/{TaskId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
        public async Task<CommonApiResponse<bool>> ReopenTask(TaskCommentDto taskCommentDto)
        {
            var url = $"api/Task/ReopenTask";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, taskCommentDto);
            return res.Data;
        }
        public async Task<CommonApiResponse<bool>> RejectTask(TaskCommentDto taskCommentDto)
        {
            var url = $"api/Task/RejectTask";
            var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, taskCommentDto);
            return res.Data;
        }
        public async Task<CommonApiResponse<bool>> UpdateTasksDepartment(Guid oldId, Guid newId)
        {
            var url = $"api/Task/UpdateTasksDepartment/{oldId}/{newId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
        public async Task<CommonApiResponse<bool>> ChangeTaskProgress(Guid TaskId, int Progress)
        {
            var url = $"api/Task/ChangeTaskProgress/{TaskId}/{Progress}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
        public async Task<CommonApiResponse<bool>> UpdateTaskPriority(Guid TaskId, Guid PriorityId)
        {
            var url = $"api/Task/UpdateTaskPriority/{TaskId}/{PriorityId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
        public async Task<CommonApiResponse<bool>> CheckOpenedTaskForUser(Guid userId)
        {
            var url = $"api/Task/CheckOpenedTaskForUser/{userId}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
        public async Task<CommonApiResponse<bool>> SoftDeleteTask(Guid id, Guid userId)
        {
            var url = $"api/Task/SoftDeleteTask/{id}/{userId}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
    }
}
