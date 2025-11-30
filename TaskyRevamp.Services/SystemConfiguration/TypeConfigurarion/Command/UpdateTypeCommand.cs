using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
	public record UpdateTypeCommand(TypeDto TypeDto) : IRequest<bool>;
	public class UpdateTypeHandler : IRequestHandler<UpdateTypeCommand, bool>
	{
		private readonly IRepository<Types> _typeRepository;
		public UpdateTypeHandler(IRepository<Types> typeRepository)
		{
			_typeRepository = typeRepository;
		}
		public async Task<bool> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
		{
			var res = await _typeRepository.FindByKey(request.TypeDto.Id);
			if (res.Success && res != null && res.Value != null)
			{
				var typeData = res.Value;
				var newData = request.TypeDto;

				typeData.NameEnglish = newData.NameEnglish;
				typeData.NameArabic = newData.NameArabic;
				typeData.IsActive = newData.IsActive;
				typeData.UpdatedById = newData.UpdatedById;
				await _typeRepository.Update(typeData);
				//await _typeRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
