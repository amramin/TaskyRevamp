using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using DefaultSettings = TaskyRevamp.Domain.Models.SystemConfiguration.DefaultViewSettings;

namespace TaskyRevamp.Services.SystemConfiguration.DefaultViewSetting.Query
{
	public record GetDefaultViewSettingQuery() :IRequest<DefaultViewSettingsDto>;
	public class GetDefaultViewSettingQueryHandler : IRequestHandler<GetDefaultViewSettingQuery, DefaultViewSettingsDto>
	{
		private readonly IRepository<DefaultSettings> _DefaultSettingsRepository;
		public GetDefaultViewSettingQueryHandler(IRepository<DefaultSettings> _defaultSettingsRepository)
		{
			_DefaultSettingsRepository = _defaultSettingsRepository;
		}
		public async Task<DefaultViewSettingsDto> Handle(GetDefaultViewSettingQuery request, CancellationToken cancellationToken)
		{
			DefaultViewSettingsDto defaultViewSettingsDto = new DefaultViewSettingsDto();
			var res = await _DefaultSettingsRepository.AllAsNoTracking();
			if (res.Success && res.Value != null && res.Value.Any())
			{
				var viewsetting = res.Value.FirstOrDefault();
				if (viewsetting != null)
				{
					defaultViewSettingsDto = viewsetting.CopyToDto();
				}
			}
			return defaultViewSettingsDto;
		}
	}
}
