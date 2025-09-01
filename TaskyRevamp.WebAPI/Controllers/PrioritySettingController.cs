using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrioritySettingController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PrioritySettingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetPrioritySettings")]
		public async Task<IActionResult> GetPriorities()
		{
			return Ok(await _mediator.Send(new GetProrityQuery()));
		}

		[HttpPost("AddPriority")]
		public async Task<IActionResult> AddPriority([FromBody] PriorityDto priorityDto)
		{
			return Ok(await _mediator.Send(new CreatePriorityCommand(priorityDto)));
		}

		[HttpPost("UpdatePriority")]
		public async Task<IActionResult> UpdatePriority([FromBody] PriorityDto priorityDto)
		{
			return Ok(await _mediator.Send(new UpdateProrityCommand(priorityDto)));
		}

		[HttpDelete("DeletePriority/{id}")]
		public async Task<IActionResult> DeletePriority(Guid id)
		{
			return Ok(await _mediator.Send(new DeletePriorityCommand(new PriorityDto { Id = id })));
		}
	}
}
