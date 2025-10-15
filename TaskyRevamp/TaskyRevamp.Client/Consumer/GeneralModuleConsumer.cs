using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class GeneralModuleConsumer
	{
		private readonly TaskyService _taskyService;
		public GeneralModuleConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<List<GeneralModuleDto>>> GetGeneralModules()
		{
			var url = $"api/GeneralModule/GetGeneralModules";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<GeneralModuleDto>>>(url);

			return res;
		}
	}
}
