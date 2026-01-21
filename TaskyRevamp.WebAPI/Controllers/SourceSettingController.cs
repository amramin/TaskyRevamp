using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Account.Query;
using TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query;
using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command;
using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query;
using TaskyRevamp.Services.TaskSources.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceSettingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SourceSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetSourceSettings")]
        public async Task<IActionResult> GetSourceSettings(
            [FromServices] IOptions<PaginationSettings> paginationSettings,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int? pageSize = null,
            [FromQuery] string sortByColumnName = "CreateDate",
            [FromQuery] bool sortAscending = true,
            [FromQuery] List<SearchField> searchFields = null,
            [FromQuery] string searchText = null)
        {

            var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
            var result = await _mediator.Send(new GetSourceConfigurationQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));
            return Ok(result);
        }
        [HttpGet("GetSourceSettingsView")]
        public async Task<IActionResult> GetSourceSettingsView(
        [FromServices] IOptions<PaginationSettings> paginationSettings,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int? pageSize = null, bool IsCompleted = false)
        {

            var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
            var result = await _mediator.Send(new GetSourceConfigurationQueryView(pageNumber, size,IsCompleted));
            return Ok(result);
        }
        [HttpGet("GetSourceWithoutPagination")]
        public async Task<IActionResult> GetSourceWithoutPagination()
        {
            return Ok(await _mediator.Send(new GetSourcesWithoutPaginationQuery()));
        }

        [HttpGet("GetSourceSettingById/{id}")]
        public async Task<IActionResult> GetSourceSettingById(Guid id)
        {
            return Ok(await _mediator.Send(new GetSourceByIdQuery(id)));
        }

        [HttpPost("CreateSource")]
        public async Task<IActionResult> CreateSource(SourceDto source)
        {
            return Ok(await _mediator.Send(new CreateSourceCommand(source)));
        }


        [HttpPost("UpdateSource")]
        public async Task<IActionResult> UpdateSource(SourceDto source)
        {
            return Ok(await _mediator.Send(new UpdateSourceCommand(source)));
        }

        [HttpPost("RetriveSource")]
        public async Task<IActionResult> RetriveSource(SourceDto source)
        {
            return Ok(await _mediator.Send(new RetriveSourceCommand(source)));
        }
        [HttpPost("CheckRelatedComplatedTaskitemSource")]
        public async Task<IActionResult> CheckRelatedComplatedTaskitemSource(SourceDto source)
        {
            return Ok(await _mediator.Send(new CheckRelatedComplatedTaskitemSourceQuery(source)));
        }
        [HttpDelete("DeleteSource/{id}")]
        public async Task<IActionResult> DeleteSource(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteSourceCommand(id)));
        }

        [HttpGet("GetTaskSourcesForDDL")]
        public async Task<IActionResult> GetTaskSourcesForDDL()
        {

            var all = await _mediator.Send(new GetTaskSourcesQuery());

            return Ok(all);
        }
    }
}
