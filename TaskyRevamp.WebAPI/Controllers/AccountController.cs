using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Domain.Exceptions;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;
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
    public async Task<ActionResult<CommonApiResponse<string>>> Authenticate([FromBody] UserLoginDto user)
    {
        var token = await _mediator.Send(new AuthenticateCommand(user.Username, user.Password));

        if (token is null)
        {
            return Unauthorized(CommonApiResponse<string>.CreateError("Invalid username or password."));
        }

        return Ok(CommonApiResponse<string>.Create(StatusCodes.Status200OK, token));
    }
}