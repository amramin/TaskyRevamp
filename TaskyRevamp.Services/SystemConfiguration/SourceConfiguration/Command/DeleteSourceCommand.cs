using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query;
using TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
    public record DeleteSourceCommand(Guid id) : IRequest<bool>;
    public class DeleteSourceHandler : IRequestHandler<DeleteSourceCommand, bool>
    {
        private readonly IRepository<Sources> _sourceRepository;
        private readonly IMediator _mediator;

        public DeleteSourceHandler(IRepository<Sources> sourceRepository, IMediator mediator)
        {
            _sourceRepository = sourceRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            var re = await _mediator.Send(new CheckRelatedTaskitemSourceQuery(request.id));
            if (re == true)
            {
                return false;
            }
            else
            {
                var res = await _sourceRepository.FindByKey(request.id);
                if (res.Success && res != null && res.Value != null)
                {
                    var source = res.Value;
                    source.IsDeleted = true;
                    await _sourceRepository.Update(source);
                }
            }
            return true;
        }
    }
}
