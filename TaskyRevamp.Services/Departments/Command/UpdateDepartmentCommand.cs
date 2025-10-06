using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;

namespace TaskyRevamp.Services.Departments.Command;

public record UpdateDepartmentCommand(DepartmentDto Department) : IRequest<bool>;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IRepository<Department> _departmentRepository;

    public UpdateDepartmentCommandHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentResponse = await _departmentRepository.FindByKey(request.Department.Id);
        if (!departmentResponse.Success)
        {
            return false;
        }
        var updated = DepartmentResponse.Value;
        updated.SetData(request.Department);
        await _DepartmentRepository.Update(updated);

        return true;
    }
}