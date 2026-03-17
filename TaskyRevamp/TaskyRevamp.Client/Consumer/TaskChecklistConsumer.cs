using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Client.Consumer
{
	public class TaskChecklistConsumer
	{
		private readonly TaskyService _taskyService;
		public TaskChecklistConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<List<TaskChecklistDto>>> GetTaskChecklists(Guid Id)
		{
			var url = $"api/TaskChecklist/GetAllTaskChecklists/{Id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<TaskChecklistDto>>>(url);
			return res;
		}
		public async Task<CommonApiResponse<Guid>> CreateTaskChecklist(TaskChecklistDto taskChecklistDto)
		{
			var url = $"api/TaskChecklist/CreateTaskChecklist";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<Guid>>(url, taskChecklistDto);
			return res.Data!;
		}
		public async Task<CommonApiResponse<Guid>> UpdateTaskChecklist(TaskChecklistDto taskChecklistDto)
		{
			var url = $"api/TaskChecklist/UpdateTaskChecklist";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<Guid>>(url, taskChecklistDto);
			return res.Data!;
		}
		public async Task<CommonApiResponse<bool>> DeleteCheklist(Guid taskId)
		{
			var url = $"api/TaskChecklist/DeleteCheklist/{taskId}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);
			return res;
		}
	}
}
