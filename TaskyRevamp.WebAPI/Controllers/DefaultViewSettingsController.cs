using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.DefaultViewSetting.Command;
using TaskyRevamp.Services.SystemConfiguration.DefaultViewSetting.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DefaultViewSettingsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public DefaultViewSettingsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetDefaultViewSettings")]
		public async Task<IActionResult> GetDefaultViewSettings()
		{
			return Ok(await _mediator.Send(new GetDefaultViewSettingQuery()));
		}

		[HttpPost("UpdateDefaultViewSetting")]
		public async Task<IActionResult> UpdateDefultViewSetting(DefaultViewSettingsDto viewSettingsDto)
		{
			return	Ok(await _mediator.Send(new UpdateDefaultViewSettingCommand(viewSettingsDto)));
		}

		[HttpPost("UpdateSubTaskLevelDefaultViewSetting")]
		public async Task<IActionResult> UpdateSubTaskLevelDefultViewSetting(DefaultViewSettingsDto viewSettingsDto)
		{
			return Ok(await _mediator.Send(new UpdateSubTaskDefaultViewSettingCommand(viewSettingsDto)));
		}
	}
}
