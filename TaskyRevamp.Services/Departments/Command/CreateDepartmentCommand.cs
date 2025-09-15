using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;

namespace TaskyRevamp.Services.Departments.Command;

public record CreateDepartmentCommand(DepartmentDto DepartmentDto) : IRequest<Guid>;

public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IRepository<Department> _departmentRepository;

    public CreateDepartmentHandler(IRepository<Department> departmentRepository) =>
        _departmentRepository = departmentRepository;

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department();
        department.Name = request.DepartmentDto.Name;

        await _departmentRepository.Insert(department);
        await _departmentRepository.SaveChangesAsync();
        return department.Id;
    }
}