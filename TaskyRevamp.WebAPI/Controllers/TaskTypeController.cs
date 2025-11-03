using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;

using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query;
using TaskyRevamp.Services.TaskTypes.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskTypeRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpPost("CreateTaskType")]
    //public async Task<ActionResult<string>> CreateTaskType([FromBody] TaskTypeDto TaskTypeDto)
    //{
    //    var res = await _mediator.Send(new CreateTaskTypeCommand(TaskTypeDto));



    //    return Ok(res);
    //}


    //[HttpPost("UpdateTaskType")]
    //public async Task<IActionResult> UpdateTaskType([FromBody] TaskTypeDto TaskType)
    //{
    //    return Ok(await _mediator.Send(new UpdateTaskTypeCommand(TaskType)));
    //}
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    return Ok(await _mediator.Send(new DeleteTaskTypeCommand(Guid.Parse(id))));
    //}

    [HttpGet("GetTaskTypesForDDL")]
    public async Task<IActionResult> GetTaskTypesForDDL()
    {

        var all = await _mediator.Send(new GetTaskTypesQuery());

        return Ok(all);
    }

    //[HttpGet("GetAllTaskTypes")]
    //public async Task<IActionResult> GetAllTaskTypes(
    //        [FromServices] IOptions<PaginationSettings> paginationSettings,
    //        [FromQuery] int pageNumber = 1,
    //        [FromQuery] int? pageSize = null,
    //        [FromQuery] string sortByColumnName = "CreateDate",
    //        [FromQuery] bool sortAscending = true,
    //        [FromQuery] List<SearchFieldTaskType> searchFields = null,
    //        [FromQuery] string searchText = null)
    //{

    //    var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
    //    var all = await _mediator.Send(new GetTaskTypesQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));

    //    return Ok(all);
    //}


    ////

    //     [HttpGet("GetTaskTypesNoPagnation")]
    //public async Task<IActionResult> GetTaskTypesNoPagnation(

    //        [FromQuery] List<SearchFieldTaskType> searchFields = null,
    //        [FromQuery] string searchText = null)
    //{

    //    var all = await _mediator.Send(new GetTaskTypesNoPagnationQuery(searchFields, searchText));

    //    return Ok(all);
    //}
    ////
    //[HttpGet("GetTaskTypeById/{id}")]
    //public async Task<IActionResult> GetOne(string id)
    //{

    //    var Task = await _mediator.Send(new GetTaskTypeQuery(new Guid(id)));


    //    return Ok(Task);
    //}


}