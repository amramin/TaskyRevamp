using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Command
{
    public record CreatePrivilegeCommand(PrivilegeDto PrivilegeDto) : IRequest<bool>;
    public class CreatePrivilegeHandler : IRequestHandler<CreatePrivilegeCommand, bool>
    {
        private readonly IRepository<Privileges> _privilegeRepository;
		private readonly IStringLocalizer<SharedResources> _localizer;
		public CreatePrivilegeHandler(IRepository<Privileges> privilegeRepository, IStringLocalizer<SharedResources> localizer)
        {
            this._privilegeRepository = privilegeRepository;
			_localizer = localizer;
		}
        public async Task<bool> Handle(CreatePrivilegeCommand request, CancellationToken cancellationToken)
        {
			await ValidatePrivilege(request.PrivilegeDto);
			var privilegeDto = request.PrivilegeDto;

			var privilege = new Privileges();
            privilege.SetData(privilegeDto);
            await _privilegeRepository.Insert(privilege);
            return true;
        }

		private async Task ValidatePrivilege(PrivilegeDto privilegeDto)
		{
			var exists = await _privilegeRepository.FindBy(p => p.Id != privilegeDto.Id
				&& (p.NameEnglish.ToLower() == privilegeDto.NameEnglish.ToLower()
				|| p.NameArabic.ToLower() == privilegeDto.NameArabic.ToLower()));
			if (exists?.Value?.Count > 0)
			{
				var privileges = exists.Value;
				var errors = new Dictionary<string, List<string>>();
				if (privileges.Any(x => string.Equals(x.NameEnglish, privilegeDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
					errors.Add(nameof(PrivilegeDto.NameEnglish), new List<string> { _localizer["PrivilegeDuplicateValidation"] });
				if (privileges.Any(x => string.Equals(x.NameArabic, privilegeDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
					errors.Add(nameof(PrivilegeDto.NameArabic), new List<string> { _localizer["PrivilegeDuplicateValidation"] });

				throw new ValidationException(errors);
			}
		}
	}
}
