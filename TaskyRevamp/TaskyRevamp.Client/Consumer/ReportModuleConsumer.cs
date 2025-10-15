using Microsoft.AspNetCore.Components;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Client.Consumer
{
    public class ReportModuleConsumer
    {
        private readonly TaskyService _taskyService;
        public ReportModuleConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }
        public async Task<CommonApiResponse<List<ReportModuleDto>>> GetReportModules()
        {
            var url = $"api/ReportModule/GetReportModules";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<ReportModuleDto>>>(url);

            return res;
        }
    }
}
