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
	public record UpdateRecycleBinSettingCommand(RecycleBinSettingDto RecycleBinSettingDto) : IRequest<bool>;

	public class UpdateRecycleBinSettingHandler : IRequestHandler<UpdateRecycleBinSettingCommand, bool>
	{
		private readonly IRepository<TaskyRevamp.Domain.Models.SystemConfiguration.RecycleBinSettings> _recycleBinSettingsRepository;

		public UpdateRecycleBinSettingHandler(IRepository<Domain.Models.SystemConfiguration.RecycleBinSettings> recycleBinSettingsRepository)
		{
			_recycleBinSettingsRepository = recycleBinSettingsRepository;
		}

		public async Task<bool> Handle(UpdateRecycleBinSettingCommand request, CancellationToken cancellationToken)
		{
			var res = await _recycleBinSettingsRepository.AllAsNoTracking();
			if (res != null && res.Success)
			{
				if(res.Value == null || (res.Value != null && res.Value.Count() == 0))
				{
					
					var recycleBinSettings = new RecycleBinSetting(request.RecycleBinSettingDto.PeriodType, request.RecycleBinSettingDto.CustomDays);
					await _recycleBinSettingsRepository.Insert(recycleBinSettings);
				}
				else
				{
					var recycleBinRecord = res.Value.FirstOrDefault();
					if (request.RecycleBinSettingDto.PeriodType != PeriodType.Custom)
					{
						request.RecycleBinSettingDto.CustomDays = null;
					}
					recycleBinRecord.Update(request.RecycleBinSettingDto.PeriodType, request.RecycleBinSettingDto.CustomDays);
					await _recycleBinSettingsRepository.Update(recycleBinRecord);
				}
				
			}
			

			return true;
		}

	}
	
}
