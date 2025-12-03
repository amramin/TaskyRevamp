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

			if (parentChanged)
			{
				// Store the old parent before changing
				var oldParentId = department.ParentdepartmentId;
				await BreakCircularReferenceIfExists(request.department.Id, request.department.ParentdepartmentId, oldParentId);
				department.Level = await CalculateLevelAsync(request.department.ParentdepartmentId);
				department.SetData(request.department);
				await UpdateChildrenLevelsAsync(department.Id, department.Level);
				await _departmentRepository.Update(department);
			}
			else
			{
				department.SetData(request.department);
				await _departmentRepository.Update(department);
			}
		}
        return true;
    }
	private async Task BreakCircularReferenceIfExists(Guid departmentId, Guid? newParentId, Guid? oldParentId)
	{
		if (newParentId == null || newParentId == Guid.Empty)
			return;
		// Check if the new parent is a descendant of this department
		var descendants = await GetAllDescendants(departmentId);
		if (descendants.Any(d => d.Id == newParentId))
		{
			// The new parent is currently a descendant
			// Move it to the current department's parent (swap positions)
			var newParentDept = descendants.First(d => d.Id == newParentId);
			// Set the new parent to have the same parent as the current department
			// This maintains the hierarchy structure
			newParentDept.ParentdepartmentId = oldParentId;
			newParentDept.Level = await CalculateLevelAsync(oldParentId);
			await _departmentRepository.Update(newParentDept);
			await UpdateChildrenLevelsAsync(newParentDept.Id, newParentDept.Level); // update childern level
		}
	}

	private async Task<List<Department>> GetAllDescendants(Guid departmentId)
	{
		var allDescendants = new List<Department>();
		await CollectDescendants(departmentId, allDescendants);
		return allDescendants;
	}

	private async Task CollectDescendants(Guid departmentId, List<Department> descendants) // -->  all childern of selected department 
	{
		var children = await _departmentRepository.FindBy(d => d.ParentdepartmentId == departmentId);
		if (children?.Value != null && children.Value.Count > 0)
		{
			descendants.AddRange(children.Value);
			foreach (var child in children.Value)
			{
				await CollectDescendants(child.Id, descendants);
			}
		}
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