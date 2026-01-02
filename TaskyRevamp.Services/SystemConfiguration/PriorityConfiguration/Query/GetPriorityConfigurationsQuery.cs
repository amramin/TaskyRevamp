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
    public record GetPriorityQuery() : IRequest<List<PriorityDto>>;
    public class GetPriorityConfigurationsHandler : IRequestHandler<GetPriorityQuery, List<PriorityDto>>
    {
        private readonly IRepository<prioritySettings> _priorityRepository;
        public GetPriorityConfigurationsHandler(IRepository<prioritySettings> priorityRepository)
        {
            this._priorityRepository = priorityRepository;
        }
        public async Task<List<PriorityDto>> Handle(GetPriorityQuery request, CancellationToken cancellationToken)
        {
            var priorityDto = new List<PriorityDto>();
            var priortiyQuieriesResponse = await _priorityRepository.FindBy(k => !k.IsDeleted);
            if (priortiyQuieriesResponse.Success && priortiyQuieriesResponse.Value != null && priortiyQuieriesResponse.Value.Any())
            {
                priorityDto = priortiyQuieriesResponse.Value.Select(q => q.CopyToDto()).ToList();
            }
            return priorityDto;
        }
    }
}
