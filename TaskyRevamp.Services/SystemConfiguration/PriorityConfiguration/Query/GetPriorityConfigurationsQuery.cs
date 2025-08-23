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
	public class GetPriorityConfigurationsQuery : IRequestHandler<GetProrityQuery, List<PriorityDto>>
	{
		private readonly IRepository<prioritySettings> _PriorityRepository;
		public GetPriorityConfigurationsQuery(IRepository<prioritySettings> _priorityRepository)
		{
			_PriorityRepository = _priorityRepository;
		}
		public async Task<List<PriorityDto>> Handle(GetProrityQuery request, CancellationToken cancellationToken)
		{
			List<PriorityDto> _priorityDto = new List<PriorityDto>();
			var priortiyQuieriesResponse = await _PriorityRepository.AllAsNoTracking();
			if (priortiyQuieriesResponse.Success && priortiyQuieriesResponse.Value != null && priortiyQuieriesResponse.Value.Any())
			{
				_priorityDto = priortiyQuieriesResponse.Value.Select(q => q.CopyToDto()).ToList();
			}
			return _priorityDto;
		}
	}
}
