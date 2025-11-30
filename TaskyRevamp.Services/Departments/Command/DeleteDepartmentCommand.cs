using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Departments.Command;

public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
{
    private readonly IRepository<Department> _departmentRepository;

    public DeleteGroupCommandHandler(IRepository<Department> departmentRepository)
    {
		_departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
		var departmentResponse = await _departmentRepository.FindByKey(request.Id);
		if (departmentResponse.Success && departmentResponse.Value != null)
		{
			var department = departmentResponse.Value;
			var children = await _departmentRepository.FindBy(d => d.ParentdepartmentId == department.Id);
			if(children.Success && children.Value != null)
			{
				int newLevel = department.Level;
				var parentId = department.ParentdepartmentId;
				foreach (var child in children.Value)
				{
					child.ParentdepartmentId = parentId;
					child.Level = newLevel;
					await _departmentRepository.Update(child);
					await UpdateChildrenLevelsAsync(child.Id, child.Level);
				}
			}
		}
		await _departmentRepository.Delete(request.Id);
		return true;
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
}