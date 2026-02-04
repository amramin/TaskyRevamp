using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Account.Commands;
using TaskyRevamp.Services.Account.Query;
using TaskyRevamp.Services.Jobs.ActiveDirectory;
using TaskyRevamp.Services.Jobs.ActiveDirectory.SyncFirstTime;

namespace TaskyRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Authenticate")]
    public async Task<ActionResult> Authenticate([FromBody] UserLoginDto user)
    {
        var token = await _mediator.Send(new AuthenticateCommand(user.Username, user.Password));

        if (token is null)
        {
            return Unauthorized(CommonApiResponse<string>.CreateError("Invalid username or password."));
        }

        return Ok(CommonApiResponse<string>.Create(StatusCodes.Status200OK, token));
    }

    [HttpGet("GetUsers")]
    public async Task<ActionResult<PagedResult<UserDto>>> GetUsers(
            [FromServices] IOptions<PaginationSettings> paginationSettings,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int? pageSize = null)
    {
        var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
        var result = await _mediator.Send(new GetUsersQuery(pageNumber, size));
        return Ok(result);
    }




    [HttpGet("SyncUsers/{LogedInUser}")]
    public async Task<int> SyncUsers(Guid LogedInUser)
    {
        var data = await _mediator.Send(new SyncAllUsersFt(LogedInUser));
        return data;
    }
}