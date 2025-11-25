using DepartmentyRevamp.Services.Departments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Account.Query;
using TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query;
using TaskyRevamp.Services.Users.Command;
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

	[HttpGet("GetAllUsersByDepartmentWithPaginationQuery/{DepartmentId}")]
	public async Task<IActionResult> GetAllUsersByDepartmentWithPagination(
			Guid DepartmentId,
			[FromServices] IOptions<PaginationSettings> paginationSettings,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int? pageSize = null,
			[FromQuery] string sortByColumnName = "CreateDate",
			[FromQuery] bool sortAscending = true,
			[FromQuery] List<SearchFieldUserDepartment> searchFields = null,
			[FromQuery] string searchText = null)
	{
		var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
		var result = await _mediator.Send(new GetAllUsersByDepartmentWithPaginationQuery(DepartmentId, pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));
		return Ok(result);
	}

	[HttpGet("GetUsersByPrivilegeWithPaginationQuery/{PrivilegeId}")]
	public async Task<IActionResult> GetUsersByPrivilegeWithPagination(
			Guid PrivilegeId,
			[FromServices] IOptions<PaginationSettings> paginationSettings,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int? pageSize = null,
			[FromQuery] string sortByColumnName = "CreateDate",
			[FromQuery] bool sortAscending = true,
			[FromQuery] List<SearchFieldUserPrivilege> searchFields = null,
			[FromQuery] string searchText = null)
	{
		var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
		var result = await _mediator.Send(new GetAllUsersByPrivilegeQuery(PrivilegeId, pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));
		return Ok(result);
	}

	[HttpGet("GetAllUsers")]
	public async Task<IActionResult> GetAllUsers()
	{


		var all = await _mediator.Send(new GetUnassignedUsersToDepartmentQuery());

		return Ok(all);
	}

	[HttpGet("GetUsersWithPagination")]
	public async Task<IActionResult> GetUsersWithPagination(
			[FromServices] IOptions<PaginationSettings> paginationSettings,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int? pageSize = null,
			[FromQuery] string sortByColumnName = "CreateDate",
			[FromQuery] bool sortAscending = true,
			[FromQuery] List<SearchFieldUser> searchFields = null,
			[FromQuery] string searchText = null)
	{
		var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
		var result = await _mediator.Send(new GetUsersWithPaginationQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));
		return Ok(result);
	}


	[HttpPost("CreateAssignedUser")]
	public async Task<ActionResult<string>> CreateAssignedUser([FromBody] AssignedUserDto assignedUserDto)
	{
		var res = await _mediator.Send(new CreateAssignedUserCommand(assignedUserDto));
		return Ok(res);
	}

	[HttpPost("LinkUser/{departmentId}/{privilegeId}")]
	public async Task<ActionResult<bool>> LinkUser(UserDto user, Guid privilegeId, Guid departmentId)
	{
		var res = await _mediator.Send(new LinkUserCommand(user, departmentId, privilegeId));
		return Ok(res);
	}

	[HttpPost("AssignUsersToPrivilege/{id}")]
	public async Task<ActionResult<string>> AssignUsersToPrivilege([FromBody] List<UserDto> usersDto, Guid id)
	{
		var res = await _mediator.Send(new AssignUsersToPrivilegeCommand(usersDto,id));
		return Ok(res);
	}

	[HttpGet("GetUsers")]
	public async Task<List<UserDto>> GetUsers()
	{
		var data = await _mediator.Send(new TaskyRevamp.Services.Users.Query.GetUsersQuery());
		return data;
	}
	[HttpGet("GetUnAssignedUsersToPrivilege")]
	public async Task<List<UserDto>> GetUnAssignedUsersToPrivilege()
	{
		var data = await _mediator.Send(new GetUnAssignedUserstoPrivilegeQuery());
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

	[HttpGet("GetUserById/{id:Guid}")]
	public async Task<UserDto> GetUserById(Guid id)
	{
		var data = await _mediator.Send(new GetUserByIdQuery(id));
		return data;
	}

}