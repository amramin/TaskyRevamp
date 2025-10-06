using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;

namespace TaskyRevamp.Services.Departments.Query;

public record GetDepartmentQuery(Guid Id) : IRequest<DepartmentDto>;

public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
{
    private readonly IRepository<Department> _departmentRepository;

    public GetDepartmentByIdHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.FindByKey(request.Id);
        if (!department.Success || department?.Value is null)
        {
            throw new Exception("Department not found");
        }
        var departmentDto = new DepartmentDto()
        {
            Id = department.Value.Id,
            Name = department.Value.Name
        };

        return departmentDto;
    }

 
}