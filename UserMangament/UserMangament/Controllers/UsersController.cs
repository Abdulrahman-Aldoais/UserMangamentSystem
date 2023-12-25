using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Queries.Git;
using Application.Features.Users.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UserMangament.Models;

namespace UserMangament.Controllers
{
    public class UsersController : BaseController
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("Id"); }

        public UsersController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = await Mediator.Send(new GetListUserQuery());
            return View(allUsers.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        //[Route("user/addUser/")]
        public async Task<IActionResult> AddUser(CreateUserCommand getUserOutput)
        {

            var result = await Mediator.Send(new CreateUserCommand
            {
                UserName = getUserOutput.UserName,
                Name = getUserOutput.Name,
                Email = getUserOutput.Email,
                Phone = getUserOutput.Phone,
                CreatedBy = null,
                Age = getUserOutput.Age,

            });
            return await NewResult(result, () =>
            {
                if (result.Success)
                {

                    NotifySuccess(result.Message);
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    NotifyError(result.Errors, result.Message);
                    return View(getUserOutput);
                }
            });
        }

        [HttpGet]
        //[Route("user/edit")]
        public async Task<IActionResult> EditUser(GetUserQuery getUser)
        {
            if (getUser.Id == 0) RedirectToAction("Index", "Users");

            var result = await Mediator.Send(getUser);


            if (result.Success)
            {
                var modle = new GetUserOutput
                {
                    Id = result.Data.Id,
                    Age = result.Data.Age,
                    Name = result.Data.Name,
                    Phone = result.Data.Phone,
                    Email = result.Data.Email,
                    UserName = result.Data.UserName,
                    IsActive = result.Data.IsActive,

                };

                return View(modle);
            }
            else
            {
                NotifyError(result.Errors, result.Message);
                return RedirectToAction("Index", "Users");
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(GetUserOutput updateUser)
        {
            var result = await Mediator.Send(new UpdateUserCommand()
            {
                Id = updateUser.Id,
                Age = updateUser.Age,
                Name = updateUser.Name,
                Phone = updateUser.Phone,
                Email = updateUser.Email,
                UserName = updateUser.UserName,
                IsActive = updateUser.IsActive,

            });

            if (result.Success)
            {
                NotifySuccess(result.Message);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                NotifyError(result.Errors, result.Message);
                return View(updateUser);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayUserInformation(GetUserQuery getUser)
        {
            if (getUser.Id == 0) RedirectToAction("Index", "Users");

            var result = await Mediator.Send(getUser);


            if (result.Success)
            {

                return View(result.Data);
            }
            else
            {
                NotifyError(result.Errors, result.Message);
                return RedirectToAction("Index", "Users");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}