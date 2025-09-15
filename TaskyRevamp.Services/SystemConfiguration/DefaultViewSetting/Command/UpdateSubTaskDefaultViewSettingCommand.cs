using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using ViewSettings = TaskyRevamp.Domain.Models.SystemConfiguration.DefaultViewSettings;

namespace TaskyRevamp.Services.SystemConfiguration.DefaultViewSetting.Command
{
	public record UpdateSubTaskDefaultViewSettingCommand(DefaultViewSettingsDto ViewSettingDto): IRequest<bool>;
	public class UpdateSubTaskDefaultViewSettingCommandHandler : IRequestHandler<UpdateSubTaskDefaultViewSettingCommand, bool>
	{
		private readonly IRepository<ViewSettings> _ViewSettingsRepository;

		public UpdateSubTaskDefaultViewSettingCommandHandler(IRepository<ViewSettings> _viewSettingsRepository)
		{
			_ViewSettingsRepository = _viewSettingsRepository;
		}
		public async Task<bool> Handle(UpdateSubTaskDefaultViewSettingCommand request, CancellationToken cancellationToken)
		{

			var originalSetting = await _ViewSettingsRepository.AllAsNoTracking();
			if(originalSetting.Success && originalSetting != null && originalSetting.Value != null)
			{
				var originalSettingData = originalSetting.Value.FirstOrDefault();
				var newViewSetting = request.ViewSettingDto;
				if(newViewSetting != null)
				{
					if(newViewSetting.SubTaskLevels != originalSettingData!.SubTaskLevels)
					{
						originalSettingData.SubTaskLevels = newViewSetting.SubTaskLevels;
						await _ViewSettingsRepository.Update(originalSettingData);
					}
				}
			}
			return true;
		}
	}

}
