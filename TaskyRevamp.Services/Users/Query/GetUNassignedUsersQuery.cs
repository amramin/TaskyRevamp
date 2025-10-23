using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;

namespace userRevamp.Services.userCQRS.Query;
public record GetUNassignedUsersQuery() : IRequest<List<UserDto>>;

public class GetUNassignedUsersHandler : IRequestHandler<GetUNassignedUsersQuery, List<UserDto>>
{
    private readonly IRepository<User> _userRepository;


    public GetUNassignedUsersHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetUNassignedUsersQuery request, CancellationToken cancellationToken)
    {
        List<UserDto> allusers = new List<UserDto>();
       

        var data = await _userRepository.FindBy(K=>K.DepartmentId==null);

        data.Value.ToList().ForEach(k => allusers.Add(k.CopyToDto()));
        
        
        return allusers;
    }
}