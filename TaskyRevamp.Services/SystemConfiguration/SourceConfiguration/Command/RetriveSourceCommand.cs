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
using TaskyRevamp.Localization.Resources;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
    public record RetriveSourceCommand(SourceDto SourceDto) : IRequest<bool>;
    public class RetriveSourceHandler : IRequestHandler<RetriveSourceCommand, bool>
    {
        private readonly IRepository<Sources> _sourceRepository;
        public RetriveSourceHandler(IRepository<Sources> sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }
        public async Task<bool> Handle(RetriveSourceCommand request, CancellationToken cancellationToken)
        {
            var res = await _sourceRepository.FindBy(k => k.NameArabic == request.SourceDto.NameArabic || k.NameEnglish == request.SourceDto.NameEnglish);
            if (res.Success && res != null && res.Value != null)
            {
                var sourceData = res.Value.FirstOrDefault();
                sourceData.IsDeleted = false;


                await _sourceRepository.Update(sourceData);
                //await _sourceRepository.SaveChangesAsync();
            }
            return true;
        }
    }
}
