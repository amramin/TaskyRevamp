using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query;
using TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
    public record DeleteTypeCommand(Guid id) : IRequest<bool>;
    public class DeleteTypeHandler : IRequestHandler<DeleteTypeCommand, bool>
    {
        private readonly IRepository<Types> _typeRepository;
        private readonly IMediator _mediator;

        public DeleteTypeHandler(IRepository<Types> typeRepository, IMediator mediator)
        {
            _typeRepository = typeRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
        {
            var re = await _mediator.Send(new CheckRelatedTaskitemTypeQuery(request.id));
            if (re == true)
            {
                return false;
            }
            else
            {
                var res = await _typeRepository.FindByKey(request.id);
                if (res.Success && res != null && res.Value != null)
                {
                    var type = res.Value;
                    type.IsDeleted = true;
                    await _typeRepository.Update(type);
                    //await _typeRepository.Delete(type.Id);
                }
            }
            return true;
        }
    }
}
