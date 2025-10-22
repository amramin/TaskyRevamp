using MediatR;
using Microsoft.AspNetCore.Http;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;

namespace userRevamp.Services.userCQRS.Query;

public record GetAllUsersByDepartmentQuery(Guid departmentId) : IRequest<DepartmentDto>;

public class GetAllUsersByDepartmentHandler : IRequestHandler<GetAllUsersByDepartmentQuery,DepartmentDto>
{
    private readonly IRepository<Department> _departmentRepository;

    public GetAllUsersByDepartmentHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto> Handle(GetAllUsersByDepartmentQuery request, CancellationToken cancellationToken)
    {

       DepartmentDto returned = new DepartmentDto();
        var data = await _departmentRepository.FindBy(k => k.Id == request.departmentId,includeProperties:$"{nameof(Department.AssignedUser)}");
        returned.AssignedUsers = new List<UserDto>();
       var  department = data.Value.FirstOrDefault();
        returned = department.CopyToDto();
        
        foreach (var usr in department.AssignedUser)
        {
            var userDto = new UserDto()
            {
                Id = usr.Id,
                userNameAR = usr.NameArabic,
                userNameEN = usr.Username,
                Email = usr.Email
            };
            returned.AssignedUsers.Add(userDto);
        }
        return returned;

    }


}