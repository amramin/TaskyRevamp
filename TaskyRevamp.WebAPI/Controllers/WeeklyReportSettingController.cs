using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.WeeklyReportSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.WeeklyReportSettings.Query;
using TaskyRevamp.Services.SystemConfiguration.WorkingDaysSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeeklyReportSettingController : ControllerBase
	{
		private readonly IMediator _mediator;

		public WeeklyReportSettingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetWeeklyReportSettings")]
		public async Task<IActionResult> GetWeeklyReportSettings()
		{
			return Ok(await _mediator.Send(new GetWeeklyReportSettingQuery()));
		}

		[HttpPost("UpdateWeeklyReportSetting")]
		public async Task<IActionResult> UpdateWeekltReportSetting(WeeklyReportSettingsDto weeklyReportSettingsDto)
		{
			return Ok(await _mediator.Send(new UpdateWeeklyReportSettingCommand(weeklyReportSettingsDto)));
		}
	}
}
