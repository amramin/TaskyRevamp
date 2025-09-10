using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.DefaultColumnsSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.DefaultColumnsSettings.Query;


namespace TaskyRevamp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefaultColumnsSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DefaultColumnsSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetDefaultColumnsSettings")]
        public async Task<ActionResult> GetDefaultColumnsSettings()
        {
            return Ok(await _mediator.Send(new GetDefaultColumnsSettingsQuery()));
        }

        [HttpPost("UpdateDefaultColumnsSettings")]
        public async Task<ActionResult> UpdateDefaultColumnsSettings([FromBody] List<DefaultColumnsSettingDto> defaultColumnsSettings)
        {

            return Ok(await _mediator.Send(new UpdateDefaultColumnsSettingsCommand(defaultColumnsSettings)));
        }
    }
}
