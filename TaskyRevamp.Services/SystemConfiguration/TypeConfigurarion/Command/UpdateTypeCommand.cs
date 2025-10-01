using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
	public record UpdateTypeCommand(TypeDto TypeDto) : IRequest<bool>;
	public class UpdateTypeHandler : IRequestHandler<UpdateTypeCommand, bool>
	{
		private readonly IRepository<Types> _TypeRepository;
		public UpdateTypeHandler(IRepository<Types> _typeRepository)
		{
			_TypeRepository = _typeRepository;
		}
		public async Task<bool> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
		{
			var res = await _TypeRepository.FindByKey(request.TypeDto.Id);
			if (res.Success && res != null && res.Value != null)
			{
				var typeData = res.Value;
				var newData = request.TypeDto;

				typeData.NameEnglish = newData.NameEnglish;
				typeData.NameArabic = newData.NameArabic;
				typeData.IsActive = newData.IsActive;

				await _TypeRepository.Update(typeData);
				await _TypeRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
