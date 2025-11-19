using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
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
	public record GetAllUsersByDepartmentWithPaginationQuery(Guid departmentId, int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldUserDepartment> SearchFields, string SearchText) : IRequest<PagedResult<DepartmentDto>>;
	public class GetAllUsersByDepartmentWithPaginationHandler : IRequestHandler<GetAllUsersByDepartmentWithPaginationQuery, PagedResult<DepartmentDto>>
	{
		private readonly IRepository<Department> _departmentRepository;
		private readonly IRepository<User> _userRepository;
		string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		public GetAllUsersByDepartmentWithPaginationHandler(IRepository<Department> departmentRepository, IRepository<User> userRepository)
		{
			_departmentRepository = departmentRepository;
			_userRepository = userRepository;
		}
		public async Task<PagedResult<DepartmentDto>> Handle(GetAllUsersByDepartmentWithPaginationQuery request, CancellationToken cancellationToken)
		{
			DepartmentDto departmentDto = new DepartmentDto();
			var departmentData = await _departmentRepository.FindBy(d => d.Id == request.departmentId);
			if (departmentData.Success && departmentData.Value != null)
			{
				var department = departmentData.Value.FirstOrDefault();
				if (department != null)
					departmentDto = department.CopyToDto();
			}
			var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

			Expression<Func<User, bool>> filter = u => u.DepartmentId == request.departmentId;
			if (request.SearchFields != null && request.SearchFields.Any() && !string.IsNullOrWhiteSpace(request.SearchText))
			{
				var predicates = request.SearchFields.Select(f => UserDepartmentSearchFieldMap.Map[f]).ToList();
				var searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
				filter = filter.AndAlso(searchExpression);
			}
			var usersPaged = await _userRepository.GetPagedAsync(
				request.pageNumber,
				request.pageSize,
				null,
				filter,
				orderBy,
				includeProperties: $"{nameof(User.Privilege)},{nameof(User.Department)}"
			);

			departmentDto.AssignedUsers = usersPaged.Items.Select(u => new UserDto
			{
				Id = u.Id,
				userNameAR = u.NameArabic,
				userNameEN = u.NameEnglish,
				Email = u.Email,
				PrivilegName = u.Privilege?.NameEnglish
			}).ToList();

			return new PagedResult<DepartmentDto>
			{
				Items = new List<DepartmentDto> { departmentDto },
				TotalCount = usersPaged.TotalCount,
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
				default:
					return q => q.OrderBy(u => u.CreateDate);
			}
		}
	}
}

