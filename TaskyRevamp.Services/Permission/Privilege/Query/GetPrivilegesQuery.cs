using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Query
{
	public record GetPrivilegesQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchField> SearchFields, string SearchText) : IRequest<PagedResult<PrivilegeDtoWithName>>;
	public class GetPrivilegesHandler : IRequestHandler<GetPrivilegesQuery, PagedResult<PrivilegeDtoWithName>>
	{
		private readonly IRepository<Privileges> _privilegesRepository;
		public GetPrivilegesHandler(IRepository<Privileges> privilegesRepository)
		{
			_privilegesRepository = privilegesRepository;
		}

		public async Task<PagedResult<PrivilegeDtoWithName>> Handle(GetPrivilegesQuery request, CancellationToken cancellationToken)
		{
			var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

			Expression<Func<Privileges, bool>> searchExpression = null;
			if (request.SearchFields != null && request.SearchFields.Any())
			{
				var predicates = request.SearchFields.Select(x => PrivilegeSearchFieldMap.Map[x]).ToList();
				searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
			}

			var res = await _privilegesRepository.GetPagedAsync(
								request.pageNumber,
								request.pageSize,
								null,
								searchExpression,
								orderBy: orderBy,
								includeProperties: $"{nameof(Privileges.CreatedBy)},{nameof(Privileges.UpdatedBy)}");

			var items = res.Items.Select(u => new PrivilegeDtoWithName
			{
				PrivilegeDto = u.CopyToDto(),
				CreatedByName = u.CreatedBy != null ? u.CreatedBy.NameEnglish : string.Empty,
				UpdatedByName = u.UpdatedBy != null ? u.UpdatedBy.NameEnglish : string.Empty
			}).ToList();

			return new PagedResult<PrivilegeDtoWithName>
			{
				Items = items,
				TotalCount = res.TotalCount,
				PageNumber = request.pageNumber,
				PageSize = request.pageSize
			};
		}
		private Func<IQueryable<Privileges>, IOrderedQueryable<Privileges>> GetOrderBy(string sortByColumn, bool sortAscending)
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
