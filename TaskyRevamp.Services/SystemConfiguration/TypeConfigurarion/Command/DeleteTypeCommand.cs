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
		private readonly IRepository<Types> _TypeRepository;
		public DeleteTypeHandler(IRepository<Types> _typeRepository)
		{
			_TypeRepository = _typeRepository;
		}
		public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
		{
			var res = await _TypeRepository.FindByKey(request.id);
			if (res.Success && res != null && res.Value != null)
			{
				var type = res.Value;
				await _TypeRepository.Delete(type.Id);
			}
			return true;
		}
	}
}
