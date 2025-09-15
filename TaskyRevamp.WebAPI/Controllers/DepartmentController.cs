using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Departments.Command;
using TaskyRevamp.Services.Departments.Query;

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


    [HttpPut]
    public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDto Department)
    {
        return Ok(await _mediator.Send(new UpdateDepartmentCommand(Department)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteDepartmentCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllDepartments")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetDepartmentsQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetDepartmentQuery(new Guid(id)));


        return Ok(Task);
    }


}