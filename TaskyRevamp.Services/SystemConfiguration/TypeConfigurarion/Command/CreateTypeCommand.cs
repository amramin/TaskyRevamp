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
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
    public record CreateTypeCommand(TypeDto TypeDto) : IRequest<bool>;
    public class CreateTypeHandler : IRequestHandler<CreateTypeCommand, bool>
    {
        private readonly IRepository<Types> _typeRepository;
        public CreateTypeHandler(IRepository<Types> typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public async Task<bool> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
        {
            var type = new Types
            {
                NameEnglish = request.TypeDto.NameEnglish,
                NameArabic = request.TypeDto.NameArabic,
                IsActive = request.TypeDto.IsActive,
                CreatedById = request.TypeDto.CreatedById
            };
            await _typeRepository.Insert(type);
            //await _typeRepository.SaveChangesAsync();
            return true;
        }
    }
}
