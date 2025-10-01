using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
	public record CreateTypeCommand(TypeDto TypeDto) : IRequest<bool>;
	public class CreateTypeHandler : IRequestHandler<CreateTypeCommand, bool>
	{
		private readonly IRepository<Types> _TypeRepository;
		public CreateTypeHandler(IRepository<Types> _typeRepository)
		{
			_TypeRepository = _typeRepository;
		}
		public async Task<bool> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
		{
			var type = new Types
			{
				NameEnglish = request.TypeDto.NameEnglish,
				NameArabic = request.TypeDto.NameArabic,
				IsActive = request.TypeDto.IsActive,
			};
			await _TypeRepository.Insert(type);
			await _TypeRepository.SaveChangesAsync();
			return true;
		}
	}
}
