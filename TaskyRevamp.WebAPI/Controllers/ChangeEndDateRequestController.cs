using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.ChangeEndDateRequest;
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
    [HttpPost("GetAllChangeEndDateRequests")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetChangeEndDateRequestsQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetChangeEndDateRequestQuery(new Guid(id)));


        return Ok(Task);
    }


}