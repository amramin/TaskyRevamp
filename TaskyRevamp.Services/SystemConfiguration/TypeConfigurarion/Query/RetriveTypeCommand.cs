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
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfiguration.Command
{
    public record RetriveTypeCommand(TypeDto TypeDto) : IRequest<bool>;
    public class RetriveTypeHandler : IRequestHandler<RetriveTypeCommand, bool>
    {
        private readonly IRepository<Types> _TypeRepository;
        public RetriveTypeHandler(IRepository<Types> TypeRepository)
        {
            _TypeRepository = TypeRepository;
        }
        public async Task<bool> Handle(RetriveTypeCommand request, CancellationToken cancellationToken)
        {
            var res = await _TypeRepository.FindBy(k => (k.NameArabic == request.TypeDto.NameArabic || k.NameEnglish == request.TypeDto.NameEnglish) && (k.Id != request.TypeDto.Id));
            if (res.Success && res != null && res.Value != null)
            {
                var TypeData = res.Value.FirstOrDefault();
                TypeData.IsDeleted = false;


                await _TypeRepository.Update(TypeData);
                //await _TypeRepository.SaveChangesAsync();
            }
            return true;
        }
    }
}
