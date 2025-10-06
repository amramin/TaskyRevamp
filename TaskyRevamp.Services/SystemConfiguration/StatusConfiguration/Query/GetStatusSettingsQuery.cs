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
	public class GetStatusSettingsHandler : IRequestHandler<GetStatusSettingsQuery, List<StatusSettingsDto>>
	{
		private readonly IRepository<statusSettings> _statusRepository;

		public GetStatusSettingsHandler(IRepository<statusSettings> statusRepository)
		{
			this._statusRepository = statusRepository;
		}
		public async Task<List<StatusSettingsDto>> Handle(GetStatusSettingsQuery request, CancellationToken cancellationToken)
		{
			var statusSettingsDto = new List<StatusSettingsDto>();
			var statusQuieriesResponse = await _statusRepository.AllAsNoTracking();
			if (statusQuieriesResponse.Success && statusQuieriesResponse.Value != null && statusQuieriesResponse.Value.Any())
			{
				statusSettingsDto = statusQuieriesResponse.Value.Select(q => q.CopyToDto()).ToList();
			}
			return statusSettingsDto;
		}
	}
}
