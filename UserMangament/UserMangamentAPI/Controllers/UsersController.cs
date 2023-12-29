using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.Git;
using Application.Features.Users.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using UserMangamentAPI.Base;

namespace UserMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AppControllerBase
    {
        [HttpGet("GetUsersList")]
        public async Task<IActionResult> GetUsersList()
        {

            return NewResult(await Mediator.Send(new GetListUserQuery()));
        }




        [HttpPost("CreateUser")]
        public async Task<IActionResult> AddUser(CreateUserCommand createUserCommand)
        {
            return NewResult(await Mediator.Send(createUserCommand));
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return NewResult(await Mediator.Send(new GetUserQuery() { Id = id }));
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return NewResult(await Mediator.Send(new DeleteUserCommand() { Id = id }));
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            return NewResult(await Mediator.Send(new GetUserQuery() { Id = id }));
        }


        [HttpPost("UpdateUser")]
        public async Task<IActionResult> Edit(UpdateUserCommand updateUserCommand)
        {
            return NewResult(await Mediator.Send(updateUserCommand));
        }




    }
}
