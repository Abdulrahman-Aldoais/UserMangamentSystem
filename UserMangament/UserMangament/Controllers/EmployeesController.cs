using Application.Features.Departments.Dtos.GetList;
using Application.Features.Departments.Queries.Git;
using Application.Features.Departments.Queries.GitList;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Dtos.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserMangament.Models;

namespace UserMangament.Controllers
{
    public class EmployeesController : BaseController
    {
        private static List<GetListDepartmentOutput> _cachedDepartments;
        private static DateTime _cacheExpirationTime = DateTime.MinValue;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);
        private readonly ILogger<EmployeesController> _logger;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            if (_cachedDepartments == null || DateTime.UtcNow > _cacheExpirationTime)
            {
                var getListDepartment = await Mediator.Send(new GetDepartmentListQuery());
                
                _cachedDepartments = getListDepartment.Data;
                List<GetListDepartmentOutput> convertedDepartments = _cachedDepartments
                    .Select(department => new GetListDepartmentOutput
                    {

                        Id = department.Id,
                        Name = department.Name,
                    })
                    .ToList();

                _cacheExpirationTime = DateTime.UtcNow + CacheDuration;

                var model = new EmployeeCreateViewModel
                {
                    employeeOutput = new GetEmployeeOutput(),
                    departmentOutput = convertedDepartments
                };

                return View(model);
            }


            var cachedModel = new EmployeeCreateViewModel
            {
                employeeOutput = new GetEmployeeOutput(),
                departmentOutput = _cachedDepartments
            };

            return View(cachedModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("student/addStudent")]
        public async Task<IActionResult> AddEmployee(EmployeeCreateViewModel model)
        {
            var createStudentCommand = new CreateEmployesCommand
            {
               
            };


            var result = await Mediator.Send(createStudentCommand);

            if (result.Success)
            {
                NotifySuccess(result.Message);
                return RedirectToAction("Index", "Employees");
            }
            else
            {

                NotifyError(result.Errors,result.Message);

                var modell = new EmployeeCreateViewModel
                {
                    employeeOutput = new GetEmployeeOutput(),
                    departmentOutput = _cachedDepartments
                };

                return View(modell);
            }
        }
    }
}
