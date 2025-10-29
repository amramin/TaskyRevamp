using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Services.Permission.RepotModule.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportModuleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportModuleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetReportModules")]
        public async Task<IActionResult> GetReportModules()
        {
            return Ok(await _mediator.Send(new GetReportModulesQuery()));

        }
    }
}
