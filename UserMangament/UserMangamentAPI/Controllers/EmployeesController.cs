using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Queries.Git;
using Application.Features.Employees.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using UserMangamentAPI.Base;

namespace UserMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : AppControllerBase
    {


        [HttpGet("GetEmployeesList")]
        public async Task<IActionResult> GetEmployeesList()
        {
            return NewResult(await Mediator.Send(new GetEmployeeListQuery()));
        }




        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> AddEmployee(CreateEmployesCommand createEmployes)
        {
            return NewResult(await Mediator.Send(createEmployes));
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return NewResult(await Mediator.Send(new GetEmployeeQuery() { Id = id }));
        }
        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //    return NewResult(await Mediator.Send(new DeleteEmpCommand() { Id = id }));
        //}

        [HttpGet("GetEmployee/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            return NewResult(await Mediator.Send(new GetEmployeeQuery() { Id = id }));
        }


        //[HttpPost("UpdateUser")]
        //public async Task<IActionResult> Edit(UpdateEmployeeCommand updateUserCommand)
        //{
        //    return NewResult(await Mediator.Send(updateUserCommand));
        //}


    }
}
