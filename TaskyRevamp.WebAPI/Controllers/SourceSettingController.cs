using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Account.Query;
using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command;
using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SourceSettingController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SourceSettingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetSourceSettings")]
		public async Task<IActionResult> GetSourceSettings(
			[FromServices] IOptions<PaginationSettings> paginationSettings,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int? pageSize = null)
		{
			var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
			var result = await _mediator.Send(new GetSourceConfigurationQuery(pageNumber, size));
			return Ok(result);
		}

		[HttpGet("GetSourceSettingById/{id}")]
		public async Task<IActionResult> GetSourceSettingById(Guid id)
		{
			return Ok(await _mediator.Send(new GetSourceByIdQuery(id)));
		}

		[HttpPost("CreateSource")]
		public async Task<IActionResult> CreateSource(SourceDto source)
		{
			return Ok(await _mediator.Send(new CreateSourceCommand(source)));
		}

		[HttpPost("UpdateSource")]
		public async Task<IActionResult> UpdateSource(SourceDto source)
		{
			return Ok(await _mediator.Send(new UpdateSourceCommand(source)));
		}

		[HttpDelete("DeleteSource/{id}")]
		public async Task<IActionResult> DeleteSource(Guid id)
		{
			return Ok(await _mediator.Send(new DeleteSourceCommand(id)));
		}
	}
}
