using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Command
{
    public record CreatePrivilegeCommand(PrivilegeDto PrivilegeDto) : IRequest<bool>;
    public class CreatePrivilegeHandler : IRequestHandler<CreatePrivilegeCommand, bool>
    {
        private readonly IRepository<Privileges> _privilegeRepository;

        public CreatePrivilegeHandler(IRepository<Privileges> privilegeRepository)
        {
            this._privilegeRepository = privilegeRepository;
        }
        public async Task<bool> Handle(CreatePrivilegeCommand request, CancellationToken cancellationToken)
        {
            var privilegeDto = request.PrivilegeDto;

            var privilege = new Privileges();
            privilege.SetData(privilegeDto);

            await _privilegeRepository.Insert(privilege);
            
            return true;
        }
    }
}
