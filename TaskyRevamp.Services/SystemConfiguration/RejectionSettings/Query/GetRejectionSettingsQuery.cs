using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using RejectionSetting = TaskyRevamp.Domain.Models.SystemConfiguration.RejectionSettings;

namespace TaskyRevamp.Services.SystemConfiguration.RejectionSettings.Query
{
	public record GetRejectionSettingsQuery() : IRequest<RejectionSettingsDto>;
	public class GetRejectionSettingsQueryHandler : IRequestHandler<GetRejectionSettingsQuery, RejectionSettingsDto>
	{
		private readonly IRepository<RejectionSetting> _rejectionSettingRepositry;
		public GetRejectionSettingsQueryHandler(IRepository<RejectionSetting> rejectionSettingRepositry)
		{
			this._rejectionSettingRepositry = rejectionSettingRepositry;
		}

		public async Task<RejectionSettingsDto> Handle(GetRejectionSettingsQuery request, CancellationToken cancellationToken)
		{
			var rejectionSettingDto = new RejectionSettingsDto();

			var settingsResponse = await _rejectionSettingRepositry.AllAsNoTracking();

			if (settingsResponse.Success && settingsResponse.Value != null && settingsResponse.Value.Any())
			{
				var rejectionSetting = settingsResponse.Value.FirstOrDefault();
				rejectionSettingDto = rejectionSetting.CopyToDto();
			}

			return rejectionSettingDto;
		}
	}
}
