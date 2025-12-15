using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Client.Consumer
{
	public class TaskCommentConsumer
	{
		private readonly TaskyService _taskyService;

		public TaskCommentConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<List<TaskCommentWithNameDto>>> GetTaskCommentsById(Guid Id)
		{
			var url = $"api/TaskComment/GetAllTaskComments/{Id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<TaskCommentWithNameDto>>>(url);
			return res;
		}
		public async Task<CommonApiResponse<bool>> AddTaskComment(TaskCommentDto taskCommentDto)
		{
			var url = $"api/TaskComment/CreateTaskComment";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, taskCommentDto);
			return res.Data;
		}

		public async Task<CommonApiResponse<bool>> UpdateTask(TaskCommentDto taskCommentDto)
		{
			var url = $"api/Task/UpdateTask";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, taskCommentDto);

			return res.Data;
		}
		public async Task<CommonApiResponse<bool>> DeleteTask(Guid id)
		{
			var url = $"api/Task/{id}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

			return res;
		}
	}
}
