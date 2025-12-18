using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Services.Users.Command
{
	public record LinkUserCommand(UserDto user, Guid DepartmentId, Guid PrivilegeId) : IRequest<bool>;
	public class LinkUserHandler : IRequestHandler<LinkUserCommand, bool>
	{
		private readonly IRepository<User> _userRepository;

		public LinkUserHandler(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<bool> Handle(LinkUserCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await _userRepository.FindBy(u => u.Id == request.user.Id);
			if(existingUser.Success && existingUser.Value != null && existingUser.Value.Any())
			{
				var user = existingUser.Value.FirstOrDefault();
				if(user != null)
				{
					user.PrivilegeId= request.PrivilegeId;
					user.DepartmentId = request.DepartmentId;
					user.IsActive = request.user.IsActive;
					user.UpdatedById = request.user.UpdatedById;
					await _userRepository.Update(user);
					await _userRepository.SaveChangesAsync();
				}
			}
			return true;
		}
	}
}
