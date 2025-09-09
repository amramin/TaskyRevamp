
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;


namespace DepartmentyRevamp.Services.Departments.Query;

public record GetDepartmentQuery(Guid Id) : IRequest<DepartmentDto>;

public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
{
    private readonly IRepository<Department> _DepartmentRepository;

    public GetDepartmentByIdHandler(IRepository<Department> DepartmentRepository)
    {
        _DepartmentRepository = DepartmentRepository;
    }

    public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var res = await _DepartmentRepository.FindByKey(request.Id);
        DepartmentDto DepartmentModel = res.Value.CopyToDto();
        return DepartmentModel;
    }

 
}