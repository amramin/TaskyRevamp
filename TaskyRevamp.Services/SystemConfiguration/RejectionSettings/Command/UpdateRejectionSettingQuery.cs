using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;
using RejectionSetting = TaskyRevamp.Domain.Models.SystemConfiguration.RejectionSettings;

namespace TaskyRevamp.Services.SystemConfiguration.RejectionSettings.Command
{
	public record UpdateRejectionSettingCommand(RejectionSettingsDto __RejectionSettingDto) : IRequest<bool>;

	public class UpdateRejectionSettingQueryHandler : IRequestHandler<UpdateRejectionSettingCommand, bool>
	{
		private readonly IRepository<RejectionSetting> _RejectionSettingsRepository;
		public UpdateRejectionSettingQueryHandler(IRepository<RejectionSetting> _rejectionSettingsRepository)
		{
			_RejectionSettingsRepository = _rejectionSettingsRepository;
		}

		public async Task<bool> Handle(UpdateRejectionSettingCommand request, CancellationToken cancellationToken)
		{
			var res = await _RejectionSettingsRepository.AllAsNoTracking();
			if (res != null && res.Success)
			{
				if (res.Value == null || (res.Value != null && res.Value.Count() == 0))
				{

					RejectionSetting rejectionSettings = new RejectionSetting(request.__RejectionSettingDto.PeriodType, request.__RejectionSettingDto.CustomDays);
					await _RejectionSettingsRepository.Insert(rejectionSettings);
				}
				else
				{
					var rejectionRecord = res.Value.FirstOrDefault();
					if (request.__RejectionSettingDto.PeriodType != RejectionPeriodType.Custom)
					{
						request.__RejectionSettingDto.CustomDays = null;
					}
					rejectionRecord.Update(request.__RejectionSettingDto.PeriodType, request.__RejectionSettingDto.CustomDays);
					await _RejectionSettingsRepository.Update(rejectionRecord);
				}

			}


			return true;
		}
	}
}
