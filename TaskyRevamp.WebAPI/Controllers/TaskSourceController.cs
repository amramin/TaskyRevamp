using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;

using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query;
using TaskyRevamp.Services.TaskSources.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskSourceRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskSourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskSourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpPost("CreateTaskSource")]
    //public async Task<ActionResult<string>> CreateTaskSource([FromBody] TaskSourceDto TaskSourceDto)
    //{
    //    var res = await _mediator.Send(new CreateTaskSourceCommand(TaskSourceDto));



    //    return Ok(res);
    //}


    //[HttpPost("UpdateTaskSource")]
    //public async Task<IActionResult> UpdateTaskSource([FromBody] TaskSourceDto TaskSource)
    //{
    //    return Ok(await _mediator.Send(new UpdateTaskSourceCommand(TaskSource)));
    //}
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    return Ok(await _mediator.Send(new DeleteTaskSourceCommand(Guid.Parse(id))));
    //}

    [HttpGet("GetTaskSourcesForDDL")]
    public async Task<IActionResult> GetTaskSourcesForDDL()
    {

        var all = await _mediator.Send(new GetTaskSourcesQuery());

        return Ok(all);
    }

    //[HttpGet("GetAllTaskSources")]
    //public async Task<IActionResult> GetAllTaskSources(
    //        [FromServices] IOptions<PaginationSettings> paginationSettings,
    //        [FromQuery] int pageNumber = 1,
    //        [FromQuery] int? pageSize = null,
    //        [FromQuery] string sortByColumnName = "CreateDate",
    //        [FromQuery] bool sortAscending = true,
    //        [FromQuery] List<SearchFieldTaskSource> searchFields = null,
    //        [FromQuery] string searchText = null)
    //{

    //    var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
    //    var all = await _mediator.Send(new GetTaskSourcesQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));

    //    return Ok(all);
    //}


    ////

    //     [HttpGet("GetTaskSourcesNoPagnation")]
    //public async Task<IActionResult> GetTaskSourcesNoPagnation(

    //        [FromQuery] List<SearchFieldTaskSource> searchFields = null,
    //        [FromQuery] string searchText = null)
    //{

    //    var all = await _mediator.Send(new GetTaskSourcesNoPagnationQuery(searchFields, searchText));

    //    return Ok(all);
    //}
    ////
    //[HttpGet("GetTaskSourceById/{id}")]
    //public async Task<IActionResult> GetOne(string id)
    //{

    //    var Task = await _mediator.Send(new GetTaskSourceQuery(new Guid(id)));


    //    return Ok(Task);
    //}


}