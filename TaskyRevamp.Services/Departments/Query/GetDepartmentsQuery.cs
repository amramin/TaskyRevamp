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
        var departmentsDto = new List<DepartmentDto>();
        var departments = await _departmentRepository.All();
        
        foreach (var department in departments?.Value ??[])
        {
            departmentsDto.Add(new DepartmentDto()
            {
                Id = department.Id, Name = department.Name,
            });
        }
        
        return departmentsDto;
    }
}