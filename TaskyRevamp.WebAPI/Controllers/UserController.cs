using DepartmentyRevamp.Services.Departments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;
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



    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {


        var all = await _mediator.Send(new GetUNassignedUsersQuery());

        return Ok(all);
    }

    
        [HttpPost("CreateAssignedUser")]
    public async Task<ActionResult<string>> CreateAssignedUser([FromBody] AssignedUserDto assignedUserDto)
    {
        var res = await _mediator.Send(new CreateAssignedUserCommand(assignedUserDto));



        return Ok(res);
    }
}