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
	public record DeletePriorityCommand(PriorityDto PriorityDto): IRequest<bool>;
	public class DeletePriorityHandler : IRequestHandler<DeletePriorityCommand, bool>
	{
		private readonly IRepository<PrioritySetting> _priorityRepository;

		public DeletePriorityHandler(IRepository<PrioritySetting> priorityRepository)
		{
			this._priorityRepository = priorityRepository;
		}
		public async Task<bool> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
		{
			var res = await _priorityRepository.FindByKey(request.PriorityDto.Id);
			if(res != null && res.Value != null && res.Success)
			{
				var priority = res.Value;
				await _priorityRepository.Delete(priority.Id);
				await _priorityRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
