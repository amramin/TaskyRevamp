using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;

namespace TaskyRevamp.Services.Departments.Command;

public record UpdateDepartmentCommand(DepartmentDto department) : IRequest<bool>;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IRepository<Department> _departmentRepository;

    public UpdateDepartmentCommandHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentResponse = await _departmentRepository.FindByKey(request.department.Id);
        if (!departmentResponse.Success)
        {
            return false;
        }
       

        if (departmentResponse.Value is null)
        {
            throw new Exception("Department not found");
        }


        var updated = departmentResponse.Value;
        updated.SetData(request.department);
        await _departmentRepository.Update(updated);

        return true;
    }
}