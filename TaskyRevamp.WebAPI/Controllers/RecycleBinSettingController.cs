using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecycleBinSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecycleBinSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetRecycleBinSetting")]
        public async Task<ActionResult<CommonApiResponse<string>>> GetRecycleBinSetting()
        {
            return Ok(await _mediator.Send(new GetRecycleBinSettingQuery()));
        }

		[HttpPost("UpdatetRecycleBinSetting")]
		public async Task<ActionResult<CommonApiResponse<string>>> UpdateRecycleBinSetting([FromBody]RecycleBinSettingDto _recycleBinSettingDto)
		{

			return Ok(await _mediator.Send(new UpdateRecycleBinSettingCommand(_recycleBinSettingDto)));
		}

	}
}
