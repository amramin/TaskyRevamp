using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query
{
	public record GetTypeByIdQuery(Guid id) : IRequest<TypeDto>;
	public class GetTypeByIdHandler : IRequestHandler<GetTypeByIdQuery, TypeDto>
	{
		private readonly IRepository<Types> _typeRepository;
		public GetTypeByIdHandler(IRepository<Types> typeRepository)
		{
			_typeRepository = typeRepository;
		}
		public async Task<TypeDto> Handle(GetTypeByIdQuery request, CancellationToken cancellationToken)
		{
			TypeDto typeDto = new TypeDto();
			var res = await _typeRepository.FindByKey(request.id);
			if (res.Success && res.Value != null && res != null)
			{
				typeDto = res.Value.CopyToDto();
			}
			return typeDto;
		}
	}
}
