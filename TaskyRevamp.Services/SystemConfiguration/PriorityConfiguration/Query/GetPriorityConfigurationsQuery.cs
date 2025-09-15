using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using prioritySettings = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query
{
	public record GetProrityQuery() :IRequest<List<PriorityDto>>;
	public class GetPriorityConfigurationsHandler : IRequestHandler<GetProrityQuery, List<PriorityDto>>
	{
		private readonly IRepository<prioritySettings> _priorityRepository;
		public GetPriorityConfigurationsHandler(IRepository<prioritySettings> priorityRepository)
		{
			this._priorityRepository = priorityRepository;
		}
		public async Task<List<PriorityDto>> Handle(GetProrityQuery request, CancellationToken cancellationToken)
		{
			var priorityDto = new List<PriorityDto>();
			var priortiyQuieriesResponse = await _priorityRepository.AllAsNoTracking();
			if (priortiyQuieriesResponse.Success && priortiyQuieriesResponse.Value != null && priortiyQuieriesResponse.Value.Any())
			{
				priorityDto = priortiyQuieriesResponse.Value.Select(q => q.CopyToDto()).ToList();
			}
			return priorityDto;
		}
	}
}
