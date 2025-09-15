using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query
{
	public record GetPriorityByIdQuery(Guid Id): IRequest<PriorityDto>;
	public class GetPriorityByIdQueryHandler : IRequestHandler<GetPriorityByIdQuery, PriorityDto>
	{
		private readonly IRepository<PrioritySetting> _priorityRepository;
		public GetPriorityByIdQueryHandler(IRepository<PrioritySetting> priorityRepository)
		{
			this._priorityRepository = priorityRepository;
		}
		public async Task<PriorityDto> Handle(GetPriorityByIdQuery request, CancellationToken cancellationToken)
		{
			var priorityQuery = new PriorityDto();
			var res = await _priorityRepository.FindByKey(request.Id);
			if (res.Success && res != null && res.Value != null)
			{
				priorityQuery = res.Value.CopyToDto();
			}
			return priorityQuery;
		}
	}
}
