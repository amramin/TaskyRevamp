using MediatR;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Services.Users.Command
{
	public record AssignUsersToPrivilegeCommand(List<UserDto> UsersDto, Guid PrivilegeId): IRequest<bool>;
	public class AssignUsersToPrivilegeHandler : IRequestHandler<AssignUsersToPrivilegeCommand, bool>
	{
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Privilege> _privilegeRepository;

		public AssignUsersToPrivilegeHandler(IRepository<User> userRepository, IRepository<Privilege> privilegeRepository)
		{
			_userRepository = userRepository;
			_privilegeRepository = privilegeRepository;
		}

		public async Task<bool> Handle(AssignUsersToPrivilegeCommand request, CancellationToken cancellationToken)
		{
			PrivilegeDto privilegeDto = new PrivilegeDto();
			var privilege = await _privilegeRepository.FindBy(u => u.Id == request.PrivilegeId);
			if (privilege.Success && privilege != null && privilege.Value != null)
			{
				privilegeDto = privilege.Value.FirstOrDefault()!.CopyToDto();
			}
			var users = request.UsersDto;
			if (users.Any())
			{
				foreach (var userDto in users)
				{
					var res = await _userRepository.FindBy(u => u.Id == userDto.Id);
					if (res.Success && res != null && res.Value != null)
					{
						var user = res.Value.FirstOrDefault();
						if(user != null)
						{
							user.PrivilegeId = privilegeDto.Id;
							await _userRepository.Update(user);
							await _userRepository.SaveChangesAsync();
						}	
					}
				}
			}
			return true;
		}
	}
}
