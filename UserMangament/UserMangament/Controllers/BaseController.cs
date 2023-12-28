using Core.Application.Responses;
using Domain.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using UserMangament.Models;

namespace UserMangament.Controllers
{
    public class BaseController : Controller
    {
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator? _mediator;


        #region Actions


        public async Task<IActionResult> NewResult<T>(
            BaseCommandResponse<T> response, Func<IActionResult> value)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    NotifySuccess(SharedResourcesKeys.Success);
                    return await Task.FromResult(value.Invoke());
                case HttpStatusCode.Created:
                    NotifySuccess(SharedResourcesKeys.Created);
                    return await Task.FromResult(value.Invoke());
                case HttpStatusCode.Unauthorized:
                    NotifyError(response.Errors, SharedResourcesKeys.UnAuthorized);
                    return await Task.FromResult(value.Invoke());
                case HttpStatusCode.BadRequest:
                    NotifyError(response.Errors, SharedResourcesKeys.BadRequest);
                    return await Task.FromResult(value.Invoke());
                case HttpStatusCode.NotFound:
                    NotifyError(response.Errors, SharedResourcesKeys.NotFound);
                    return await Task.FromResult(value.Invoke()); ;
                case HttpStatusCode.Accepted:
                    NotifySuccess("Success message");
                    return await Task.FromResult(value.Invoke());
                case HttpStatusCode.UnprocessableEntity:
                    NotifyError(response.Errors, SharedResourcesKeys.UnprocessableEntity);
                    return await Task.FromResult(value.Invoke());
                default:
                    NotifyError(response.Errors, SharedResourcesKeys.UserNameIsNotExist);
                    return await Task.FromResult(value.Invoke());
            }
        }

        #endregion

        public void NotifySuccess(string successMessage)
        {
            var msg = new
            {
                message = successMessage,
                title = "نظام إدارة المستخدمين - شركة ثروات",
                icon = NotificationType.success.ToString(),
                type = NotificationType.success.ToString(),
                provider = GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }


        //public void NotifyError(List<string>? errorMessage, string? additionalError)
        //{
        //    errorMessage.Add(additionalError); // إضافة النص المعين إلى القائمة

        //    var msg = new
        //    {
        //        message = JsonConvert.SerializeObject(errorMessage),
        //        title = "نظام إدارة المستخدمين - شركة ثروات",
        //        icon = NotificationType.error.ToString(),
        //        type = NotificationType.error.ToString(),
        //        provider = GetProvider()
        //    };

        //    TempData["Message"] = JsonConvert.SerializeObject(msg);
        //}


        public void NotifyError(List<string>? errorMessage, string? additionalError)
        {
            errorMessage ??= new List<string>();

            if (additionalError != null)
            {
                errorMessage.Add(additionalError);
            }

            var msg = new
            {
                message = JsonConvert.SerializeObject(errorMessage),
                title = "نظام إدارة المستخدمين - شركة ثروات",
                icon = NotificationType.error.ToString(),
                type = NotificationType.error.ToString(),
                provider = GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }




        private string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProvider"];

            return value;
        }
    }
}
