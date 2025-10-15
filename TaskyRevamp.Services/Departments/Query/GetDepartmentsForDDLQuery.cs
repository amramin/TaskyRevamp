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

public record GetDepartmentsForDDLQuery() : IRequest<List<DepartmentDto>>;

public class GetDepartmentsForDDLHandler : IRequestHandler<GetDepartmentsForDDLQuery, List<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;


    public GetDepartmentsForDDLHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsForDDLQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDto> allDepartments = new List<DepartmentDto>();


        var data = await _departmentRepository.FindBy(K => K.Id != null, includeProperties: $"{nameof(Department.Parentdepartment)},{nameof(Department.CreatedBy)}");



        foreach (var Department in data.Value)
        {
            DepartmentDto dep = Department.CopyToDto();
            dep.CreatedByName = Department.CreatedBy?.NameEnglish;
            dep.UpdatedByName = Department.UpdatedBy?.NameEnglish;
            allDepartments.Add(dep);

        }

        return allDepartments;
    }

}