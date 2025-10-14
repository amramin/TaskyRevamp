using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Services.Permission.GeneralModulePrivillege.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GeneralModulePrivilegeController : ControllerBase
	{
		private readonly IMediator _mediator;
		public GeneralModulePrivilegeController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("GetGeneralModules")]
		public async Task<IActionResult> GetGeneralModules()
		{
			return Ok(await _mediator.Send(new GetGeneralModulesQuery()));

		}
	}
}
