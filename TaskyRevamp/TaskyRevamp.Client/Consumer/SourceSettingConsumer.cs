using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class SourceSettingConsumer
	{
		private readonly TaskyService _taskyService;

		public SourceSettingConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<PagedResult<SourceDtoWithName>>> GetSources(int pageNumber, int pageSize)
		{
			var url = $"api/SourceSetting/GetSourceSettings?pageNumber={pageNumber}&pageSize={pageSize}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<SourceDtoWithName>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<SourceDto>> GetSourceById(Guid id)
		{
			var url = $"api/SourceSetting/GetSourceSettingById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<SourceDto>>(url);
			return res;
		}

		public async Task<CommonApiResponse<bool>> CreateSource(SourceDto SourceDto)
		{
			var url = $"api/SourceSetting/CreateSource";
			var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateSource(SourceDto SourceDto)
		{
			var url = $"api/SourceSetting/UpdateSource";
			var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

			return res;
		}
		public async Task<CommonApiResponse<bool>> DeleteSource(Guid id)
		{
			var url = $"api/SourceSetting/DeleteSource/{id}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

			return res;
		}
	}
}
