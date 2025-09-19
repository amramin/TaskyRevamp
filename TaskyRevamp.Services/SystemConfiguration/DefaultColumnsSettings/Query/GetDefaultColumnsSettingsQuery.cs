using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultColumnsSetting = TaskyRevamp.Domain.Models.SystemConfiguration.DefaultColumnsSettings;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SystemConfiguration.DefaultColumnsSettings.Query
{
    public record GetDefaultColumnsSettingsQuery() : IRequest<List<DefaultColumnsSettingDto>>;

    public class GetDefaultColumnsSettingsHandler : IRequestHandler<GetDefaultColumnsSettingsQuery, List<DefaultColumnsSettingDto>>
    {
        private readonly IRepository<DefaultColumnsSetting> _defaultColumnsSettingRepository;

        public GetDefaultColumnsSettingsHandler(IRepository<DefaultColumnsSetting> defaultColumnsSettingRepository)
        {
            _defaultColumnsSettingRepository = defaultColumnsSettingRepository;
        }

        public async Task<List<DefaultColumnsSettingDto>> Handle(GetDefaultColumnsSettingsQuery request, CancellationToken cancellationToken)
        {
            List<DefaultColumnsSettingDto> defaultColumnsSettingsDto = new List<DefaultColumnsSettingDto>();

            var settingsResponse = await _defaultColumnsSettingRepository.AllAsNoTracking();

            if (settingsResponse.Success && settingsResponse.Value != null && settingsResponse.Value.Any())
            {
                defaultColumnsSettingsDto = settingsResponse.Value.Select(x => x.CopyToDto()).OrderBy(x => x.Order).ToList();
            }

            return defaultColumnsSettingsDto;
        }
    }
}
