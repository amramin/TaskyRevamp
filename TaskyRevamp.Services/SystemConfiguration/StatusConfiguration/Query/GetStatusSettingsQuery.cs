using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using statusSettings = TaskyRevamp.Domain.Models.SystemConfiguration.StatusSettings;

namespace TaskyRevamp.Services.SystemConfiguration.StatusConfiguration.Query
{
	public record GetStatusSettingsQuery() : IRequest<List<StatusSettingsDto>>;
	public class GetStatusSettingsQueryHandler : IRequestHandler<GetStatusSettingsQuery, List<StatusSettingsDto>>
	{
		private readonly IRepository<statusSettings> _StatusRepository;

		public GetStatusSettingsQueryHandler(IRepository<statusSettings> _statusRepository)
		{
			_StatusRepository = _statusRepository;
		}
		public async Task<List<StatusSettingsDto>> Handle(GetStatusSettingsQuery request, CancellationToken cancellationToken)
		{
			List<StatusSettingsDto> _statusSettingsDto = new List<StatusSettingsDto>();
			var statusQuieriesResponse = await _StatusRepository.AllAsNoTracking();
			if (statusQuieriesResponse.Success && statusQuieriesResponse.Value != null && statusQuieriesResponse.Value.Any())
			{
				_statusSettingsDto = statusQuieriesResponse.Value.Select(q => q.CopyToDto()).ToList();
			}
			return _statusSettingsDto;
		}
	}
}
