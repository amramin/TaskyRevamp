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
	public record GetStatusByIdQuery(Guid Id) : IRequest<StatusSettingsDto>;
	public class GetStatusByIdHandler : IRequestHandler<GetStatusByIdQuery, StatusSettingsDto>
	{
		private readonly IRepository<statusSetting> _statusRepository;
		public GetStatusByIdHandler(IRepository<statusSetting> statusRepository)
		{
			this._statusRepository = statusRepository;
		}

		public async Task<StatusSettingsDto> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
		{
			var statusQuery = new StatusSettingsDto();
			var res = await _statusRepository.FindByKey(request.Id);
			if (res.Success && res != null)
			{
				statusQuery = res.Value.CopyToDto();
			}
			return statusQuery;
		}
	}
}
