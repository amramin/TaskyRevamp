using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
	public record DeleteSourceCommand(Guid id) : IRequest<bool>;
	public class DeleteSourceHandler : IRequestHandler<DeleteSourceCommand, bool>
	{
		private readonly IRepository<Sources> _SourceRepository;
		public DeleteSourceHandler(IRepository<Sources> _sourceRepository)
		{
			_SourceRepository = _sourceRepository;
		}
		public async Task<bool> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
		{
			var res = await _SourceRepository.FindByKey(request.id);
			if(res.Success && res != null && res.Value != null)
			{
				var source = res.Value;
				await _SourceRepository.Delete(source.Id);
			}
			return true;
		}
	}
}
