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
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UserMangament.Models;

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
            var response = new BaseCommandResponse<List<GetListDepartmentOutput>>();
            HttpResponseMessage responseJson = await _httpClient.GetAsync("https://localhost:7289/api/Depatrment/GetDepartmentList");
            if (responseJson.IsSuccessStatusCode)
            {
                string data = await responseJson.Content.ReadAsStringAsync();
                List<GetListDepartmentOutput> userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<List<GetListDepartmentOutput>>>(data).Data;
                var resultMapp = _mapper.Map<List<GetListDepartmentOutput>>(userInfo);
                response.Data = resultMapp;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;
                return View(resultMapp);

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddDepartment()
        {
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(CreateDepartmentsCommand getUserOutput)
        {

            var response = new BaseCommandResponse<GetDepartmentOutput>();
            CreateDepartmentsCommand departmentsCommand = new CreateDepartmentsCommand
            {
                Name = getUserOutput.Name,
            };
            string data = JsonSerializer.Serialize(departmentsCommand);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseJson = await _httpClient.PostAsync("https://localhost:7289/api/Depatrment/CreateDepartment", content);
            if (responseJson.IsSuccessStatusCode)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;
            }

            return await NewResult(response, () =>
        {
            if (response.Success)
            {

                NotifySuccess(response.Message);
                return RedirectToAction("Index", "Departments");
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return View(getUserOutput);
            }
        });
        }

        //[HttpGet]
        ////[Route("user/edit")]
        //public async Task<IActionResult> EditDepartment(GetDepartmentQuery getDepartment)
        //{
        //    if (getDepartment.Id == 0) RedirectToAction("Index", "Departments");

        //    var response = new BaseCommandResponse<GetDepartmentOutput>();
        //    HttpResponseMessage responseJson = await _httpClient.GetAsync("https://localhost:7289/api/Depatrment/GetDepartmentList");
        //    if (responseJson.IsSuccessStatusCode)
        //    {
        //        string data = await responseJson.Content.ReadAsStringAsync();
        //        GetDepartmentOutput userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<GetDepartmentOutput>>(data).Data;
        //        var resultMapp = _mapper.Map<GetDepartmentOutput>(userInfo);
        //        response.StatusCode = System.Net.HttpStatusCode.OK;
        //        response.Success = true;
        //        response.Message = SharedResourcesKeys.Success;
        //        response.Errors = null;

        //        return View(resultMapp);

        //    }
        //    else
        //    {
        //        var modle = new GetDepartmentOutput
        //        {
        //            Id = resu.Data.Id,
        //            Name = result.Data.Name
        //        };

        //        return View(modle);
        //    }
        //    else
        //    {
        //        NotifyError(result.Errors, result.Message);
        //        return RedirectToAction("Index", "Departments");
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> EditDepartment(GetDepartmentQuery getDepartment)
        {
            if (getDepartment.Id == 0)
            {
                return RedirectToAction("Index", "Departments");
            }
            GetDepartmentQuery getId = new GetDepartmentQuery
            {
                Id = getDepartment.Id,
            };
            var response = new BaseCommandResponse<GetDepartmentOutput>();
            HttpResponseMessage responseJson = await _httpClient.GetAsync("https://localhost:7289/api/Department/GetDepartment/" + getId.Id);

            if (responseJson.IsSuccessStatusCode)
            {
                string data = await responseJson.Content.ReadAsStringAsync();
                BaseCommandResponse<GetDepartmentOutput> result = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<GetDepartmentOutput>>(data);

                GetDepartmentOutput userInfo = result.Data;
                var resultMapped = _mapper.Map<GetDepartmentOutput>(userInfo);

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;

                return View(resultMapped);
            }
            else
            {
                var errorData = await responseJson.Content.ReadAsStringAsync();
                BaseCommandResponse<GetDepartmentOutput> errorResult = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<GetDepartmentOutput>>(errorData);

                NotifyError(errorResult?.Errors, errorResult?.Message);

                return RedirectToAction("Index", "Departments");
            }
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(GetDepartmentOutput updateDepartment)
        {

            var response = new BaseCommandResponse<GetDepartmentOutput>();
            UpdateDepartmentsCommand departmentsCommand = new UpdateDepartmentsCommand
            {
                Name = updateDepartment.Name,
            };
            string data = JsonSerializer.Serialize(departmentsCommand);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseJson = await _httpClient.PostAsync("https://localhost:7289/api/Depatrment/UpdateDepartment", content);
            if (responseJson.IsSuccessStatusCode)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Success;
                response.Errors = null;
            }

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