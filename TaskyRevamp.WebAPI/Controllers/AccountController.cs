using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Services.Account.Commands;

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
    public async Task<ActionResult<string>> Authenticate([FromBody] UserLoginDto User)
    {
        var token = await _mediator.Send(new AuthenticateCommand(User.Username, User.Password));
        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}