using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskyRevamp.Services.Departments.Query;

public record GetDepartmentsNoPagnationQuery(List<SearchFieldDepartment> SearchFields, string SearchText) : IRequest<List<DepartmentDto>>;

public class GetDepartmentsNoPagnationHandler : IRequestHandler<GetDepartmentsNoPagnationQuery, List<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;
    private string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
	public GetDepartmentsNoPagnationHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsNoPagnationQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDto> allDepartments = new List<DepartmentDto>();
        Expression<Func<Department, bool>> searchExpression = null;
        if (request.SearchFields != null && request.SearchFields.Any())
        {
            var map = DepartmentSearchFieldDepartmentMap.Map(currentCulture);
			var predicates = request.SearchFields.Select(x => map[x]).ToList();
            searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
        }

        var res = await _departmentRepository.FindWithFilters(k=>k.Id !=null, includeProperties: $"{nameof(Department.CreatedBy)},{nameof(Department.UpdatedBy)},{nameof(Department.Parentdepartment)}", searchExpression );

        foreach (var Department in res.Value.ToList())
        {
            DepartmentDto dep = Department.CopyToDto();
            dep.CreatedByName = Department.CreatedBy?.NameEnglish;
            dep.UpdatedByName = Department.UpdatedBy?.NameEnglish;
            allDepartments.Add(dep);

        }
        return allDepartments;


    }

}