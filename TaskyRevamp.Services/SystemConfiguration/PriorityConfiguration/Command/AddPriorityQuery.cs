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
	public record AddPriorityQuery(PriorityDto PriorityDto):IRequest<bool>;
	public class AddPriorityQueryHandler : IRequestHandler<AddPriorityQuery, bool>
	{
		private readonly IRepository<PrioritySetting> _PriorityRepositry;

		public AddPriorityQueryHandler(IRepository<PrioritySetting> _priorityRepositry)
		{
			_PriorityRepositry = _priorityRepositry;
		}
		public async Task<bool> Handle(AddPriorityQuery request, CancellationToken cancellationToken)
		{
			PrioritySetting priority = new PrioritySetting(
				request.PriorityDto.NameEnglish,
				request.PriorityDto.NameArabic,
				request.PriorityDto.NameColor,
				request.PriorityDto.BackgroundColor,
				request.PriorityDto.Order
			); 
			await _PriorityRepositry.Insert(priority);
			await _PriorityRepositry.SaveChangesAsync();
			return true;
		}
	}
}
