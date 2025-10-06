using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query
{
	public record GetSourceByIdQuery(Guid id) : IRequest<SourceDto>;
	public class GetSourceByIdHandler : IRequestHandler<GetSourceByIdQuery, SourceDto>
	{
		private readonly IRepository<Sources> _sourceRepository;
		public GetSourceByIdHandler(IRepository<Sources> sourceRepository)
		{
			_sourceRepository = sourceRepository;
		}
		public async Task<SourceDto> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
		{
			SourceDto sourceDto = new SourceDto();
			var res = await _sourceRepository.FindByKey(request.id);
			if(res.Success && res.Value != null && res != null)
			{
				sourceDto = res.Value.CopyToDto();
			} 
			return sourceDto;
		}
	}
}
