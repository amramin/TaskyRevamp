using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace TaskyRevamp.WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SendEmailController : ControllerBase
{
    private readonly IMediator _mediator;

    public SendEmailController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpPost]
    public async Task<IActionResult> CreateSendEmail([FromBody] TaskyRevamp.Dto.Email.SendEmailDto SendEmail)
    {
        return Ok(await _mediator.Send(new TaskyRevamp.Services.Email.Command.SendMailCommand(SendEmail)));
    }


}
