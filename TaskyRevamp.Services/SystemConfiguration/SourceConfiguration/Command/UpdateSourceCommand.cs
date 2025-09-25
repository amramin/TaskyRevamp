using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
	public record UpdateSourceCommand(SourceDto SourceDto) : IRequest<bool>;
	public class UpdateSourceHandler : IRequestHandler<UpdateSourceCommand, bool>
	{
		private readonly IRepository<Sources> _SourceRepository;
		public UpdateSourceHandler(IRepository<Sources> _sourceRepository)
		{
			_SourceRepository = _sourceRepository;
		}
		public async Task<bool> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
		{
			var res = await _SourceRepository.FindByKey(request.SourceDto.Id);
			if (res.Success && res != null && res.Value != null)
			{
				var sourceData = res.Value;
				var newData = request.SourceDto;

				sourceData.NameEnglish = newData.NameEnglish;
				sourceData.NameArabic = newData.NameArabic;
				sourceData.IsActive = newData.IsActive;

				await _SourceRepository.Update(sourceData);
				await _SourceRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
