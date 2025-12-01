using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Services.Users.Command
{
	public record LinkUserWithDepartmentAndPrivilegeCommand(UserLinkDto UserLinkDto) : IRequest<bool>;
	public class LinkUserWithDepartmentAndPrivilegeHandler : IRequestHandler<LinkUserWithDepartmentAndPrivilegeCommand, bool>
	{
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Department> _departmentRepository;
		private readonly IRepository<Privilege> _privilegeRepository;

		public LinkUserWithDepartmentAndPrivilegeHandler(IRepository<User> userRepository, IRepository<Department> departmentRepository, IRepository<Privilege> privilegeRepository)
		{
			_userRepository = userRepository;
			_departmentRepository = departmentRepository;
			_privilegeRepository = privilegeRepository;
		}

		public async Task<bool> Handle(LinkUserWithDepartmentAndPrivilegeCommand request, CancellationToken cancellationToken)
		{
			var email = request.UserLinkDto.email.Trim().ToLower();
			var _departmentName = request.UserLinkDto.departmentName.Trim().ToLower();
			var _privilegeName = request.UserLinkDto.privilegeName.Trim().ToLower();

			var deptRes = await _departmentRepository.FindBy(d => d.NameEnglish.ToLower() == _departmentName || d.NameArabic.ToLower() == _departmentName);
			var department = deptRes.Value?.FirstOrDefault();
			if (!deptRes.Success || department == null)
				return false;

			var privRes = await _privilegeRepository.FindBy(p => p.NameEnglish.ToLower() == _privilegeName || p.NameArabic.ToLower() == _privilegeName);
			var privilege = privRes.Value?.FirstOrDefault();
			if (!privRes.Success || privilege == null)
				return false;

			var userRes = await _userRepository.FindBy(u => u.Email!.ToLower() == email);
			var user = userRes.Value?.FirstOrDefault();
			if (!userRes.Success || user == null)
			{
				return true;
			}
			if (user.DepartmentId != null && user.PrivilegeId != null)
			{
				return true;
			}
			user.DepartmentId = department.Id;
			user.PrivilegeId = privilege.Id;
			await _userRepository.Update(user);
			await _userRepository.SaveChangesAsync();

			return true;
		}
	}
}
