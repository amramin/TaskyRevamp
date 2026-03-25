using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.Departments.Query;

public record GetDepartmentsQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDepartment> SearchFields, string SearchText) : IRequest<PagedResult<DepartmentDto>>;
public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, PagedResult<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<TaskItem> _taskRepository;
	private readonly string currentCulture;

	public GetDepartmentsHandler(IRepository<Department> departmentRepository, IRepository<TaskItem> taskRepository)
	{
		_departmentRepository = departmentRepository;
		currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		_taskRepository = taskRepository;
	}

	public async Task<PagedResult<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
		List<DepartmentDto> allSortedDepartments = new List<DepartmentDto>();
		//var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);
		Expression<Func<Department, bool>> searchExpression = null;
        if (request.SearchFields != null && request.SearchFields.Any())
        {
			var map = DepartmentSearchFieldDepartmentMap.Map(currentCulture);
			var predicates = request.SearchFields.Select(x => map[x]).ToList();
            searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
        }
        var allDepartments = await _departmentRepository.GetPagedAsync(1, int.MaxValue, null, searchExpression, orderBy: null,
                            includeProperties: $"{nameof(Department.CreatedBy)},{nameof(Department.UpdatedBy)}," +
								$"{nameof(Department.Parentdepartment)},{nameof(Department.AssignedUser)}");

		var deptList = allDepartments.Items.ToList();
		var allDeptLookup = deptList.ToDictionary(d => d.Id);
		// Reorder hierarchically based on sort column
		var reordered = ReorderHierarchically(deptList, request.sortByColumnName, request.sortAscending);
		var departments = reordered.Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize);

		foreach (var Department in departments)
        {
			//var res = await _taskRepository.GetAsync(t => t.DepartmentId == Department.Id);
			DepartmentDto dep = Department.CopyToDto();
            dep.CreatedByName = currentCulture == "ar"? Department.CreatedBy?.NameArabic : Department.CreatedBy?.NameEnglish;
            dep.UpdatedByName = currentCulture == "ar" ? Department.UpdatedBy?.NameArabic : Department.UpdatedBy?.NameEnglish;
			dep.SubDepartmentUsers = allDeptLookup.Values.Count(d => d.ParentdepartmentId == Department.Id);
			allSortedDepartments.Add(dep);
        }
        return new PagedResult<DepartmentDto>
        {
            Items = allSortedDepartments,
            TotalCount = allDepartments.TotalCount,
            PageNumber = request.pageNumber,
            PageSize = request.pageSize
        };

	}
	private List<Department> ReorderHierarchically(IEnumerable<Department> departments, string sortByColumn, bool sortAscending)
	{
		var deptList = departments.ToList();
		var result = new List<Department>();
		var lookup = deptList.ToDictionary(d => d.Id);
		var root = deptList.FirstOrDefault(d => d.ParentdepartmentId == null); // root

		if (root == null)
			return deptList;

		AddDepartmentWithChildren(root, lookup, result, sortByColumn, sortAscending);
		return result;
	}
	private void AddDepartmentWithChildren(Department dept, Dictionary<Guid, Department> lookup, List<Department> result, string sortByColumn, bool sortAscending)
	{
		result.Add(dept);
		var children = lookup.Values.Where(d => d.ParentdepartmentId == dept.Id);
		children = SortDepartments(children, sortByColumn, sortAscending);
		foreach (var child in children)
		{
			AddDepartmentWithChildren(child, lookup, result, sortByColumn, sortAscending);
		}
	}
	private IEnumerable<Department> SortDepartments(IEnumerable<Department> departments, string sortByColumn, bool sortAscending)
	{
		switch (sortByColumn)
		{
			case "CreateDate":
				return sortAscending
					? departments.OrderBy(d => d.CreateDate)
					: departments.OrderByDescending(d => d.CreateDate);

			case "DisplayedName":
				if (currentCulture == "ar")
				{
					return sortAscending
						? departments.OrderBy(d => d.NameArabic)
						: departments.OrderByDescending(d => d.NameArabic);
				}
				else
				{
					return sortAscending
						? departments.OrderBy(d => d.NameEnglish)
						: departments.OrderByDescending(d => d.NameEnglish);
				}

			case "UpdateDate":
				return sortAscending
					? departments.OrderBy(d => d.UpdateDate)
					: departments.OrderByDescending(d => d.UpdateDate);

			case "CreatedBy":
				if (currentCulture == "ar")
				{
					return sortAscending
						? departments.OrderBy(d => d.CreatedBy?.NameArabic)
						: departments.OrderByDescending(d => d.CreatedBy?.NameArabic);
				}
				else
				{
					return sortAscending
						? departments.OrderBy(d => d.CreatedBy?.NameEnglish)
						: departments.OrderByDescending(d => d.CreatedBy?.NameEnglish);
				}

			case "UpdatedBy":
				if (currentCulture == "ar")
				{
					return sortAscending
						? departments.OrderBy(d => d.UpdatedBy?.NameArabic)
						: departments.OrderByDescending(d => d.UpdatedBy?.NameArabic);
				}
				else
				{
					return sortAscending
						? departments.OrderBy(d => d.UpdatedBy?.NameEnglish)
						: departments.OrderByDescending(d => d.UpdatedBy?.NameEnglish);
				}

			case "DepartmentParent":
				if (currentCulture == "ar")
				{
					return sortAscending
						? departments.OrderBy(d => d.Parentdepartment?.NameArabic)
						: departments.OrderByDescending(d => d.Parentdepartment?.NameArabic);
				}
				else
				{
					return sortAscending
						? departments.OrderBy(d => d.Parentdepartment?.NameEnglish)
						: departments.OrderByDescending(d => d.Parentdepartment?.NameEnglish);
				}

			case "Level":
				return sortAscending
					? departments.OrderBy(d => d.Level)
					: departments.OrderByDescending(d => d.Level);

			default:
				return sortAscending
					? departments.OrderBy(d => d.CreateDate)
					: departments.OrderByDescending(d => d.CreateDate);
		}
	}
}