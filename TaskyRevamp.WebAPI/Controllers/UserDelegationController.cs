using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.UserDelegation;
using TaskyRevamp.Services.Departments.Query;
using TaskyRevamp.Services.UserDelegations.Command;
using TaskyRevamp.Services.UserDelegations.Query;
using TaskyRevamp.Services.UserDelegations.Query;


namespace TaskyRevamp.WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserDelegationController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserDelegationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var UserDelegation = await _mediator.Send(new GetUserDelegationQuery(new Guid(id)));


        return Ok(UserDelegation);
    }
    [HttpGet("GetAllUserDelegation")]
    public async Task<IActionResult> GetAllDepartments(
          [FromServices] IOptions<PaginationSettings> paginationSettings,
          [FromQuery] int pageNumber = 1,
          [FromQuery] int? pageSize = null,
          [FromQuery] string sortByColumnName = "CreateDate",
          [FromQuery] bool sortAscending = true,
          [FromQuery] List<SearchFieldDelegation> searchFields = null,
          [FromQuery] string searchText = null)
    {

        var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
        var all = await _mediator.Send(new GetUsersDelegationsQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));

        return Ok(all);
    }


    //

    //[HttpPost("GetAllUserDelegation/{fromuserid}")]
    //public async Task<IActionResult> AllUserDelegation(string fromuserid, [FromBody]PagingParameterModel paging = null)
    //{

    //    var all = await _mediator.Send(new GetUserDelegationsQuery(Guid.Parse(fromuserid), paging));

    //    return Ok(all);
    //}
    [HttpPut]
    public async Task<IActionResult> UpdateUserDelegation([FromBody] UserDelegationDto UserDelegation)
    {
        return Ok(await _mediator.Send(new UpdateUserDelegationCommand(UserDelegation)));
    }
   
    [HttpPost]
    public async Task<IActionResult> CreateUserDelegation([FromBody] UserDelegationDto UserDelegation)
    {
        return Ok(await _mediator.Send(new CreateUserDelegationCommand(UserDelegation)));
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteUserDelegationCommand(Guid.Parse(id))));
    }
  

 

}
