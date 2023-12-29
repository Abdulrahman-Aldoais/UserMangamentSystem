using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Queries.Git;
using Application.Features.Departments.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using UserMangamentAPI.Base;

namespace UserMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepatrmentController : AppControllerBase
    {
        [HttpGet("GetDepartmentList")]
        public async Task<IActionResult> GetDepartmentList()
        {

            return NewResult(await Mediator.Send(new GetDepartmentListQuery()));
        }




        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> AddDepartment(CreateDepartmentsCommand createDepartmentsCommand)
        {
            return NewResult(await Mediator.Send(createDepartmentsCommand));
        }

        [HttpGet("GetDepartment/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            return NewResult(await Mediator.Send(new GetDepartmentQuery() { Id = id }));
        }

        //[HttpGet("GetDepartment/{id}")]
        //public async Task<ActionResult> Edit([FromRoute] int id)
        //{
        //    return NewResult(await Mediator.Send(new GetDepartmentQuery() { Id = id }));
        //}

        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> Edit(UpdateDepartmentsCommand updateDepartmentsCommand)
        {
            return NewResult(await Mediator.Send(updateDepartmentsCommand));
        }


    }
}
