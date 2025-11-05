using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.SystemIdentityConfiguration.Command;
using TaskyRevamp.Services.SystemConfiguration.SystemIdentityConfiguration.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SystemIdentityController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SystemIdentityController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetSystemIdentitySetting")]
		public async Task<IActionResult> GetSystemIdentitySetting()
		{
			return Ok(await _mediator.Send(new GetSystemIdentityQuery()));
		}

		[HttpPost("UpdateSystemIdentitySetting")]
		public async Task<IActionResult> UpdateSystemIdentitySetting(SystemIdentityDto _systemIdentityDto)
		{
			return Ok(await _mediator.Send(new UpdateSystemIdentityCommand(_systemIdentityDto)));
		}
	}
}
