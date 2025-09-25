using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query
{
	public record GetSourceConfigurationQuery(int pageNumber, int pageSize) : IRequest<PagedResult<SourceDtoWithName>>;
	public class GetSourceConfigurationHandler : IRequestHandler<GetSourceConfigurationQuery, PagedResult<SourceDtoWithName>>
	{
		private readonly IRepository<Sources> _SourceRepository;
		public GetSourceConfigurationHandler(IRepository<Sources> _sourceRepository)
		{
			_SourceRepository = _sourceRepository;
		}
		public async Task<PagedResult<SourceDtoWithName>> Handle(GetSourceConfigurationQuery request, CancellationToken cancellationToken)
		{
			List<SourceDtoWithName> sourceDtos = new List<SourceDtoWithName>();
			var res = await _SourceRepository.GetPagedAsync(
								request.pageNumber,
								request.pageSize,
								null,
								orderBy: q => q.OrderBy(u => u.CreateDate),
								includeProperties: $"{nameof(Sources.CreatedBy)},{nameof(Sources.UpdatedBy)}");
			
			var items =  res.Items.Select(u => new SourceDtoWithName
			{
				Source = u.CopyToDto(),
				CreatedByName = u.CreatedBy != null ? u.CreatedBy.NameEnglish : string.Empty,
				UpdatedByName = u.UpdatedBy != null ? u.UpdatedBy.NameEnglish : string.Empty
			}).ToList();

			return new PagedResult<SourceDtoWithName> {
				Items = items,
				TotalCount = res.TotalCount,
				PageNumber = request.pageNumber,
				PageSize = request.pageSize
			};
		}
	}
}
