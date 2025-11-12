using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Departments.Query;
using TaskyRevamp.Services.Tasks.Commands;
using TaskyRevamp.Services.Tasks.Query;

namespace TaskyRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTask")]
    public async Task<ActionResult<string>> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        var res = await _mediator.Send(new CreateTaskCommand(taskDto));



        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] CreateTaskDto Task)
    {
        return Ok(await _mediator.Send(new UpdateTaskCommand(Task)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskCommand(Guid.Parse(id))));
    }

    [HttpGet("GetAllTask")]
    public async Task<IActionResult> GetAllTask(
         [FromServices] IOptions<PaginationSettings> paginationSettings,
         [FromQuery] int pageNumber = 1,
         [FromQuery] int? pageSize = null,
         [FromQuery] string sortByColumnName = "CreateDate",
         [FromQuery] bool sortAscending = true,
         [FromQuery] List<SearchFieldTask> searchFields = null,
         [FromQuery] string searchText = null)
    {

        var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
        var all = await _mediator.Send(new GetTasksQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));

        return Ok(all);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetTaskQuery(new Guid(id)));


        return Ok(Task);
    }

    [HttpGet("GetTasksForDDL")]
    public async Task<IActionResult> GetTasksForDDL()
    {

        var all = await _mediator.Send(new GetTasksForDDLQuery());

        return Ok(all);
    }


}