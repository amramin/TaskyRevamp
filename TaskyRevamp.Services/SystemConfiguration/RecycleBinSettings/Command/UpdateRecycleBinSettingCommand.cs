using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;
using RecycleBinSetting = TaskyRevamp.Domain.Models.SystemConfiguration.RecycleBinSettings;

namespace TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Command
{
	public record UpdateRecycleBinSettingCommand(RecycleBinSettingDto _RecycleBinSettingDto) : IRequest<bool>;

	public class UpdateRecycleBinSettingHandler : IRequestHandler<UpdateRecycleBinSettingCommand, bool>
	{
		private readonly IRepository<TaskyRevamp.Domain.Models.SystemConfiguration.RecycleBinSettings> _RecycleBinSettingsRepository;

		public UpdateRecycleBinSettingHandler(IRepository<Domain.Models.SystemConfiguration.RecycleBinSettings> RecycleBinSettingsRepository)
		{
			_RecycleBinSettingsRepository = RecycleBinSettingsRepository;
		}

		public async Task<bool> Handle(UpdateRecycleBinSettingCommand request, CancellationToken cancellationToken)
		{
			var res = await _RecycleBinSettingsRepository.AllAsNoTracking();
			if (res != null && res.Success)
			{
				if(res.Value == null || (res.Value != null && res.Value.Count() == 0))
				{
					
					RecycleBinSetting recycleBinSettings = new RecycleBinSetting(request._RecycleBinSettingDto.PeriodType, request._RecycleBinSettingDto.CustomDays);
					await _RecycleBinSettingsRepository.Insert(recycleBinSettings);
				}
				else
				{
					var recycleBinRecord = res.Value.FirstOrDefault();
					if (request._RecycleBinSettingDto.PeriodType != PeriodType.Custom)
					{
						request._RecycleBinSettingDto.CustomDays = null;
					}
					recycleBinRecord.Update(request._RecycleBinSettingDto.PeriodType, request._RecycleBinSettingDto.CustomDays);
					await _RecycleBinSettingsRepository.Update(recycleBinRecord);
				}
				
			}
			

			return true;
		}

	}
	
}
