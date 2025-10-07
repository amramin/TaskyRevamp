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
		private readonly IRepository<Sources> _sourceRepository;
		public DeleteSourceHandler(IRepository<Sources> sourceRepository)
		{
			_sourceRepository = sourceRepository;
		}
		public async Task<bool> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
		{
			var res = await _sourceRepository.FindByKey(request.id);
			if(res.Success && res != null && res.Value != null)
			{
				var source = res.Value;
				await _sourceRepository.Delete(source.Id);
			}
			return true;
		}
	}
}
