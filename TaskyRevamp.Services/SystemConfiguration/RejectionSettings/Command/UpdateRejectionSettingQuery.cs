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
	public record UpdateRejectionSettingCommand(RejectionSettingsDto RejectionSettingDto) : IRequest<bool>;

	public class UpdateRejectionSettingQueryHandler : IRequestHandler<UpdateRejectionSettingCommand, bool>
	{
		private readonly IRepository<RejectionSetting> _rejectionSettingsRepository;
		public UpdateRejectionSettingQueryHandler(IRepository<RejectionSetting> rejectionSettingsRepository)
		{
			this._rejectionSettingsRepository = rejectionSettingsRepository;
		}

		public async Task<bool> Handle(UpdateRejectionSettingCommand request, CancellationToken cancellationToken)
		{
			var res = await _rejectionSettingsRepository.AllAsNoTracking();
			if (res != null && res.Success)
			{
				if (res.Value == null || (res.Value != null && res.Value.Count() == 0))
				{

					var rejectionSettings = new RejectionSetting(request.RejectionSettingDto.PeriodType, request.RejectionSettingDto.CustomDays);
					await _rejectionSettingsRepository.Insert(rejectionSettings);
				}
				else
				{
					var rejectionRecord = res.Value.FirstOrDefault();
					if (request.RejectionSettingDto.PeriodType != RejectionPeriodType.Custom)
					{
						request.RejectionSettingDto.CustomDays = null;
					}
					rejectionRecord.Update(request.RejectionSettingDto.PeriodType, request.RejectionSettingDto.CustomDays);
					await _rejectionSettingsRepository.Update(rejectionRecord);
				}

			}


			return true;
		}
	}
}
