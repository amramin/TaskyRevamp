using MediatR;
using Microsoft.AspNetCore.Http;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;

namespace userRevamp.Services.userCQRS.Query;

public record GetUsersByDepartmentQuery(Guid departmentId) : IRequest<DepartmentDto>;

public class GetUsersByDepartmentHandler : IRequestHandler<GetUsersByDepartmentQuery, DepartmentDto>
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<User> _userRepository;

    public GetUsersByDepartmentHandler(IRepository<Department> departmentRepository, IRepository<User> userRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto> Handle(GetUsersByDepartmentQuery request, CancellationToken cancellationToken)
    {

        DepartmentDto returned = new DepartmentDto();
        var data = await _departmentRepository.FindBy(k => k.Id == request.departmentId, includeProperties: $"{nameof(Department.AssignedUser)}.{nameof(User.Privilege)}");
        returned.AssignedUsers = new List<UserDto>();
        var department = data.Value.FirstOrDefault();
        returned = department.CopyToDto();
        //var all = await _userRepository.FindByAsNoTracking(u => u.DepartmentId == request.departmentId, includeProperties: $"{nameof(User.Department)},{nameof(User.Privilege)}");// ?? new List<User>();

        //returned = all.Value.FirstOrDefault()?.Department.CopyToDto();

        foreach (var usr in department.AssignedUser)
        {
            var userDto = new UserDto()
            {
                Id = usr.Id,
                userNameAR = usr.NameArabic,
                userNameEN = usr.NameEnglish,
                Email = usr.Email,
                PrivilegName = usr.Privilege?.NameEnglish
            };

            returned.AssignedUsers.Add(userDto);
        }
        return returned;

    }


}