using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Query;
using TaskyRevamp.Services.SystemConfiguration.RejectionSettings.Command;

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
		public async Task<ActionResult<CommonApiResponse<string>>> GetRejectionSetting()
		{
			return Ok(await _mediator.Send(new GetRecycleBinSettingQuery()));
		}

		[HttpPost("UpdateRejectionSetting")]
		public async Task<ActionResult<CommonApiResponse<string>>> UpdateRejectionSetting([FromBody] RejectionSettingsDto _RejectionSettingsDto)
		{
			return Ok(await _mediator.Send(new UpdateRejectionSettingCommand(_RejectionSettingsDto)));
		}
	}
}
