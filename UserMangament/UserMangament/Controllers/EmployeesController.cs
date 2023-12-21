using Application.Features.Departments.Dtos.GetList;
using Application.Features.Departments.Queries.GitList;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Dtos.Get;
using Application.Features.Employees.Queries.GitList;
using Application.Features.WorkingHours.Dtos.GetList;
using Application.Features.WorkingHours.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using UserMangament.Models;

namespace UserMangament.Controllers
{
    public class EmployeesController : BaseController
    {
        private static List<GetListDepartmentOutput> _cachedDepartments = new List<GetListDepartmentOutput>();
        private static List<GetListWorkingHourOutput> _cachedWorkingHours = new List<GetListWorkingHourOutput>();
        private static DateTime _departmentsCacheExpirationTime = DateTime.MinValue;
        private static DateTime _workingHoursCacheExpirationTime = DateTime.MinValue;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);
        private readonly ILogger<EmployeesController> _logger;

        public async Task<IActionResult> Index()
        {
            var allUsers = await Mediator.Send(new GetEmployeeListQuery());
            return View(allUsers.Data);
        }


        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            var currentTime = DateTime.UtcNow;

            if (currentTime > _departmentsCacheExpirationTime || !_cachedDepartments.Any())
            {
                var getListDepartment = await Mediator.Send(new GetDepartmentListQuery());
                _cachedDepartments = getListDepartment.Data;
                _departmentsCacheExpirationTime = currentTime + CacheDuration;
            }

            if (currentTime > _workingHoursCacheExpirationTime || !_cachedWorkingHours.Any())
            {
                var getListWorkinHour = await Mediator.Send(new GetListWorkingHourQuery());
                _cachedWorkingHours = getListWorkinHour.Data;
                _workingHoursCacheExpirationTime = currentTime + CacheDuration;
            }

            var model = new EmployeeCreateViewModel
            {
                employeeOutput = new GetEmployeeOutput(),
                departmentListOutput = _cachedDepartments.Select(department => new GetListDepartmentOutput
                {
                    Id = department.Id,
                    Name = department.Name,
                }).ToList(),
                workinHourListOutput = _cachedWorkingHours,
            };

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(EmployeeCreateViewModel model)
        {
            var createEmployesCommand = new CreateEmployesCommand
            {
                Name = model.employeeOutput.Name,
                JobTitle = model.employeeOutput.JobTitle,
                Phone = model.employeeOutput.Phone,
                Salary = model.employeeOutput.Salary,
                HireDate = model.employeeOutput.HireDate,
                JobDescription = model.employeeOutput.JobDescription,
                WorkingHourId = model.employeeOutput.WorkingHourId,
                CreatedDate = DateTime.Now,
                CreateBy = 1,
                AccountCancellationStatusBy = 1,
                DepartmentId = model.employeeOutput.DepartmentId,
                //JobId = model.employeeOutput.JobId,
                JobId = 1,
            };


            var result = await Mediator.Send(createEmployesCommand);

            if (result.Success)
            {
                NotifySuccess(result.Message);
                return RedirectToAction("Index", "Employees");
            }
            else
            {

                NotifyError(result.Errors, result.Message);

                var modell = new EmployeeCreateViewModel
                {
                    employeeOutput = new GetEmployeeOutput(),
                    departmentListOutput = _cachedDepartments,
                    workinHourListOutput = _cachedWorkingHours
                };

                return View(modell);
            }
        }
    }
}
