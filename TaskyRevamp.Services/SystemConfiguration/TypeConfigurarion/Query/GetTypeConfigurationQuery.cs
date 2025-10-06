using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query
{
	public record GetTypeConfigurationQuery(int PageNumber, int pageSize, string sortByColumn, bool sortAscending) : IRequest<PagedResult<TypeDtoWithName>>;
	public class GetTypeConfigurationHandler : IRequestHandler<GetTypeConfigurationQuery, PagedResult<TypeDtoWithName>>
	{
		private readonly IRepository<Types> _typeRepository;
		public GetTypeConfigurationHandler(IRepository<Types> typeRepository)
		{
			_typeRepository = typeRepository;
		}
		public async Task<PagedResult<TypeDtoWithName>> Handle(GetTypeConfigurationQuery request, CancellationToken cancellationToken)
		{
			var orderBy = GetOrderBy(request.sortByColumn, request.sortAscending);
			var res = await _typeRepository.GetPagedAsync(
								request.PageNumber,
								request.pageSize,
								null,
								orderBy: orderBy,
								includeProperties: $"{nameof(Types.CreatedBy)},{nameof(Types.UpdatedBy)}");

			var items = res.Items.Select(u => new TypeDtoWithName
			{
				Type = u.CopyToDto(),
				CreatedByName = u.CreatedBy != null ? u.CreatedBy.NameEnglish! : string.Empty,
				UpdatedByName = u.UpdatedBy != null ? u.UpdatedBy.NameEnglish! : string.Empty
			}).ToList();

			return new PagedResult<TypeDtoWithName>
			{
				Items = items,
				TotalCount = res.TotalCount,
				PageNumber = request.PageNumber,
				PageSize = request.pageSize
			};
		}
		private Func<IQueryable<Types>, IOrderedQueryable<Types>> GetOrderBy(string sortByColumn, bool sortAscending)
		{
			string currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			switch (sortByColumn)
			{
				case "CreateDate":
					return sortAscending? q => q.OrderBy(u => u.CreateDate) : q => q.OrderByDescending(u => u.CreateDate);

				case "UpdateDate":
					return sortAscending? q => q.OrderBy(u => u.UpdateDate): q => q.OrderByDescending(u => u.UpdateDate);

				case "CreatedBy":
					return sortAscending? q => q.OrderBy(u => u.CreatedBy.NameEnglish) : q => q.OrderByDescending(u => u.CreatedBy.NameEnglish);

				case "UpdatedBy":
					return sortAscending? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish): q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);

				case "DisplayedName":
					if(currentLanguage == "ar")
					{
						return sortAscending? q => q.OrderBy(u => u.NameArabic): q => q.OrderByDescending(u => u.NameArabic);
					}
					else
					{
						return sortAscending ? q => q.OrderBy(u => u.NameEnglish) : q => q.OrderByDescending(u => u.NameEnglish);
					}

				default:
					return q => q.OrderBy(u => u.CreateDate);
			}

		}
	}
}
