using DepartmentyRevamp.Services.Departments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Account.Query;
using TaskyRevamp.Services.Users.Query;
using userRevamp.Services.userCQRS.Query;
using userRevamp.Services.UserS.Query;

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
	[HttpGet("GetUsersByDepartment/{DepartmentId}")]
	public async Task<IActionResult> GetUsersByDepartment(string DepartmentId)
	{

		var all = await _mediator.Send(new GetUsersByDepartmentQuery(Guid.Parse(DepartmentId)));

		return Ok(all);
	}



	[HttpGet("GetAllUsers")]
	public async Task<IActionResult> GetAllUsers()
	{


		var all = await _mediator.Send(new GetUnassignedUsersToDepartmentQuery());

		return Ok(all);
	}


	[HttpPost("CreateAssignedUser")]
	public async Task<ActionResult<string>> CreateAssignedUser([FromBody] AssignedUserDto assignedUserDto)
	{
		var res = await _mediator.Send(new CreateAssignedUserCommand(assignedUserDto));



		return Ok(res);
	}

	[HttpGet("GetUsers")]
	public async Task<List<UserDto>> GetUsers()
	{
		var data = await _mediator.Send(new TaskyRevamp.Services.Users.Query.GetUsersQuery());
		return data;
	}

	[HttpGet("GetUsers/{culture}/{pageSize:int}/{offset:int}")]
	public async Task<SearchableBackendDto<DdlDto>> GetUsers(string Culture,
		int pageSize,
		int offset,
		[FromQuery] string searchValue
	   )
	{
		var data = await _mediator.Send(new GetUsersBySearchValueQuery(searchValue, Culture, pageSize, offset));
		return data;
	}
	[HttpGet("GetSelectedUserDdlById/{id:Guid}")]
	public async Task<DdlDto> GetSelectedUserDdlById(Guid id)
	{
		var data = await _mediator.Send(new GetSelectedUserDdlByIdQuery(id));
		return data;
	}

}