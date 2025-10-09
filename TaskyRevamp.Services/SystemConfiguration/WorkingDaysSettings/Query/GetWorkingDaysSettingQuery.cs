using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using WorkingDaySetting = TaskyRevamp.Domain.Models.SystemConfiguration.WorkingDaysSettings;

namespace TaskyRevamp.Services.SystemConfiguration.WorkingDaysSettings.Query
{
	public record GetWorkingDaysSettingQuery() : IRequest<List<WorkingDaysSettingsDto>>;
	public class GetWorkingDaysSettingQueryHandler : IRequestHandler<GetWorkingDaysSettingQuery, List<WorkingDaysSettingsDto>>
	{
		private readonly IRepository<WorkingDaySetting> _workingDaySettingRepository;
		public GetWorkingDaysSettingQueryHandler(IRepository<WorkingDaySetting> workingDaySettingRepository)
		{
			_workingDaySettingRepository = workingDaySettingRepository;
		}
		public async Task<List<WorkingDaysSettingsDto>> Handle(GetWorkingDaysSettingQuery request, CancellationToken cancellationToken)
		{
			List<WorkingDaysSettingsDto> workingDaysSettingsDtos = new List<WorkingDaysSettingsDto>();
			var res = await _workingDaySettingRepository.AllAsNoTracking();
			if(res.Success && res != null && res.Value!= null)
			{
				workingDaysSettingsDtos = res.Value.Select(d => d.CopyToDto()).ToList();
			} 
			return workingDaysSettingsDtos;
		}
	}
}
