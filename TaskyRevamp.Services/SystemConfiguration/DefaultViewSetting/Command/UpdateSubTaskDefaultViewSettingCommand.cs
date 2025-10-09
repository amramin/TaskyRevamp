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
	public class UpdateSubTaskDefaultViewSettingHandler : IRequestHandler<UpdateSubTaskDefaultViewSettingCommand, bool>
	{
		private readonly IRepository<ViewSettings> _viewSettingsRepository;

		public UpdateSubTaskDefaultViewSettingHandler(IRepository<ViewSettings> viewSettingsRepository)
		{
			_viewSettingsRepository = viewSettingsRepository;
		}
		public async Task<bool> Handle(UpdateSubTaskDefaultViewSettingCommand request, CancellationToken cancellationToken)
		{

			var originalSetting = await _viewSettingsRepository.AllAsNoTracking();
			if(originalSetting.Success && originalSetting != null && originalSetting.Value != null)
			{
				var originalSettingData = originalSetting.Value.FirstOrDefault();
				var newViewSetting = request.ViewSettingDto;
				if(newViewSetting != null)
				{
					if(newViewSetting.SubTaskLevels != originalSettingData!.SubTaskLevels)
					{
						originalSettingData.SubTaskLevels = newViewSetting.SubTaskLevels;
						await _viewSettingsRepository.Update(originalSettingData);
					}
				}
			}
			return true;
		}
	}

}
