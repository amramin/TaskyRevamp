using MediatR;
using Microsoft.Extensions.Localization;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
namespace TaskyRevamp.Services.Departments.Command;

public record CreateDepartmentsBulkCommand(List<DepartmentDto> DepartmentDtos) : IRequest<bool>;

public class CreateDepartmentsBulkHandler : IRequestHandler<CreateDepartmentsBulkCommand, bool>
{
    private readonly IRepository<Department> _departmentsRepository;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public CreateDepartmentsBulkHandler(
        IRepository<Department> _department, IStringLocalizer<SharedResources> localizer
        )
    {
        _departmentsRepository = _department;
        _localizer = localizer;
    }

    public async Task<bool> Handle(CreateDepartmentsBulkCommand request, CancellationToken cancellationToken)
    {
        foreach (var dept in request.DepartmentDtos)
        {
            await Validate(dept);

        }

        await _departmentsRepository.InsertRange(
    request.DepartmentDtos.Select(item => new Department
    {
        Level = item.Level,
        NameArabic = item.NameArabic,
        NameEnglish = item.NameEnglish,
        ParentdepartmentId = item.ParentdepartmentId
    })
);



        await _departmentsRepository.SaveChangesAsync();

        return true;
    }

    private async Task Validate(DepartmentDto departmentDto)
    {
        var exists = await _departmentsRepository.FindBy(p => p.Id != departmentDto.Id
            && (p.NameEnglish.ToLower() == departmentDto.NameEnglish.ToLower()
            || p.NameArabic.ToLower() == departmentDto.NameArabic.ToLower()));
        if (exists?.Value?.Count > 0)
        {
            var privileges = exists.Value;
            var errors = new Dictionary<string, List<string>>();
            if (privileges.Any(x => string.Equals(x.NameEnglish, departmentDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
                errors.Add(nameof(departmentDto.NameEnglish), new List<string> { _localizer["DepartmentDuplicateValidation"] });
            if (privileges.Any(x => string.Equals(x.NameArabic, departmentDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
                errors.Add(nameof(departmentDto.NameArabic), new List<string> { _localizer["DepartmentDuplicateValidation"] });

            throw new ValidationException(errors);
        }
    }

}