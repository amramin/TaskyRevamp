using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Services.Departments.Query;

public record GetDepartmentsQuery(QueryModel? Query) : IRequest<List<DepartmentDto>>;

public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;


    public GetDepartmentsHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDto> Departmentss = new List<DepartmentDto>();
       

        var data = await _DepartmentRepository.FindBy(K=>K.Id!=null, includeProperties:$"{nameof(Department.Parentdepartment) },{nameof(Department.CreatedBy)}");



        foreach (var Department in data.Value)
        {
            DepartmentDto dep = Department.CopyToDto();
            dep.CreatedByName = Department.CreatedBy?.NameEnglish;
            dep.UpdatedByName = Department.UpdateddBy?.NameEnglish;
            Departmentss.Add(dep);
            
        }
        
        return departmentsDto;
    }
}