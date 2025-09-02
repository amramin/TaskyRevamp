using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.StatusConfiguration.Command;
using TaskyRevamp.Services.SystemConfiguration.StatusConfiguration.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatusSettingController : ControllerBase
	{
		private readonly IMediator _mediator;

		public StatusSettingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetStatusSettings")]
		public async Task<IActionResult> GetStatus()
		{
			return Ok(await _mediator.Send(new GetStatusSettingsQuery()));
		}

		[HttpGet("GetStatusById/{id}")]
		public async Task<IActionResult> GetStatusByID(Guid id)
		{
			return Ok(await _mediator.Send(new GetStatusByIdQuery(id)));
		}

		[HttpPost("UpdateStatusSettings")]
		public async Task<ActionResult> UpdateStatus([FromBody] StatusSettingsDto _statusDto)
		{
			return Ok(await _mediator.Send(new UpdateStatusCommand(_statusDto)));
		}
	}
}
