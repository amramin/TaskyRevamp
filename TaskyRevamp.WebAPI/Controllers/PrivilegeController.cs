using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Services.Permission.Privilege.Command;

namespace TaskyRevamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PrivilegeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreatePrivilege")]
        public async Task<IActionResult> CreatePrivilege([FromBody] PrivilegeDto privilegeDto)
        {
            return Ok(await _mediator.Send(new CreatePrivilegeCommand(privilegeDto)));
        }

        [HttpPost("UpdatePrivilege")]
        public async Task<IActionResult> UpdatePrivilege([FromBody] PrivilegeDto privilegeDto)
        {
            return Ok(await _mediator.Send(new UpdatePrivilegeCommand(privilegeDto)));
        }


    }
}
