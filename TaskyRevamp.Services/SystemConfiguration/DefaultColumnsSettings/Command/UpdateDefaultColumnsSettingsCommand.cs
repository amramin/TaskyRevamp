using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using DefaultColumnsSetting = TaskyRevamp.Domain.Models.SystemConfiguration.DefaultColumnsSettings;

namespace TaskyRevamp.Services.SystemConfiguration.DefaultColumnsSettings.Command
{
    public record UpdateDefaultColumnsSettingsCommand(List<DefaultColumnsSettingDto> DefaultColumnsSettings) : IRequest<bool>;

    public class UpdateDefaultColumnsSettingsHandler : IRequestHandler<UpdateDefaultColumnsSettingsCommand, bool>
    {
        private readonly IRepository<DefaultColumnsSetting> _defaultColumnsSettingRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateDefaultColumnsSettingsHandler(IRepository<DefaultColumnsSetting> defaultColumnsSettingRepository, IStringLocalizer<SharedResources> localizer)
        {
            _defaultColumnsSettingRepository = defaultColumnsSettingRepository;
            _localizer = localizer;
        }

        public async Task<bool> Handle(UpdateDefaultColumnsSettingsCommand request, CancellationToken cancellationToken)
        {
            
            foreach(var setting in request.DefaultColumnsSettings)
            {
                var existingSettingResponse = await _defaultColumnsSettingRepository.FindByKey(setting.Id);

                if (existingSettingResponse == null || !existingSettingResponse.Success || existingSettingResponse.Value == null)
                {
                    throw new NotFoundException(_localizer[ApiError.DefaultColumnNotFound].Value.Replace("{id}", setting.Id.ToString()));
                }

                var existingSetting = existingSettingResponse.Value;
                existingSetting.Update(setting);
                await _defaultColumnsSettingRepository.Update(existingSetting);
            }

            return true;
        }

    }
}
