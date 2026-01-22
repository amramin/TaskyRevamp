using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;

namespace userRevamp.Services.UserS.Query;
public record GetUnassignedUsersToDepartmentQuery() : IRequest<List<UserDto>>;

public class GetUnassignedUsersToDepartmentHandler : IRequestHandler<GetUnassignedUsersToDepartmentQuery, List<UserDto>>
{
    private readonly IRepository<User> _userRepository;


    public GetUnassignedUsersToDepartmentHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetUnassignedUsersToDepartmentQuery request, CancellationToken cancellationToken)
    {
        List<UserDto> allusers = new List<UserDto>();
        var data = await _userRepository.FindBy(K => (K.DepartmentId == null && K.IsActive));
        if(data.Value != null)
			data.Value.ToList().ForEach(k => allusers.Add(k.CopyToDto()));
        return allusers;
    }
}