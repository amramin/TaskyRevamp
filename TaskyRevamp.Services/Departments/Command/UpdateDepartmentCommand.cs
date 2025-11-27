using MediatR;
using Microsoft.Extensions.Localization;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;

namespace TaskyRevamp.Services.Departments.Command;

public record UpdateDepartmentCommand(DepartmentDto department) : IRequest<bool>;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IRepository<Department> _departmentRepository;
	private readonly IStringLocalizer<SharedResources> _localizer;
	public UpdateDepartmentCommandHandler(IRepository<Department> departmentRepository, IStringLocalizer<SharedResources> localizer)
	{
		_departmentRepository = departmentRepository;
		_localizer = localizer;
	}

	public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
		await ValidateDepartment(request.department);
		var departmentResponse = await _departmentRepository.FindByKey(request.department.Id);
        if (departmentResponse.Success && departmentResponse.Value != null)
		{
			var department = departmentResponse.Value;
			// Check if parent changed
			bool parentChanged = department.ParentdepartmentId !=
				(request.department.ParentdepartmentId == Guid.Empty ? null : request.department.ParentdepartmentId);
			department.SetData(request.department);
			if (parentChanged)
			{
				department.Level = await CalculateLevelAsync(request.department.ParentdepartmentId);
				await UpdateChildrenLevelsAsync(department.Id, department.Level);
			}
			department.SetData(request.department);
			await _departmentRepository.Update(department);
		}
        return true;
    }
	private async Task<int> CalculateLevelAsync(Guid? parentId)
	{
		if (parentId == null || parentId == Guid.Empty)
			return 1; // Root level
		var parent = await _departmentRepository.FindByKey(parentId.Value);
		if (parent.Success && parent.Value != null)
			return parent != null ? parent.Value.Level + 1 : 1;
		return 1;
	}
	private async Task UpdateChildrenLevelsAsync(Guid departmentId, int parentLevel)
	{
		var children = await _departmentRepository.FindBy(d => d.ParentdepartmentId == departmentId);
		if (children?.Value != null)
		{
			foreach (var child in children.Value)
			{
				child.Level = parentLevel + 1;
				await _departmentRepository.Update(child);
				await UpdateChildrenLevelsAsync(child.Id, child.Level);
			}
		}
	}
	private async Task ValidateDepartment(DepartmentDto departmentDto)
	{
		var exists = await _departmentRepository.FindBy(p => p.Id != departmentDto.Id && (p.NameEnglish.ToLower() == departmentDto.NameEnglish.ToLower() || p.NameArabic.ToLower() == departmentDto.NameArabic.ToLower()));
		if (exists?.Value?.Count > 0)
		{
			var departs = exists.Value;
			var errors = new Dictionary<string, List<string>>();
			if (departs.Any(x => string.Equals(x.NameEnglish, departmentDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
				errors.Add(nameof(DepartmentDto.NameEnglish), new List<string> { _localizer["DepartmentDuplicateValidation"] });
			if (departs.Any(x => string.Equals(x.NameArabic, departmentDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
				errors.Add(nameof(DepartmentDto.NameArabic), new List<string> { _localizer["DepartmentDuplicateValidation"] });

			throw new ValidationException(errors);
		}
	}
}