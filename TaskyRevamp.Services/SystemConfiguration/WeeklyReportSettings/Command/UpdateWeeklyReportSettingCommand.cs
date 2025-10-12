using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using WeeklyReportSetting = TaskyRevamp.Domain.Models.SystemConfiguration.WeeklyReportSettings;

namespace TaskyRevamp.Services.SystemConfiguration.WeeklyReportSettings.Command
{
	public record UpdateWeeklyReportSettingCommand(WeeklyReportSettingsDto weeklyReportSettingsDto) : IRequest<bool>;

	public class UpdateWeeklyReportSettingCommandHandler : IRequestHandler<UpdateWeeklyReportSettingCommand, bool>
	{
		private readonly IRepository<WeeklyReportSetting> _weeklyReortSettingRepository;

		public UpdateWeeklyReportSettingCommandHandler(IRepository<WeeklyReportSetting> weeklyReortSettingRepository)
		{
			_weeklyReortSettingRepository = weeklyReortSettingRepository;
		}
		public async Task<bool> Handle(UpdateWeeklyReportSettingCommand request, CancellationToken cancellationToken)
		{
			var weeklySetting = await _weeklyReortSettingRepository.AllAsNoTracking();
			if(weeklySetting.Success && weeklySetting != null)
			{
				if (weeklySetting.Value == null || (weeklySetting.Value != null && weeklySetting.Value.Count() == 0))
				{
					WeeklyReportSetting newWeeklyReportSetting = new WeeklyReportSetting
					{
						Day = request.weeklyReportSettingsDto.Day,
						Time = request.weeklyReportSettingsDto.Time,
						Language = request.weeklyReportSettingsDto.Language,
					};
					await _weeklyReortSettingRepository.Insert(newWeeklyReportSetting);
				}
				else
				{
					var WeeklyReportQuery = weeklySetting.Value!.FirstOrDefault();
					if(WeeklyReportQuery != null)
					{
						WeeklyReportQuery.Day = request.weeklyReportSettingsDto.Day;
						WeeklyReportQuery.Time = request.weeklyReportSettingsDto.Time;
						WeeklyReportQuery.Language = request.weeklyReportSettingsDto.Language;
						await _weeklyReortSettingRepository.Update(WeeklyReportQuery);
					}
				}
			}
			
			return true;
		}
	}
}
