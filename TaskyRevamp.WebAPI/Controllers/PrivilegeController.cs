using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.Permissions;
using TaskyRevamp.Services.Permission.Privilege.Command;
using TaskyRevamp.Services.Permission.Privilege.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PrivilegeController(IMediator mediator)
        {
            _mediator = mediator;
        }

		[HttpGet("GetPrivilleges")]
		public async Task<IActionResult> GetPrivilleges(
			[FromServices] IOptions<PaginationSettings> paginationSettings,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int? pageSize = null,
			[FromQuery] string sortByColumnName = "CreateDate",
			[FromQuery] bool sortAscending = true,
			[FromQuery] List<SearchFieldPrivileg> searchFields = null,
			[FromQuery] string searchText = null)
		{

			var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
			var result = await _mediator.Send(new GetPrivilegesQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));
			return Ok(result);
		}

		[HttpGet("GetPrivilegesWithoutPagination")]
		public async Task<IActionResult> GetPrivilegesWithoutPagination()
		{
			return Ok(await _mediator.Send(new GetPrivilegesWithoutPaginationQuery()));
		}

		[HttpGet("GetPrivilegeById/{id}")]
		public async Task<IActionResult> GetPrivilegeById(Guid id)
		{
			return Ok(await _mediator.Send(new GetPrivilegeByIdQuery(id)));
		}
		[HttpGet("GetPrivilegeNames")]
		public async Task<IActionResult> GetPrivilegeNames()
		{
			return Ok(await _mediator.Send(new GetPrivilegeNamesQuery()));
		}

		[HttpGet("GetPrivilegeNameById/{id}")]
		public async Task<IActionResult> GetPrivilegeNameById(Guid id)
		{
			return Ok(await _mediator.Send(new GetPrivilegeNameByIdQuery(id)));
		}

		[HttpPost("CreatePrivilege")]
        public async Task<IActionResult> CreatePrivilege([FromBody] PrivilegeDto privilegeDto)
        {
			return Ok(await _mediator.Send(new CreatePrivilegeCommand(privilegeDto)));
        }

        [HttpPost("UpdatePrivilege")]
        public async Task<IActionResult> UpdatePrivilege([FromBody] PrivilegeDto privilegeDto)
        {
            return Ok(await _mediator.Send(new UpdatePrivilegeCommand(privilegeDto)));
        }

		[HttpGet("CheckPrivilegeIsLinkedWithUsers/{id}")]
		public async Task<IActionResult> CheckPrivilegeIsLinkedWithUsers(Guid id)
		{
			return Ok(await _mediator.Send(new CheckPrivilegeIsLinkedWithUsersCommand(id)));
		}

		[HttpDelete("DeletePrivilege/{id}")]
		public async Task<IActionResult> DeletePrivilege(Guid id)
		{
			return Ok(await _mediator.Send(new DeletePrivilegeCommand(id)));
		}
	}
}
