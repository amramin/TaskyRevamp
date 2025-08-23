using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
	public record DeletePriorityQuery(PriorityDto PriorityDto): IRequest<bool>;
	public class DeletePriorityQueryHandler : IRequestHandler<DeletePriorityQuery, bool>
	{
		private readonly IRepository<PrioritySetting> _PriorityRepository;

		public DeletePriorityQueryHandler(IRepository<PrioritySetting> _priorityRepository)
		{
			_PriorityRepository = _priorityRepository;
		}
		public async Task<bool> Handle(DeletePriorityQuery request, CancellationToken cancellationToken)
		{
			var res = await _PriorityRepository.FindByKey(request.PriorityDto.Id);
			if(res != null && res.Value != null && res.Success)
			{
				var priority = res.Value;
				await _PriorityRepository.Delete(priority.Id);
				await _PriorityRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
