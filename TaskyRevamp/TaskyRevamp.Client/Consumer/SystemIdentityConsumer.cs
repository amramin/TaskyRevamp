using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class SystemIdentityConsumer
	{
		private readonly TaskyService _taskyService;
		public SystemIdentityConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<SystemIdentityDto>> GetSystemIdentitySetting()
		{
			var url = $"api/SystemIdentity/GetSystemIdentitySetting";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<SystemIdentityDto>>(url);

			return res;
		}

		public async Task<CommonApiResponse<bool>> UpdateSystemIdentitySetting(SystemIdentityDto systemIdentity)
		{
			var url = $"api/SystemIdentity/UpdateSystemIdentitySetting";
			var res = await _taskyService.PostJsonAsync<bool>(url, systemIdentity);

			return res;
		}
	}
}
