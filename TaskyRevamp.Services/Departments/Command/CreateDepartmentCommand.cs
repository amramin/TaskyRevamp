using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
namespace DepartmentyRevamp.Services.Departments.Commands;

public record CreateDepartmentCommand(DepartmentDto departmentDto) : IRequest<Guid>;

public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public CreateDepartmentHandler(IRepository<Department> departmentRepository) { _departmentRepository = departmentRepository; }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {

        await ValidateDepartment(request.departmentDto);
        Department department = new Department();
        department.SetData(request.departmentDto);
       
       
        await _departmentRepository.Insert(department);
        await _departmentRepository.SaveChangesAsync();
        return department.Id;
    }
    private async Task ValidateDepartment(DepartmentDto departmentDto)
    {
        var exists = await _departmentRepository.FindBy(p => p.Id != departmentDto.Id && (p.NameEnglish.ToLower() == departmentDto.NameEnglish.ToLower() || p.NameArabic.ToLower() == departmentDto.NameArabic.ToLower()));
        if (exists?.Value?.Count > 0)
        {
            var departs = exists.Value;
            var errors = new Dictionary<string, List<string>>();
            if (departs.Any(x => string.Equals(x.NameEnglish, departmentDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
                errors.Add(nameof(PriorityDto.NameEnglish), new List<string> { _localizer["DepartmentDuplicateValidation"] });
            if (departs.Any(x => string.Equals(x.NameArabic, departmentDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
                errors.Add(nameof(PriorityDto.NameArabic), new List<string> { _localizer["DepartmentDuplicateValidation"] });

            throw new ValidationException(errors);
        }
    }

}
