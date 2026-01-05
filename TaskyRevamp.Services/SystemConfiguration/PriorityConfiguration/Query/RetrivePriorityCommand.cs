using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Prioritys = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
    public record RetrivePriorityCommand(PriorityDto PriorityDto) : IRequest<bool>;
    public class RetrivePriorityHandler : IRequestHandler<RetrivePriorityCommand, bool>
    {
        private readonly IRepository<Prioritys> _PriorityRepository;
        public RetrivePriorityHandler(IRepository<Prioritys> PriorityRepository)
        {
            _PriorityRepository = PriorityRepository;
        }
        public async Task<bool> Handle(RetrivePriorityCommand request, CancellationToken cancellationToken)
        {
            var res = await _PriorityRepository.FindBy(k => k.NameArabic == request.PriorityDto.NameArabic || k.NameEnglish == request.PriorityDto.NameEnglish);
            if (res.Success && res != null && res.Value != null)
            {
                var PriorityData = res.Value.FirstOrDefault();
                PriorityData.IsDeleted = false;


                await _PriorityRepository.Update(PriorityData);
                //await _PriorityRepository.SaveChangesAsync();
            }
            return true;
        }
    }
}
