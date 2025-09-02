using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class PrioritySettingConsumer
	{
		private readonly TaskyService _taskyService;

		public PrioritySettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<List<PriorityDto>>> GetPriorities()
		{
			var url = $"api/PrioritySetting/GetPrioritySettings";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<PriorityDto>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<PriorityDto>> GetPriorityById(Guid id)
		{
			var url = $"api/PrioritySetting/GetPriorityById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PriorityDto>>(url);
			return res;
		}

		public async Task<CommonApiResponse<bool>> AddPriority(PriorityDto priorityDto)
		{
			var url = $"api/PrioritySetting/AddPriority";
			var res = await _taskyService.PostJsonAsync<bool>(url, priorityDto);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdatePriority(PriorityDto priorityDto)
		{
			var url = $"api/PrioritySetting/UpdatePriority";
			var res = await _taskyService.PostJsonAsync<bool>(url, priorityDto);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdatePrioritiesOrder(List<PriorityDto> priorities)
		{
			var url = $"api/PrioritySetting/UpdatePrioritiesOrder";
			var res = await _taskyService.PostJsonAsync<bool>(url, priorities);

			return res;
		}

		public async Task<CommonApiResponse<bool>> DeletePriority(Guid id)
		{
			var url = $"api/PrioritySetting/DeletePriority/{id}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

			return res;
		}
	}
}
