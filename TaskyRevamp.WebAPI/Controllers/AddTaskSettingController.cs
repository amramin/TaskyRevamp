using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.AddTaskSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.AddTaskSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AddTaskSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddTaskSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAddTaskSettings")]
        public async Task<ActionResult> GetAddTaskSettings()
        {
            return Ok(await _mediator.Send(new GetAddTaskSettingsQuery()));
        }

        [HttpPost("UpdateAddTaskSettings")]
        public async Task<ActionResult> UpdateAddTaskSettings([FromBody] List<AddTaskSettingDto> addTasksSettings)
        {

            return Ok(await _mediator.Send(new UpdateAddTaskSettingsCommand(addTasksSettings)));
        }
    }
}
