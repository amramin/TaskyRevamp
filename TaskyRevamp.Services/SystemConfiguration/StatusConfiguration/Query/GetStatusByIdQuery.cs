using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using statusSetting = TaskyRevamp.Domain.Models.SystemConfiguration.StatusSettings;

namespace TaskyRevamp.Services.SystemConfiguration.StatusConfiguration.Query
{
	public record GetStatusByIdQuery(Guid id) : IRequest<StatusSettingsDto>;
	public class GetStatusByIdHandler : IRequestHandler<GetStatusByIdQuery, StatusSettingsDto>
	{
		private readonly IRepository<statusSetting> _StatusRepository;
		public GetStatusByIdHandler(IRepository<statusSetting> _statusRepository)
		{
			_StatusRepository = _statusRepository;
		}

		public async Task<StatusSettingsDto> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
		{
			StatusSettingsDto statusQuery = new StatusSettingsDto();
			var res = await _StatusRepository.FindByKey(request.id);
			if (res.Success && res != null)
			{
				statusQuery = res.Value.CopyToDto();
			}
			return statusQuery;
		}
	}
}
