using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;



namespace DepartmentyRevamp.Services.Departments.Commands;

public record UpdateDepartmentCommand(DepartmentDto Department) : IRequest<bool>;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IRepository<Department> _DepartmentRepository;

    public UpdateDepartmentCommandHandler(IRepository<Department> DepartmentRepository)
    {
        _DepartmentRepository = DepartmentRepository;
    }

    public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var DepartmentResponse = await _DepartmentRepository.FindByKey(request.Department.Id);
        if (!DepartmentResponse.Success)
        {
            return false;
        }
        var updated = DepartmentResponse.Value;
        updated.SetData(request.Department);
        await _DepartmentRepository.Update(updated);

        return true;
    }
}