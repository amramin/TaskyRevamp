using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Command
{
    public record UpdatePrivilegeCommand(PrivilegeDto PrivilegeDto) : IRequest<bool>;
    public class UpdatePrivilegeHandler : IRequestHandler<UpdatePrivilegeCommand, bool>
    {
        private readonly IRepository<Privileges> _privilegeRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdatePrivilegeHandler(IRepository<Privileges> privilegeRepository, IStringLocalizer<SharedResources> localizer)
        {
            this._privilegeRepository = privilegeRepository;
            _localizer = localizer;
        }
        public async Task<bool> Handle(UpdatePrivilegeCommand request, CancellationToken cancellationToken)
        {

            var privilegeDto = request.PrivilegeDto;
            var existingPrivilegeResponse = await _privilegeRepository.FindByKey(privilegeDto.Id);
            if (existingPrivilegeResponse == null || !existingPrivilegeResponse.Success || existingPrivilegeResponse.Value == null)
            {
                throw new NotFoundException(_localizer[ApiError.PrivilegeNotFound].Value.Replace("{id}", privilegeDto.Id.ToString()));
            }

            var existingPrivilege = existingPrivilegeResponse.Value;

            existingPrivilege.SetData(privilegeDto);

            await _privilegeRepository.Update(existingPrivilege);
            
            return true;
        }

    }
}
