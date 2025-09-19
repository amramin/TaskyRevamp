using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterFieldsSetting = TaskyRevamp.Domain.Models.SystemConfiguration.FilterFieldsSettings;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SystemConfiguration.FilterFieldsSettings.Command
{
    public record UpdateFilterFieldsSettingsCommand(List<FilterFieldsSettingDto> FilterFieldsSettings) : IRequest<bool>;

    public class UpdateFilterFieldsSettingsHandler : IRequestHandler<UpdateFilterFieldsSettingsCommand, bool>
    {
        private readonly IRepository<FilterFieldsSetting> _filterFieldsSettingRepository;

        public UpdateFilterFieldsSettingsHandler(IRepository<FilterFieldsSetting> filterFieldsSettingRepository)
        {
            _filterFieldsSettingRepository = filterFieldsSettingRepository;
        }

        public async Task<bool> Handle(UpdateFilterFieldsSettingsCommand request, CancellationToken cancellationToken)
        {

            foreach (var setting in request.FilterFieldsSettings)
            {
                var existingSettingResponse = await _filterFieldsSettingRepository.FindByKey(setting.Id);
                if (existingSettingResponse != null && existingSettingResponse.Success && existingSettingResponse.Value != null)
                {
                    var existingSetting = existingSettingResponse.Value;
                    existingSetting.Update(setting);
                    await _filterFieldsSettingRepository.Update(existingSetting);
                }
            }

            return true;
        }

    }
}
