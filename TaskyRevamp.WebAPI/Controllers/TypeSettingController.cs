using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query;
using TaskyRevamp.Services.SystemConfiguration.TypeConfiguration.Command;
using TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command;
using TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query;
using TaskyRevamp.Services.Tasks.Commands;
using TaskyRevamp.Services.TaskTypes.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeSettingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TypeSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetTypeSettings")]
        public async Task<IActionResult> GetTypeSettings(
            [FromServices] IOptions<PaginationSettings> paginationSettings,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int? pageSize = null,
            [FromQuery] string sortByColumnName = "CreateDate",
            [FromQuery] bool sortAscending = true,
            [FromQuery] List<SearchField> searchFields = null,
            [FromQuery] string searchText = null)
        {
            var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
            var result = await _mediator.Send(new GetTypeConfigurationQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));
            return Ok(result);
        }

        [HttpGet("GetTypeSettingById/{id}")]
        public async Task<IActionResult> GetTypeSettingById(Guid id)
        {
            return Ok(await _mediator.Send(new GetTypeByIdQuery(id)));
        }


        [HttpGet("GetTaskTypesForDDL")]
        public async Task<IActionResult> GetTaskTypesForDDL()
        {

            var all = await _mediator.Send(new GetTaskTypesQuery());

            return Ok(all);
        }
        [HttpPost("RetriveType")]
        public async Task<IActionResult> RetriveType(TypeDto Type)
        {
            return Ok(await _mediator.Send(new RetriveTypeCommand(Type)));
        }
        [HttpPost("CheckRelatedComplatedTaskitemType")]
        public async Task<IActionResult> CheckRelatedComplatedTaskitemType(TypeDto Type)
        {
            return Ok(await _mediator.Send(new CheckRelatedComplatedTaskitemTypeQuery(Type)));
        }
        [HttpPost("CreateType")]
        public async Task<IActionResult> CreateType(TypeDto type)
        {
            return Ok(await _mediator.Send(new CreateTypeCommand(type)));
        }

        [HttpPost("UpdateType")]
        public async Task<IActionResult> UpdateType(TypeDto type)
        {
            return Ok(await _mediator.Send(new UpdateTypeCommand(type)));
        }

        [HttpDelete("DeleteType/{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTypeCommand(id)));
        }
    }
}
