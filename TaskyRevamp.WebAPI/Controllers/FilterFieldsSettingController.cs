using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.FilterFieldsSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.FilterFieldsSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilterFieldsSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilterFieldsSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetFilterFieldsSettings")]
        public async Task<ActionResult> GetFilterFieldsSettings()
        {
            return Ok(await _mediator.Send(new GetFilterFieldsSettingsQuery()));
        }

        [HttpPost("UpdateFilterFieldsSettings")]
        public async Task<ActionResult> UpdateFilterFieldsSettings([FromBody] List<FilterFieldsSettingDto> filterFieldsSettings)
        {

            return Ok(await _mediator.Send(new UpdateFilterFieldsSettingsCommand(filterFieldsSettings)));
        }
    }
}
