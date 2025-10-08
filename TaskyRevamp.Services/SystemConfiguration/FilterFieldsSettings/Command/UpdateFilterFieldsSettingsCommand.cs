using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using FilterFieldsSetting = TaskyRevamp.Domain.Models.SystemConfiguration.FilterFieldsSettings;

namespace TaskyRevamp.Services.SystemConfiguration.FilterFieldsSettings.Command
{
    public record UpdateFilterFieldsSettingsCommand(List<FilterFieldsSettingDto> FilterFieldsSettings) : IRequest<bool>;

    public class UpdateFilterFieldsSettingsHandler : IRequestHandler<UpdateFilterFieldsSettingsCommand, bool>
    {
        private readonly IRepository<FilterFieldsSetting> _filterFieldsSettingRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateFilterFieldsSettingsHandler(IRepository<FilterFieldsSetting> filterFieldsSettingRepository, IStringLocalizer<SharedResources> localizer)
        {
            _filterFieldsSettingRepository = filterFieldsSettingRepository;
            _localizer = localizer;
        }

        public async Task<bool> Handle(UpdateFilterFieldsSettingsCommand request, CancellationToken cancellationToken)
        {
            
            foreach (var setting in request.FilterFieldsSettings)
            {
                var existingSettingResponse = await _filterFieldsSettingRepository.FindByKey(setting.Id);

                if (existingSettingResponse == null || !existingSettingResponse.Success || existingSettingResponse.Value == null)
                {
                    throw new NotFoundException(_localizer[ApiError.FilterFieldNotFound].Value.Replace("{id}", setting.Id.ToString()));
                }
                    
                var existingSetting = existingSettingResponse.Value;
                existingSetting.Update(setting);
                await _filterFieldsSettingRepository.Update(existingSetting);
                
            }

            return true;
        }

    }
}
