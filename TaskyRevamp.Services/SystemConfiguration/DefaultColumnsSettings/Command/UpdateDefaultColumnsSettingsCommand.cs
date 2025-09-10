using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultColumnsSetting = TaskyRevamp.Domain.Models.SystemConfiguration.DefaultColumnsSettings;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SystemConfiguration.DefaultColumnsSettings.Command
{
    public record UpdateDefaultColumnsSettingsCommand(List<DefaultColumnsSettingDto> DefaultColumnsSettings) : IRequest<bool>;

    public class UpdateDefaultColumnsSettingsHandler : IRequestHandler<UpdateDefaultColumnsSettingsCommand, bool>
    {
        private readonly IRepository<DefaultColumnsSetting> _defaultColumnsSettingRepository;

        public UpdateDefaultColumnsSettingsHandler(IRepository<DefaultColumnsSetting> defaultColumnsSettingRepository)
        {
            _defaultColumnsSettingRepository = defaultColumnsSettingRepository;
        }

        public async Task<bool> Handle(UpdateDefaultColumnsSettingsCommand request, CancellationToken cancellationToken)
        {
            
            foreach(var setting in request.DefaultColumnsSettings)
            {
                var existingSettingResponse = await _defaultColumnsSettingRepository.FindByKey(setting.Id);
                if(existingSettingResponse != null && existingSettingResponse.Success && existingSettingResponse.Value != null)
                {
                    var existingSetting = existingSettingResponse.Value;
                    existingSetting.Update(setting);
                    await _defaultColumnsSettingRepository.Update(existingSetting);
                }
            }

            return true;
        }

    }
}
