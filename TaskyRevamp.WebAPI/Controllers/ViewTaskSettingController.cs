using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.ViewTaskSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.ViewTaskSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ViewTaskSettingController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ViewTaskSettingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetViewTaskSettings")]
		public async Task<IActionResult> GetViewTaskSettings()
		{
			return Ok(await _mediator.Send(new GetViewTaskSettingsQuery()));	
		}

		[HttpGet("GetActiveViewTaskSettings")]
		public async Task<IActionResult> GetActiveViewTaskSettings()
		{
			return Ok(await _mediator.Send(new GetActiveViewTaskSettingsQuery()));
		}

		[HttpPost("UpdateTaskViewsActivation")]
		public async Task<IActionResult> UpdateTaskViewSetting(List<ViewTaskSettingsDto> TaskViews)
		{
			return Ok(await _mediator.Send(new UpdateViewTaskSettingActivationCommand(TaskViews)));
		}
	}
}
