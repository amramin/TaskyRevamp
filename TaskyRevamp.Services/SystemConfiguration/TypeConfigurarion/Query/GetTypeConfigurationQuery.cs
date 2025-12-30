using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query
{
	public record GetTypeConfigurationQuery(int PageNumber, int pageSize, string sortByColumn, bool sortAscending, List<SearchField> SearchFields, string SearchText) : IRequest<PagedResult<TypeDtoWithName>>;
	public class GetTypeConfigurationHandler : IRequestHandler<GetTypeConfigurationQuery, PagedResult<TypeDtoWithName>>
	{
		private readonly IRepository<Types> _typeRepository;
		private readonly string currentLanguage; 
        public GetTypeConfigurationHandler(IRepository<Types> typeRepository)
		{
			_typeRepository = typeRepository;
            currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
		public async Task<PagedResult<TypeDtoWithName>> Handle(GetTypeConfigurationQuery request, CancellationToken cancellationToken)
		{
			var orderBy = GetOrderBy(request.sortByColumn, request.sortAscending);

            Expression<Func<Types, bool>> searchExpression = null;
            if (request.SearchFields != null && request.SearchFields.Any())
            {
                var predicates = request.SearchFields.Select(x => TaskTypeSearchFieldMap.Map[x]).ToList();
                searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
            }

            var res = await _typeRepository.GetPagedAsync(
								request.PageNumber,
								request.pageSize,
								null,
								searchExpression,
								orderBy: orderBy,
								includeProperties: $"{nameof(Types.CreatedBy)},{nameof(Types.UpdatedBy)}");

			var items = res.Items.Select(u => new TypeDtoWithName
			{
				Type = u.CopyToDto(),
				CreatedByName = u.CreatedBy != null ? (currentLanguage == "ar" ? u.CreatedBy.NameArabic! : u.CreatedBy.NameEnglish!) : string.Empty,
				UpdatedByName = u.UpdatedBy != null ? (currentLanguage == "ar" ? u.UpdatedBy.NameArabic! : u.UpdatedBy.NameEnglish!) : string.Empty,
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
			switch (sortByColumn)
			{
				case "CreateDate":
					return sortAscending? q => q.OrderBy(u => u.CreateDate) : q => q.OrderByDescending(u => u.CreateDate);

				case "UpdateDate":
					return sortAscending? q => q.OrderBy(u => u.UpdateDate): q => q.OrderByDescending(u => u.UpdateDate);

				case "CreatedBy":
					if (currentLanguage == "ar")
					{
                        return sortAscending ? q => q.OrderBy(u => u.CreatedBy.NameArabic) : q => q.OrderByDescending(u => u.CreatedBy.NameArabic);
					}
					else
					{
                        return sortAscending ? q => q.OrderBy(u => u.CreatedBy.NameEnglish) : q => q.OrderByDescending(u => u.CreatedBy.NameEnglish);
                    }

				case "UpdatedBy":
					if (currentLanguage == "ar")
					{
                        return sortAscending ? q => q.OrderBy(u => u.UpdatedBy!.NameArabic) : q => q.OrderByDescending(u => u.UpdatedBy!.NameArabic);
					}
					else
					{
                        return sortAscending ? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish) : q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);
                    }

				case "DisplayedName":
					if (currentLanguage == "ar")
					{
						return sortAscending ? q => q.OrderBy(u => u.NameArabic) : q => q.OrderByDescending(u => u.NameArabic);
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
