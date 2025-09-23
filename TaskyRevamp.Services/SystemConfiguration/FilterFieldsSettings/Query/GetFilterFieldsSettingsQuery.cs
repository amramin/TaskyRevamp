using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterFieldsSetting = TaskyRevamp.Domain.Models.SystemConfiguration.FilterFieldsSettings;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SystemConfiguration.FilterFieldsSettings.Query
{
    public record GetFilterFieldsSettingsQuery() : IRequest<List<FilterFieldsSettingDto>>;

    public class GetFilterFieldsSettingsHandler : IRequestHandler<GetFilterFieldsSettingsQuery, List<FilterFieldsSettingDto>>
    {
        private readonly IRepository<FilterFieldsSetting> _filterFieldsSettingRepository;

        public GetFilterFieldsSettingsHandler(IRepository<FilterFieldsSetting> filterFieldsSettingRepository)
        {
            _filterFieldsSettingRepository = filterFieldsSettingRepository;
        }

        public async Task<List<FilterFieldsSettingDto>> Handle(GetFilterFieldsSettingsQuery request, CancellationToken cancellationToken)
        {
            List<FilterFieldsSettingDto> filterFieldsSettingDtos = new List<FilterFieldsSettingDto>();

            var settingsResponse = await _filterFieldsSettingRepository.AllAsNoTracking();

            if (settingsResponse.Success && settingsResponse.Value != null && settingsResponse.Value.Any())
            {
                filterFieldsSettingDtos = settingsResponse.Value.Select(x => x.CopyToDto()).OrderBy(x => x.Order).ToList();
            }

            return filterFieldsSettingDtos;
        }
    }
}
