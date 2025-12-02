using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;

namespace TaskyRevamp.Services.Departments.Command;

public record CreateDepartmentsBulkCommand(List<DepartmentDto> DepartmentDtos) : IRequest<bool>;

public class CreateDepartmentsBulkHandler : IRequestHandler<CreateDepartmentsBulkCommand, bool>
{
    private readonly IRepository<Department> _departmentsRepository;

    public CreateDepartmentsBulkHandler(
        IRepository<Department> _department)
    {
        _departmentsRepository = _department;
    }

    public async Task<bool> Handle(CreateDepartmentsBulkCommand request, CancellationToken cancellationToken)
    {



        List<Department> all = new List<Department>();

        foreach (var item in request.DepartmentDtos)
        {
            all.Add(new Department()
            {
                Level = item.Level,
                NameArabic = item.NameArabic,
                NameEnglish = item.NameEnglish,
                ParentdepartmentId = item.ParentdepartmentId,
            });
        }






        await _departmentsRepository.InsertRange(all);
        await _departmentsRepository.SaveChangesAsync();

        return true;
    }
}