using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using SystemIdentity = TaskyRevamp.Domain.Models.SystemConfiguration.SystemIdentity;

namespace TaskyRevamp.Services.SystemConfiguration.SystemIdentityConfiguration.Query
{
	public record GetSystemIdentityQuery() : IRequest<SystemIdentityDto>;
	public class GetSystemIdentityHandler : IRequestHandler<GetSystemIdentityQuery, SystemIdentityDto>
	{
		private readonly IRepository<SystemIdentity> _systemIdentityRepository;
		public GetSystemIdentityHandler(IRepository<SystemIdentity> systemIdentityRepository)
		{
			_systemIdentityRepository = systemIdentityRepository;
		}
		public async Task<SystemIdentityDto> Handle(GetSystemIdentityQuery request, CancellationToken cancellationToken)
		{
			SystemIdentityDto systemIdentityDto = new SystemIdentityDto();
			var _systemSetting = await _systemIdentityRepository.AllAsNoTracking();
			if(_systemSetting.Success && _systemSetting.Value != null && _systemSetting.Value.Any())
			{
				var setting = _systemSetting.Value.FirstOrDefault();
				systemIdentityDto = setting!.CopyToDto();
			}
			return systemIdentityDto;
		}
	}
}
