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
	public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;
	public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
	{
		private readonly IRepository<User> _userRepository;

		public GetUserByIdHandler(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			UserDto userDto = new UserDto();
			var res = await _userRepository.FindBy(u => u.Id == request.UserId);
			if(res.Success && res.Value != null && res.Value.Any())
			{
				var user = res.Value.FirstOrDefault();
				if(user != null)
				{
					userDto = user.CopyToDto();
				}
			}
			return userDto;
		}
	}
}
