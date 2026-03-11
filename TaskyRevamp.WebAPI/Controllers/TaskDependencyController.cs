using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.TaskDependency.Command;
using TaskyRevamp.Services.Tasks.Commands;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskDependencyController : ControllerBase
	{
		private readonly IMediator _mediator;
		public TaskDependencyController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetDependOnTasks/{taskId}")]
		public async Task<IActionResult> GetDependOnTasks(Guid taskId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortByColumnName = "CreateDate", [FromQuery] bool sortAscending = true)
		{
			return Ok(await _mediator.Send(new GetDependentOnTasksQuery(taskId, pageNumber, pageSize, sortByColumnName, sortAscending)));
		}
		[HttpGet("GetDependentTasks/{taskId}")]
		public async Task<IActionResult> GetDependentTasks(Guid taskId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortByColumnName = "CreateDate", [FromQuery] bool sortAscending = true)
		{
			return Ok(await _mediator.Send(new GetDependentTasksQuery(taskId, pageNumber, pageSize, sortByColumnName, sortAscending)));
		}
	}
}
