using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.Users.Query
{
	public record GetAllUsersByPrivilegeQuery(Guid PrivilegeId, int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldUserPrivilege> SearchFields, string SearchText) : IRequest<PagedResult<UserDtoWithName>>;
	public class GetAllUsersByPrivilegeHandler : IRequestHandler<GetAllUsersByPrivilegeQuery, PagedResult<UserDtoWithName>>
	{
		private readonly IRepository<User> _userRepository;
		string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		public GetAllUsersByPrivilegeHandler(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}
		public async Task<PagedResult<UserDtoWithName>> Handle(GetAllUsersByPrivilegeQuery request, CancellationToken cancellationToken)
		{
			var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

			Expression<Func<User, bool>> searchExpression = u => u.PrivilegeId == request.PrivilegeId;
			if (request.SearchFields != null && request.SearchFields.Any() && !string.IsNullOrWhiteSpace(request.SearchText))
			{
				var map = UserPrivilegeSearchFieldMap.Map(currentCulture);
				var predicates = request.SearchFields.Select(x => map[x]).ToList();
				var likeFilter = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
				searchExpression = searchExpression.And(likeFilter);
			}

			var res = await _userRepository.GetPagedAsync(
								request.pageNumber,
								request.pageSize,
								null,
								searchExpression,
								orderBy: orderBy,
								includeProperties: $"{nameof(User.Privilege)},{nameof(User.Department)}");

			var items = res.Items.Select(u => new UserDtoWithName
			{
				user = u.CopyToDto(),
				PrivilegeName = u.Privilege != null ? (currentCulture == "ar" ? u.Privilege.NameArabic : u.Privilege.NameEnglish) : string.Empty,
				DepartmentName = u.Department != null ? (currentCulture == "ar" ? u.Department.NameArabic : u.Department.NameEnglish) : string.Empty

			}).ToList();

			return new PagedResult<UserDtoWithName>
			{
				Items = items,
				TotalCount = res.TotalCount,
				PageNumber = request.pageNumber,
				PageSize = request.pageSize
			};
		}

		private Func<IQueryable<User>, IOrderedQueryable<User>> GetOrderBy(string sortByColumn, bool sortAscending)
		{
			switch (sortByColumn)
			{
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

				case "Email":
					return sortAscending
						? q => q.OrderBy(u => u.Email)
						: q => q.OrderByDescending(u => u.Email);

				case "Privilege":
					if (currentCulture == "ar")
					{
						return sortAscending
						? q => q.OrderBy(u => u.Privilege!.NameArabic)
						: q => q.OrderByDescending(u => u.Privilege!.NameArabic);
					}
					else
					{
						return sortAscending
						? q => q.OrderBy(u => u.Privilege!.NameEnglish)
						: q => q.OrderByDescending(u => u.Privilege!.NameEnglish);
					}

				case "Department":
					if (currentCulture == "ar")
					{
						return sortAscending
						? q => q.OrderBy(u => u.Department!.NameArabic)
						: q => q.OrderByDescending(u => u.Department!.NameArabic);
					}
					else
					{
						return sortAscending
						? q => q.OrderBy(u => u.Department!.NameEnglish)
						: q => q.OrderByDescending(u => u.Department!.NameEnglish);
					}

				default:
					return q => q.OrderBy(u => u.CreateDate);
			}
		}

	}
}
