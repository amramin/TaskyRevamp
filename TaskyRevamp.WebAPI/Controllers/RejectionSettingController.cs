using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.RejectionSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.RejectionSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RejectionSettingController : ControllerBase
	{
		private readonly IMediator _mediator;

		public RejectionSettingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetRejectionSetting")]
		public async Task<ActionResult> GetRejectionSetting()
		{
			return Ok(await _mediator.Send(new GetRejectionSettingsQuery()));
		}

		[HttpPost("UpdateRejectionSetting")]
		public async Task<ActionResult> UpdateRejectionSetting([FromBody] RejectionSettingsDto _RejectionSettingsDto)
		{
			return Ok(await _mediator.Send(new UpdateRejectionSettingCommand(_RejectionSettingsDto)));
		}
	}
}
