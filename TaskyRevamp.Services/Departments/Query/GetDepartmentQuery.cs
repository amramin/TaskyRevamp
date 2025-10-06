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
        var res = await _DepartmentRepository.FindByKey(request.Id);
        DepartmentDto DepartmentModel = res.Value.CopyToDto();
        return DepartmentModel;
    }

 
}