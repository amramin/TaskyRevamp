using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using User = TaskyRevamp.Domain.Models.Users.User;

namespace TaskyRevamp.Services.Users.Query
{
	public record GetUsersWithPaginationQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldUser> SearchFields, string SearchText) : IRequest<PagedResult<UserDtoWithName>>;
	public class GetUsersWithPaginationHandler : IRequestHandler<GetUsersWithPaginationQuery, PagedResult<UserDtoWithName>>
	{
		private readonly IRepository<User> _userRepository;
		string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		public GetUsersWithPaginationHandler(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}
		public async Task<PagedResult<UserDtoWithName>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
		{
			var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

			Expression<Func<User, bool>> searchExpression = null;
			if (request.SearchFields != null && request.SearchFields.Any())
			{
				var predicates = request.SearchFields.Select(x => UserSearchFieldMap.Map[x]).ToList();
				searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
			}

			var res = await _userRepository.GetPagedAsync(
								request.pageNumber,
								request.pageSize,
								u => u.IsDeleted == false,
								searchExpression,
								orderBy: orderBy,
								includeProperties: $"{nameof(User.UpdatedBy)},{nameof(User.Privilege)},{nameof(User.Department)}");

			var items = res.Items.Select(u => new UserDtoWithName
			{
				user = u.CopyToDto(),
				UpdatedByName = u.UpdatedBy != null ? (currentCulture == "ar" ? u.UpdatedBy.NameArabic : u.UpdatedBy.NameEnglish) : string.Empty,
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
				case "CreateDate":
					return sortAscending
						? q => q.OrderBy(u => u.CreateDate)
						: q => q.OrderByDescending(u => u.CreateDate);
                case "Set as Manager":
                    return sortAscending
                        ? q => q.OrderBy(u => u.IsManager)
                        : q => q.OrderByDescending(u => u.IsManager);
                case "UpdateDate":
					return sortAscending
						? q => q.OrderBy(u => u.UpdateDate)
						: q => q.OrderByDescending(u => u.UpdateDate);

				case "UpdatedBy":
					if(currentCulture == "ar")
					{
						return sortAscending
						? q => q.OrderBy(u => u.UpdatedBy!.NameArabic)
						: q => q.OrderByDescending(u => u.UpdatedBy!.NameArabic);
					}
					else
						return sortAscending
						? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
						: q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);

				case "Name":
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

				//case "IsManager":
				//	return sortAscending
				//		? q => q.OrderBy(u => u.IsManager)
				//		: q => q.OrderByDescending(u => u.IsManager);

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
                case "Privileges":
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
                default:
					return q => q.OrderBy(u => u.CreateDate);
			}
		}
	}
}
