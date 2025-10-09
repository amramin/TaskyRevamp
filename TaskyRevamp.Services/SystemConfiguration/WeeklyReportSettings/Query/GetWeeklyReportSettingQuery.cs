using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using WeeklyReportSetting = TaskyRevamp.Domain.Models.SystemConfiguration.WeeklyReportSettings;

namespace TaskyRevamp.Services.SystemConfiguration.WeeklyReportSettings.Query
{
	public record GetWeeklyReportSettingQuery() : IRequest<WeeklyReportSettingsDto>;

	public class GetWeeklyReportSettingQueryHandler : IRequestHandler<GetWeeklyReportSettingQuery, WeeklyReportSettingsDto>
	{
		private readonly IRepository<WeeklyReportSetting> _weeklyReortSettingRepository;

		public GetWeeklyReportSettingQueryHandler(IRepository<WeeklyReportSetting> weeklyReortSettingRepository)
		{
			_weeklyReortSettingRepository = weeklyReortSettingRepository;
		}
		public async Task<WeeklyReportSettingsDto> Handle(GetWeeklyReportSettingQuery request, CancellationToken cancellationToken)
		{
			WeeklyReportSettingsDto settingsDto = new WeeklyReportSettingsDto();
			var res = await _weeklyReortSettingRepository.AllAsNoTracking();
			if (res.Success && res != null && res.Value != null)
			{
				var weeklySetting = res.Value.FirstOrDefault();
				if(weeklySetting != null)
				{
					settingsDto = weeklySetting.CopyToDto();
				}
			}
			return settingsDto;
		}
	}
}
