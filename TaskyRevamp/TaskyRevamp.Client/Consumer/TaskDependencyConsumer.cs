using DocumentFormat.OpenXml.Wordprocessing;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Client.Consumer
{
	public class TaskDependencyConsumer
	{
		private readonly TaskyService _taskyService;
		public TaskDependencyConsumer(TaskyService taskyService, FileManagementService fileManagementService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<PagedResult<CreateTaskDto>>> GetDependOnTasks(Guid taskId, int pageNumber, int pageSize, string sortByColumnName, bool? sortAscending)
		{
			var url = $"api/TaskDependency/GetDependOnTasks/{taskId}?pageNumber={pageNumber}&pageSize={pageSize}&sortByColumnName={sortByColumnName}&sortAscending={sortAscending}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<CreateTaskDto>>>(url);
			return res;
		}
		public async Task<CommonApiResponse<PagedResult<CreateTaskDto>>> GetDependentTasks(Guid taskId, int pageNumber, int pageSize, string sortByColumnName, bool? sortAscending)
		{
			var url = $"api/TaskDependency/GetDependentTasks/{taskId}?pageNumber={pageNumber}&pageSize={pageSize}&sortByColumnName={sortByColumnName}&sortAscending={sortAscending}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<CreateTaskDto>>>(url);
			return res;
		}
	}
}
