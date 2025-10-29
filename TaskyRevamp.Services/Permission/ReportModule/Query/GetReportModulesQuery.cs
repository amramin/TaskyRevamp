using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions.ReportModule;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions.ReportModule;

namespace TaskyRevamp.Services.Permission.RepotModule.Query
{

    public record GetReportModulesQuery() : IRequest<List<ReportModuleDto>>;
    public class GetReportModulesHandler : IRequestHandler<GetReportModulesQuery, List<ReportModuleDto>>
    {
        private readonly IRepository<ReportModule> _reportModuleRepository;
        public GetReportModulesHandler(IRepository<ReportModule> reportModuleRepository)
        {
            _reportModuleRepository = reportModuleRepository;
        }
        public async Task<List<ReportModuleDto>> Handle(GetReportModulesQuery request, CancellationToken cancellationToken)
        {
            List<ReportModuleDto> reportModules = new List<ReportModuleDto>();
            var res = await _reportModuleRepository.AllAsNoTracking();
            if (res.Success && res != null && res.Value != null)
            {
                reportModules = res.Value.Select(g => g.CopyToDto()).ToList();
            }

            return reportModules;
        }
    }
}
