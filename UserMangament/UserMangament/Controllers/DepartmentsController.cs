using Application.Features.Departments.Queries.GitList;
using Application.Features.Departments.Commands.Create;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Queries.Git;
using Application.Features.Users.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UserMangament.Models;
using Application.Features.Departments.Queries.Git;
using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Commands.Update;

namespace UserMangament.Controllers
{
    public class DepartmentsController : BaseController
    {

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ILogger<UsersController> _logger;
        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("Id"); }

        public DepartmentsController(ILogger<UsersController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = await Mediator.Send(new GetDepartmentListQuery());
            return View(allUsers.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddDepartment()
        {
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        //[Route("department/addDepartment/")]
        public async Task<IActionResult> AddDepartment(CreateDepartmentsCommand getUserOutput)
        {

            var result = await Mediator.Send(new CreateDepartmentsCommand
            {
                Name = getUserOutput.Name,
                CreatedBy = null

            });
            return await NewResult(result, () =>
            {
                if (result.Success)
                {

                    NotifySuccess(result.Message);
                    return RedirectToAction("Index", "Departments");
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
        public async Task<IActionResult> EditDepartment(GetDepartmentQuery getDepartment)
        {
            if (getDepartment.Id == 0) RedirectToAction("Index", "Departments");

            var result = await Mediator.Send(getDepartment);


            if (result.Success)
            {
                var modle = new GetDepartmentOutput
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name
                };

                return View(modle);
            }
            else
            {
                NotifyError(result.Errors, result.Message);
                return RedirectToAction("Index", "Departments");
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(GetDepartmentOutput updateDepartment)
        {
            var result = await Mediator.Send(new UpdateDepartmentsCommand()
            {
                Id = updateDepartment.Id,
                Name = updateDepartment.Name
            });

            if (result.Success)
            {
                NotifySuccess(result.Message);
                return RedirectToAction("Index", "Departments");
            }
            else
            {
                NotifyError(result.Errors, result.Message);
                return View(updateDepartment);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayDepartmentInformation(GetUserQuery getUser)
        {
            if (getUser.Id == 0) RedirectToAction("Index", "Departments");

            var result = await Mediator.Send(getUser);


            if (result.Success)
            {

                return View(result.Data);
            }
            else
            {
                NotifyError(result.Errors, result.Message);
                return RedirectToAction("Index", "Departments");
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