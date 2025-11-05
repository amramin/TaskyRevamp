using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using SystemIdentity = TaskyRevamp.Domain.Models.SystemConfiguration.SystemIdentity;

namespace TaskyRevamp.Services.SystemConfiguration.SystemIdentityConfiguration.Command
{
	public record UpdateSystemIdentityCommand(SystemIdentityDto SystemIdentityDto) : IRequest<bool>;
	public class UpdateSystemIdentityHandler : IRequestHandler<UpdateSystemIdentityCommand, bool>
	{
		private readonly IRepository<SystemIdentity> _systemIdentityRepository;
		public UpdateSystemIdentityHandler(IRepository<SystemIdentity> systemIdentityRepository)
		{
			_systemIdentityRepository = systemIdentityRepository;
		}
		public async Task<bool> Handle(UpdateSystemIdentityCommand request, CancellationToken cancellationToken)
		{
			var res = await _systemIdentityRepository.AllAsNoTracking();
			if(res.Success && res != null && res.Value != null)
			{
				var originalSetting = res.Value.FirstOrDefault();
				if(originalSetting != null)
				{
					originalSetting.NameEnglish = request.SystemIdentityDto.NameEnglish;
					originalSetting.NameArabic = request.SystemIdentityDto.NameArabic;
					originalSetting.PrimaryColor = request.SystemIdentityDto.PrimaryColor;
					originalSetting.PrimaryActiveColor = request.SystemIdentityDto.PrimaryActiveColor;
					originalSetting.MainTitle = request.SystemIdentityDto.MainTitle;
					originalSetting.SubTitle = request.SystemIdentityDto.SubTitle;
					originalSetting.NavigationBackground = request.SystemIdentityDto.NavigationBackground;
					originalSetting.BorderColor = request.SystemIdentityDto.BorderColor;
					originalSetting.Logo = request.SystemIdentityDto.Logo;

					await _systemIdentityRepository.Update(originalSetting);
					await _systemIdentityRepository.SaveChangesAsync();
				}
			}
			return true;
		}
	}
}
