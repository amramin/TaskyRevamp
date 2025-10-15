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
using Source = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
	public record CreateSourceCommand(SourceDto SourceDto) : IRequest<bool>;
	public class CreateSourceHandler : IRequestHandler<CreateSourceCommand, bool>
	{
		private readonly IRepository<Source> _sourceRepository;
		public CreateSourceHandler(IRepository<Source> sourceRepository)
		{
			_sourceRepository = sourceRepository; 
		}
		public async Task<bool> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
		{
			var source = new Source
			{
				NameEnglish = request.SourceDto.NameEnglish,
				NameArabic = request.SourceDto.NameArabic,
				IsActive = request.SourceDto.IsActive,
			};
			await _sourceRepository.Insert(source);
			return true;
		}
	}
}
