using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
	public record DeleteTypeCommand(Guid id) : IRequest<bool>;
	public class DeleteTypeHandler : IRequestHandler<DeleteTypeCommand, bool>
	{
		private readonly IRepository<Types> _typeRepository;
		public DeleteTypeHandler(IRepository<Types> typeRepository)
		{
			_typeRepository = typeRepository;
		}
		public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
		{
			var res = await _typeRepository.FindByKey(request.id);
			if (res.Success && res != null && res.Value != null)
			{
				var type = res.Value;
				await _typeRepository.Delete(type.Id);
			}
			return true;
		}
	}
}
