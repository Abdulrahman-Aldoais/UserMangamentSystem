using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Dtos.GetList;
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
    public class UsersController : BaseController
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("Id"); }

        public UsersController(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IMapper mapper)
        {
            _contextAccessor = httpContextAccessor;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _mapper = mapper;
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


        private async Task<BaseCommandResponse<T>> SendDeleteRequestAsync<T>(string url)
        {
            var response = new BaseCommandResponse<T>();
            HttpResponseMessage responseJson = await _httpClient.DeleteAsync(url);

            if (responseJson.IsSuccessStatusCode)
            {
                //string data = await responseJson.Content.ReadAsStringAsync();
                //response = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseCommandResponse<T>>(data);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Deleted;
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




        public async Task<IActionResult> Index()
        {
            var responseJson = await SendGetRequestAsync<List<GetListUserOutput>>($"https://localhost:7289/api/Users/GetUsersList");
            if (responseJson.Success)
            {
                return View(_mapper.Map<List<GetListUserOutput>>(responseJson.Data));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(CreateUserCommand getUserOutput)
        {

            var response = await SendPostRequestAsync<GetUserOutput>("https://localhost:7289/api/Users/CreateUser", getUserOutput);

            if (response.Success)
            {
                NotifySuccess(response.Message);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return View(getUserOutput);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(DeleteUserCommand userCommand)
        {
            var response = await SendDeleteRequestAsync<GetUserOutput>($"https://localhost:7289/api/Users/Delete/{userCommand.Id}");

            if (response.Success)
            {
                NotifySuccess(response.Message);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return View(userCommand);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(GetUserQuery query)
        {
            if (query.Id == 0) RedirectToAction("Index", "Users");

            var response = await SendGetRequestAsync<GetUserOutput>($"https://localhost:7289/api/Users/GetUser/{query.Id}");

            if (response.Success)
            {
                return View(_mapper.Map<GetUserOutput>(response.Data));
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return RedirectToAction("Index", "Users");
            }
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(GetUserOutput updateUser)
        {
            var updateUserCommand = new UpdateUserCommand
            {
                Id = updateUser.Id,
                Age = updateUser.Age,
                Name = updateUser.Name,
                Phone = updateUser.Phone,
                Email = updateUser.Email,
                UserName = updateUser.UserName,
                IsActive = updateUser.IsActive,

            };

            var response = await SendPostRequestAsync<GetUserOutput>("https://localhost:7289/api/Users/UpdateUser", updateUserCommand);

            if (response.Success)
            {
                NotifySuccess(response.Message);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return View(updateUserCommand);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayUserInformation(GetUserQuery getUser)
        {
            if (getUser.Id == 0) RedirectToAction("Index", "Users");

            var response = await SendGetRequestAsync<GetUserOutput>($"https://localhost:7289/api/Users/GetUserById/{getUser.Id}");

            if (response.Success)
            {
                return View(_mapper.Map<GetUserOutput>(response.Data));
            }
            else
            {
                NotifyError(response.Errors, response.Message);
                return RedirectToAction("Index", "Users");
            }
        }


    }
}