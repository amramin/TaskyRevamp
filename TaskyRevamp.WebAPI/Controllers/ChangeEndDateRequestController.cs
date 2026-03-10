using MailKit.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.ChangeEndDateRequests.Commands;
using TaskyRevamp.Services.ChangeEndDateRequests.Query;

namespace ChangeEndDateRequestRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChangeEndDateRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChangeEndDateRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateChangeEndDateRequest")]
    public async Task<ActionResult<string>> CreateChangeEndDateRequest([FromBody] ChangeEndDateRequestDto ChangeEndDateRequestDto)
    {
        var res = await _mediator.Send(new CreateChangeEndDateRequestCommand(ChangeEndDateRequestDto));



        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateChangeEndDateRequest([FromBody] ChangeEndDateRequestDto ChangeEndDateRequest)
    {
        return Ok(await _mediator.Send(new UpdateChangeEndDateRequestCommand(ChangeEndDateRequest)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteChangeEndDateRequestCommand(Guid.Parse(id))));
    }
    [HttpGet("GetAllChangeEndDateRequests")]
    public async Task<IActionResult> GetAllChangeEndDateRequests([FromServices] IOptions<PaginationSettings> paginationSettings,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int? pageSize = null,
            [FromQuery] string sortByColumnName = "CreateDate",
            [FromQuery] bool sortAscending = true,
            [FromQuery] List<SearchFieldChangeDueDate> searchFields = null,
            [FromQuery] string searchText = null)
    {

        var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
        var all = await _mediator.Send(new GetChangeEndDateRequestsQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));

        return Ok(all);
    }

    [HttpGet("GetAllChangeEndDateRequestsByTaskId/{id}")]
    public async Task<IActionResult> GetAllChangeEndDateRequestsByTaskId(Guid id, [FromServices] IOptions<PaginationSettings> paginationSettings,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int? pageSize = null,
            [FromQuery] string sortByColumnName = "CreateDate",
            [FromQuery] bool sortAscending = true)
    {


        var all = await _mediator.Send(new GetChangeEndDateRequestsQueryByTaskId(id,pageNumber,pageSize,sortByColumnName,sortAscending));

        return Ok(all);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetChangeEndDateRequestQuery(new Guid(id)));


        return Ok(Task);
    }


}