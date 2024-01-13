using Application.Features.Departments.Dtos.GetList;
using Application.Features.Departments.Queries.GitList;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Dtos.Get;
using Application.Features.Employees.Dtos.GetList;
using Application.Features.WorkingHours.Dtos.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
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


        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("Id"); }

        public EmployeesController(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IMapper mapper)
        {
            _contextAccessor = httpContextAccessor;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _mapper = mapper;
        }



        public async Task<IActionResult> Index()
        {
            var responseJson = await SendGetRequestAsync<List<GetEmployeeListOutput>>($"https://localhost:7289/api/Employees/GetEmployeesList");
            if (responseJson.Success)
            {
                return View(_mapper.Map<List<GetEmployeeListOutput>>(responseJson.Data));
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            var currentTime = DateTime.UtcNow;

            if (currentTime > _departmentsCacheExpirationTime || !_cachedDepartments.Any())
            {
                var response = await SendGetRequestAsync<List<GetListDepartmentOutput>>($"https://localhost:7289/api/Depatrment/GetDepartmentList");
                if (response.Success)
                {
                    _cachedDepartments = _mapper.Map<List<GetListDepartmentOutput>>(response.Data);
                    _departmentsCacheExpirationTime = currentTime + CacheDuration;

                }
            }

            if (currentTime > _workingHoursCacheExpirationTime || !_cachedWorkingHours.Any())
            {
                var response = await SendGetRequestAsync<List<GetListWorkingHourOutput>>($"https://localhost:7289/api/WorkingHour/GetWorkingHourList");
                if (response.Success)
                {
                    _cachedWorkingHours = _mapper.Map<List<GetListWorkingHourOutput>>(response.Data);
                    _workingHoursCacheExpirationTime = currentTime + CacheDuration;
                }
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



        private async Task<BaseCommandResponse<T>> SendPostRequestAsync<T>(string url, object data)
        {
            var response = new BaseCommandResponse<T>();
            string jsonData = System.Text.Json.JsonSerializer.Serialize(data);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage responseJson = await _httpClient.PostAsync(url, content);

            if (responseJson.IsSuccessStatusCode)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;
            }
            else
            {
                string errorData = await responseJson.Content.ReadAsStringAsync();
                try
                {
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<T>>(errorData);
                }
                catch (JsonReaderException ex)
                {

                    Console.WriteLine($"خطأ أثناء تحليل JSON: {ex.Message}");
                }
            }

            return response;
        }


        private async Task<BaseCommandResponse<T>> SendGetRequestAsync<T>(string url)
        {
            var response = new BaseCommandResponse<T>();
            HttpResponseMessage responseJson = await _httpClient.GetAsync(url);

            if (responseJson.IsSuccessStatusCode)
            {
                string data = await responseJson.Content.ReadAsStringAsync();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<T>>(data);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;
            }
            else
            {
                var errorData = await responseJson.Content.ReadAsStringAsync();
                try
                {
                    response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<T>>(errorData);
                }
                catch (JsonReaderException ex)
                {

                    Console.WriteLine($"Error while parsing JSON: {ex.Message}");
                }
            }

            return response;
        }


        //private async Task<BaseCommandResponse<T>> SendDeleteRequestAsync<T>(string url)
        //{
        //    var response = new BaseCommandResponse<T>();
        //    HttpResponseMessage responseJson = await _httpClient.DeleteAsync(url);

        //    if (responseJson.IsSuccessStatusCode)
        //    {
        //        //string data = await responseJson.Content.ReadAsStringAsync();
        //        //response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<T>>(data);
        //        response.StatusCode = System.Net.HttpStatusCode.OK;
        //        response.Success = true;
        //        response.Message = SharedResourcesKeys.Deleted;
        //        response.Errors = null;
        //    }
        //    else
        //    {
        //        var errorData = await responseJson.Content.ReadAsStringAsync();
        //        try
        //        {
        //            response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<T>>(errorData);
        //        }
        //        catch (JsonReaderException ex)
        //        {

        //            Console.WriteLine($"Error while parsing JSON: {ex.Message}");
        //        }
        //    }

        //    return response;
        //}




    }
}
