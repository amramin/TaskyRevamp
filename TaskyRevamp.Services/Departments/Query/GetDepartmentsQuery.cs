
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;


namespace DepartmentyRevamp.Services.Departments.Query;

public record GetDepartmentsQuery(QueryModel? Query) : IRequest<List<DepartmentDto>>;

public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentDto>>
{
    private readonly IRepository<Department> _DepartmentRepository;


    public GetDepartmentsHandler(IRepository<Department> DepartmentRepository)
    {
        _DepartmentRepository = DepartmentRepository;
      
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDto> Departmentss = new List<DepartmentDto>();
       

        var data = await _DepartmentRepository.All();



        foreach (var Department in data.Value)
        {
        ;
            Departmentss.Add(new DepartmentDto()
            {
                Id = Department.Id,Name = Department.Name,
            });
        }

       // return Departments.ToList();

        return  Departmentss;
    }

   
}