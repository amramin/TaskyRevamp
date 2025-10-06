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
	public record GetSourceConfigurationQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending) : IRequest<PagedResult<SourceDtoWithName>>;
	public class GetSourceConfigurationHandler : IRequestHandler<GetSourceConfigurationQuery, PagedResult<SourceDtoWithName>>
	{
		private readonly IRepository<Sources> _sourceRepository;
		public GetSourceConfigurationHandler(IRepository<Sources> sourceRepository)
		{
			_sourceRepository = sourceRepository;
		}
		public async Task<PagedResult<SourceDtoWithName>> Handle(GetSourceConfigurationQuery request, CancellationToken cancellationToken)
		{
			var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);
			var res = await _sourceRepository.GetPagedAsync(
								request.pageNumber,
								request.pageSize,
								null,
								orderBy: orderBy,
								includeProperties: $"{nameof(Sources.CreatedBy)},{nameof(Sources.UpdatedBy)}");

			var items = res.Items.Select(u => new SourceDtoWithName
			{
				Source = u.CopyToDto(),
				CreatedByName = u.CreatedBy != null ? u.CreatedBy.NameEnglish : string.Empty,
				UpdatedByName = u.UpdatedBy != null ? u.UpdatedBy.NameEnglish : string.Empty
			}).ToList();

			return new PagedResult<SourceDtoWithName>
			{
				Items = items,
				TotalCount = res.TotalCount,
				PageNumber = request.pageNumber,
				PageSize = request.pageSize
			};
		}

		private Func<IQueryable<Sources>, IOrderedQueryable<Sources>> GetOrderBy(string sortByColumn, bool sortAscending)
		{
			string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

			switch (sortByColumn)
			{
				case "CreateDate":
					return sortAscending
						? q => q.OrderBy(u => u.CreateDate)
						: q => q.OrderByDescending(u => u.CreateDate);

				case "UpdateDate":
					return sortAscending
						? q => q.OrderBy(u => u.UpdateDate)
						: q => q.OrderByDescending(u => u.UpdateDate);

				case "CreatedBy":
					return sortAscending
						? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
						: q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish);

				case "UpdatedBy":
					return sortAscending
						? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
						: q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);

				case "DisplayedName":
					if (currentCulture == "ar")
					{
						return sortAscending
							? q => q.OrderBy(u => u.NameArabic)
							: q => q.OrderByDescending(u => u.NameArabic);
					}
					else
					{
						return sortAscending
							? q => q.OrderBy(u => u.NameEnglish)
							: q => q.OrderByDescending(u => u.NameEnglish);
					}

				default:
					return q => q.OrderBy(u => u.CreateDate);
			}
		}
	}
}
