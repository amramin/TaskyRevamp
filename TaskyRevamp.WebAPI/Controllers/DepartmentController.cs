using DepartmentyRevamp.Services.Departments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Departments.Command;
using TaskyRevamp.Services.Departments.Query;
using TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DepartmentRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateDepartment")]
    public async Task<ActionResult<string>> CreateDepartment([FromBody] DepartmentDto DepartmentDto)
    {
        var res = await _mediator.Send(new CreateDepartmentCommand(DepartmentDto));



        return Ok(res);
    }


    [HttpPost("UpdateDepartment")]
    public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDto Department)
    {
        return Ok(await _mediator.Send(new UpdateDepartmentCommand(Department)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteDepartmentCommand(Guid.Parse(id))));
    }
    
        [HttpGet("GetDepartmentsForDDL")]
    public async Task<IActionResult> GetDepartmentsForDDL()
    {

        var all = await _mediator.Send(new GetDepartmentsForDDLQuery());

        return Ok(all);
    }

    [HttpGet("GetAllDepartments")]
    public async Task<IActionResult> GetAllDepartments(
            [FromServices] IOptions<PaginationSettings> paginationSettings,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int? pageSize = null,
            [FromQuery] string sortByColumnName = "CreateDate",
            [FromQuery] bool sortAscending = true,
            [FromQuery] List<SearchFieldDepartment> searchFields = null,
            [FromQuery] string searchText = null)
    {

        var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
        var all = await _mediator.Send(new GetDepartmentsQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText));

        return Ok(all);
    }


    [HttpGet("GetDepartmentById/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetDepartmentQuery(new Guid(id)));


        return Ok(Task);
    }


}