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
		public async Task<CommonApiResponse<bool>> AddTaskAttachment(List<AttachmentDto> attachmentsDto, Guid taskItemDto)
		{
			var url = $"api/TaskAttachment/AddTaskAttachment/{taskItemDto}";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url,attachmentsDto);
			return res.Data;
		}
		public async Task<CommonApiResponse<List<AttachmentWithNameDto>>> GetTaskAttachments(Guid taskItemDto)
		{
			var url = $"api/TaskAttachment/GetTaskAttachments/{taskItemDto}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<AttachmentWithNameDto>>>(url);
			return res;
		}
		public async Task<CommonApiResponse<byte[]>> GetAttachmentInfo(Guid attachmentId)
		{
			var url = $"api/TaskAttachment/GetAttachmentInfo/{attachmentId}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<byte[]>>(url);
			return res;
		}
		public async Task<CommonApiResponse<bool>> DeleteAttachment(Guid attachmentId)
		{
			var url = $"api/TaskAttachment/DeleteAttachment/{attachmentId}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);
			return res;
		}

	}
}
