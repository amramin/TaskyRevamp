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
	public record LinkUserWithDepartmentAndPrivilegeCommand(List<UserLinkDto> UserLinkDtos) : IRequest<bool>;
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
			if (request.UserLinkDtos == null || !request.UserLinkDtos.Any())
				return false;
			var dtos = request.UserLinkDtos;

			//extract data
			var emails = dtos.Select(x => x.email.Trim().ToLower()).ToList();
			var departmnets = dtos.Select(x => x.departmentName.Trim().ToLower()).ToList();
			var privileges = dtos.Select(x => x.privilegeName.Trim().ToLower()).ToList();

			// batch load (one query for each)
			var departmentsRes = await _departmentRepository.FindBy(d => departmnets.Contains(d.NameArabic.ToLower()) || departmnets.Contains(d.NameEnglish.ToLower()));
			if (!departmentsRes.Success || departmentsRes.Value == null)
				return false;
			var privilegeRes = await _privilegeRepository.FindBy(d => privileges.Contains(d.NameArabic.ToLower()) || departmnets.Contains(d.NameEnglish.ToLower()));
			if (!privilegeRes.Success || privilegeRes.Value == null)
				return false;
			var emailRes = await _userRepository.FindBy(d => emails.Contains(d.Email!.ToLower()));
			if (!emailRes.Success || emailRes.Value == null)
				return false;

			var departmentLookup = departmentsRes.Value.SelectMany(d => new[]
			{
				new { Key = d.NameEnglish.ToLower(), Value = d },
				new { Key = d.NameArabic.ToLower(), Value = d }
			}).GroupBy(x => x.Key).ToDictionary(g => g.Key, g => g.First().Value);

			var privilegeLookup = privilegeRes.Value.SelectMany(p => new[]
			{
				new { Key = p.NameEnglish.ToLower(), Value = p },
				new { Key = p.NameArabic.ToLower(), Value = p }
			}).GroupBy(x => x.Key).ToDictionary(g => g.Key, g => g.First().Value);

			var userLookup = emailRes.Value.ToDictionary(u => u.Email!.ToLower(), u => u);

			var processedUsers = new HashSet<string>();
			var usersToUpdate = new List<User>();

			foreach (var UserLinkDto in request.UserLinkDtos)
			{
				var email = UserLinkDto.email.Trim().ToLower();
				var _departmentName = UserLinkDto.departmentName.Trim().ToLower();
				var _privilegeName = UserLinkDto.privilegeName.Trim().ToLower();

				if (processedUsers.Contains(email))
					continue;

				if (!departmentLookup.TryGetValue(_departmentName, out var department))
					return false;

				if (!privilegeLookup.TryGetValue(_privilegeName, out var privilege))
					return false;

				if (!userLookup.TryGetValue(email, out var user))
					return false;

				if (user.DepartmentId == null)
					user.DepartmentId = department.Id;

				if (user.PrivilegeId == null)
					user.PrivilegeId = privilege.Id;

				usersToUpdate.Add(user);
				processedUsers.Add(email);
			}
			await _userRepository.UpdateRange(usersToUpdate);
			return true;
		}
	}
}
