using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Services.Users.Query
{
	public record GetUnAssignedUserstoPrivilegeQuery() : IRequest<List<UserDto>>;
	public class GetUnAssignedUserstoPrivilegeHandler : IRequestHandler<GetUnAssignedUserstoPrivilegeQuery, List<UserDto>>
	{
		private readonly IRepository<User> _userRepository;
		public GetUnAssignedUserstoPrivilegeHandler(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<List<UserDto>> Handle(GetUnAssignedUserstoPrivilegeQuery request, CancellationToken cancellationToken)
		{
			List<UserDto> allusers = new List<UserDto>();
			var users = await _userRepository.FindBy(u => u.PrivilegeId == null);
			if(users.Success && users.Value != null)
			{
				allusers = users.Value.Select(u => u.CopyToDto()).ToList();
			}
			return allusers;
		}
	}
}
