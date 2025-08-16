using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using RecycleBinSetting = TaskyRevamp.Domain.Models.SystemConfiguration.RecycleBinSettings;

namespace TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Query
{
    public record GetRecycleBinSettingQuery() : IRequest<RecycleBinSettingDto>;

    public class GetRecycleBinSettingHandler : IRequestHandler<GetRecycleBinSettingQuery, RecycleBinSettingDto>
    {
        private readonly IRepository<RecycleBinSetting> _recycleBinSettingsRepository;

        public GetRecycleBinSettingHandler(IRepository<RecycleBinSetting> recycleBinSettingsRepository)
        {
            _recycleBinSettingsRepository = recycleBinSettingsRepository;
        }

        public async Task<RecycleBinSettingDto> Handle(GetRecycleBinSettingQuery request, CancellationToken cancellationToken)
        {
            RecycleBinSettingDto recycleBinSettingDto = new RecycleBinSettingDto();

            var settingsResponse = await _recycleBinSettingsRepository.AllAsNoTracking();

            if (settingsResponse.Success && settingsResponse.Value != null && settingsResponse.Value.Any())
            {
                var recycleBinSetting = settingsResponse.Value.FirstOrDefault();
                recycleBinSettingDto = recycleBinSetting.CopyToDto();
            }

            return recycleBinSettingDto;
        }
    }
}
