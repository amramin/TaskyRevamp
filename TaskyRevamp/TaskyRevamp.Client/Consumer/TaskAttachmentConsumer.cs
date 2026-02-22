using Microsoft.AspNetCore.Http;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskAttachment;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Client.Consumer
{
	public class TaskAttachmentConsumer
	{
		private readonly TaskyService _taskyService;

		public TaskAttachmentConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<bool>> AddTaskAttachment(MultipartFormDataContent content, Guid taskItemDto)
		{
			var url = $"api/TaskAttachment/AddTaskAttachment/{taskItemDto}";
			var res = await _taskyService.PostFileAsync<CommonApiResponse<bool>>(url,content);
			return res.Data!;
		}
		public async Task<CommonApiResponse<List<AttachmentWithNameDto>>> GetTaskAttachments(Guid taskItemDto)
		{
			var url = $"api/TaskAttachment/GetTaskAttachments/{taskItemDto}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<AttachmentWithNameDto>>>(url);
			return res;
		}
		public async Task<CommonApiResponse<byte[]>> GetAttachmentInfo(Guid fileId)
		{
			var url = $"api/TaskAttachment/GetAttachmentInfo/{fileId}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<byte[]>>(url);
			return res;
		}
		public async Task<CommonApiResponse<bool>> DeleteAttachment(Guid attachmentId, Guid fileId)
		{
			var url = $"api/TaskAttachment/DeleteAttachment/{attachmentId}/{fileId}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);
			return res;
		}

	}
}
