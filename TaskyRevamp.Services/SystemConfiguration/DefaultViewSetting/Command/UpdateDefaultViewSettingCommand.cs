using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using DefaultSettings = TaskyRevamp.Domain.Models.SystemConfiguration.DefaultViewSettings;
using ViewSettings = TaskyRevamp.Domain.Models.SystemConfiguration.ViewTaskSettings;

namespace TaskyRevamp.Services.SystemConfiguration.DefaultViewSetting.Command
{
	public record UpdateDefaultViewSettingCommand (DefaultViewSettingsDto ViewSettingsDto): IRequest<bool>;
	public class UpdateDefaultViewSettingCommandHandler : IRequestHandler<UpdateDefaultViewSettingCommand, bool>
	{
		private readonly IRepository<DefaultSettings> _DefaultSettingsRepository;
		private readonly IRepository<ViewSettings> _ViewSettingsRepository;
		public UpdateDefaultViewSettingCommandHandler(IRepository<DefaultSettings> _defaultSettingsRepository, IRepository<ViewSettings> _viewSettingsRepository)
		{
			_DefaultSettingsRepository = _defaultSettingsRepository;	
			_ViewSettingsRepository = _viewSettingsRepository;
		}
		public async Task<bool> Handle(UpdateDefaultViewSettingCommand request, CancellationToken cancellationToken)
		{
			var res = await _DefaultSettingsRepository.AllAsNoTracking();
			if (res.Success && res != null)
			{
				var viewsetting = await _ViewSettingsRepository.AllAsNoTracking();
				if (viewsetting.Success && viewsetting != null && viewsetting.Value != null)
				{
					var originalview = viewsetting.Value.FirstOrDefault(v => (int)v.ViewType == (int)request.ViewSettingsDto.DefaultSelected);
					if (originalview != null)
					{
						if (originalview.IsActive)
						{
							if (res.Value == null || (res.Value != null && res.Value.Count() == 0))
							{
								DefaultViewSettings newView = new DefaultViewSettings();
								newView.DefaultSelected = request.ViewSettingsDto.DefaultSelected;
								await _DefaultSettingsRepository.Insert(newView);
							}
							else
							{
								var defaultview = res.Value!.FirstOrDefault();
								if (defaultview != null)
								{
									defaultview.DefaultSelected = request.ViewSettingsDto.DefaultSelected;
									await _DefaultSettingsRepository.Update(defaultview);
								}
							}
						}
					}
				}
				
			}
			return true;
		}
	}
}
