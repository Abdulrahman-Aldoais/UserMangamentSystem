using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;
using Application.Features.Departments.Queries.Git;
using Application.Features.Users.Queries.Git;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace UserMangament.Controllers
{
    public class DepartmentsController : BaseController
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("Id"); }

        public DepartmentsController(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IMapper mapper)
        {

            _contextAccessor = httpContextAccessor;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var responseJson = await SendGetRequestAsync<List<GetListDepartmentOutput>>($"https://localhost:7289/api/Depatrment/GetDepartmentList");
            if (responseJson.Success)
            {
                return View(_mapper.Map<List<GetListDepartmentOutput>>(responseJson.Data));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddDepartment()
        {
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(CreateDepartmentsCommand getDepartmentOutput)
        {


            CreateDepartmentsCommand departmentsCommand = new CreateDepartmentsCommand
            {
                Name = getDepartmentOutput.Name,
            };
            var response = await SendPostRequestAsync<GetDepartmentOutput>("https://localhost:7289/api/Depatrment/CreateDepartment", departmentsCommand);

            if (response.Success)
            {
                NotifySuccess(response.Message);
                return RedirectToAction("Index", "Departments");
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return View(getDepartmentOutput);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartment(GetDepartmentQuery query)
        {
            if (query.Id == 0)
            {
                return RedirectToAction("Index", "Departments");
            }

            var response = await SendGetRequestAsync<GetDepartmentOutput>($"https://localhost:7289/api/Depatrment/GetDepartment/{query.Id}");

            if (response.Success)
            {
                return View(_mapper.Map<GetDepartmentOutput>(response.Data));
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return RedirectToAction("Index", "Departments");
            }
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(GetDepartmentOutput updateDepartment)
        {
            var departmentsCommand = new UpdateDepartmentsCommand
            {
                Id = updateDepartment.Id,
                Name = updateDepartment.Name,
            };

            var response = await SendPostRequestAsync<GetDepartmentOutput>("https://localhost:7289/api/Depatrment/UpdateDepartment", departmentsCommand);

            if (response.Success)
            {
                NotifySuccess(response.Message);
                return RedirectToAction("Index", "Departments");
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return View(updateDepartment);
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


    }
}