using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;

namespace DepartmentyRevamp.Services.Departments.Commands;

public record CreateDepartmentCommand(DepartmentDto departmentDto) : IRequest<Guid>;

public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IRepository<Department> _DepartmentRepository;

    public CreateDepartmentHandler(IRepository<Department> DepartmentRepository) => _DepartmentRepository = DepartmentRepository;

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department department=new Department();
        department.Name=request.departmentDto.Name;
       
        await _DepartmentRepository.Insert(department);
        await _DepartmentRepository.SaveChangesAsync();
        return department.Id;

    }
}
