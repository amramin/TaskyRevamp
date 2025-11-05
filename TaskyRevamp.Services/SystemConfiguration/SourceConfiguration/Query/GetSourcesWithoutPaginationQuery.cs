using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query
{
	public record GetSourcesWithoutPaginationQuery() : IRequest<List<SourceDto>>;
	public class GetSourcesWithoutPaginationHandler : IRequestHandler<GetSourcesWithoutPaginationQuery, List<SourceDto>>
	{
		private readonly IRepository<Sources> _sourceRepository;
		public GetSourcesWithoutPaginationHandler(IRepository<Sources> sourceRepository)
		{
			_sourceRepository = sourceRepository;
		}
		public async Task<List<SourceDto>> Handle(GetSourcesWithoutPaginationQuery request, CancellationToken cancellationToken)
		{
			List<SourceDto> sources = new List<SourceDto>();
			var res = await _sourceRepository.AllAsNoTracking();
			if (res.Success && res != null && res.Value != null)
			{
				sources = res.Value.Select(source => source.CopyToDto()).ToList();
			}
			return sources;
		}
	}
}
