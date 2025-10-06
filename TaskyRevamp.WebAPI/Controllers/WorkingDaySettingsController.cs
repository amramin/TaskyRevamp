using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.WorkingDaysSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.WorkingDaysSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WorkingDaySettingsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public WorkingDaySettingsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetWorkingDaysSettings")]
		public async Task<IActionResult> GetWorkingDaysSettings()
		{
			return Ok(await _mediator.Send(new GetWorkingDaysSettingQuery()));
		}

		[HttpPost("UpdateWorkingDaysSetting")]
		public async Task<IActionResult> UpdateWoringDaysSettings(List<WorkingDaysSettingsDto> workingDays)
		{
			return Ok(await _mediator.Send(new UpdateWorkingDaysCommand(workingDays)));
		}
	}
}
