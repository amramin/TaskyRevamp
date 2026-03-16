using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskChecklist;

namespace TaskyRevamp.Client.Consumer
{
	public class ChecklistItemConsumer
	{
		private readonly TaskyService _taskyService;
		public ChecklistItemConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<List<ChecklistItemDto>>> GetChecklistItems(Guid Id)
		{
			var url = $"api/ChecklistItem/GetChecklistItems/{Id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<ChecklistItemDto>>>(url);
			return res;
		}
		public async Task<CommonApiResponse<Guid>> CreateChecklistItem(ChecklistItemDto ChecklistitemDto)
		{
			var url = $"api/ChecklistItem/CreateChecklistItem";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<Guid>>(url, ChecklistitemDto);
			return res.Data!;
		}
		public async Task<CommonApiResponse<bool>> UpdateChecklistItem(ChecklistItemDto ChecklistitemDto)
		{
			var url = $"api/ChecklistItem/UpdateChecklistItem";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, ChecklistitemDto);
			return res.Data!;
		}
		public async Task<CommonApiResponse<bool>> DeleteChecklistItem(Guid taskId)
		{
			var url = $"api/ChecklistItem/DeleteChecklistItem/{taskId}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);
			return res;
		}
	}
}
