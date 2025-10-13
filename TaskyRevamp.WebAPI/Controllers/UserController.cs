using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.GeneralDto;

using TaskyRevamp.Dto.Account;
using userRevamp.Services.userCQRS.Query;

namespace UserRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAllUsersByDepartment/{DepartmentId}")]
    public async Task<IActionResult> GetAllUsersByDepartment(string DepartmentId)
    {

        var all = await _mediator.Send(new GetAllUsersByDepartmentQuery(Guid.Parse(DepartmentId)));

        return Ok(all);
    }



}