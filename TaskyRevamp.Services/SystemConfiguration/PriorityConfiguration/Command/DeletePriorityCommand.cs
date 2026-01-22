using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
    public record DeletePriorityCommand(PriorityDto PriorityDto) : IRequest<bool>;
    public class DeletePriorityHandler : IRequestHandler<DeletePriorityCommand, bool>
    {
        private readonly IRepository<PrioritySetting> _priorityRepository;
        private readonly IMediator _mediator;

        public DeletePriorityHandler(IRepository<PrioritySetting> priorityRepository, IMediator mediator)
        {
            this._priorityRepository = priorityRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
        {
            var re = await _mediator.Send(new CheckRelatedTaskitemQuery(request.PriorityDto.Id));
            if (re == true)
            {
                return false;
            }
            else
            {
                var hascompleted = await _mediator.Send(new CheckPriorityRelatedCompletedTaskQuery(request.PriorityDto.Id));
                if (hascompleted == true)
                {
                    var res = await _priorityRepository.FindByKey(request.PriorityDto.Id);
                    if (res != null && res.Value != null && res.Success)
                    {
                        var priority = res.Value;
                        priority.IsDeleted = true;
                        await _priorityRepository.Update(priority);
                        //	await _priorityRepository.Delete(priority.Id);
                    }
                }
                else
                {
                    await _priorityRepository.Delete(request.PriorityDto.Id);
                }
            }
            return true;
        }
    }
}
