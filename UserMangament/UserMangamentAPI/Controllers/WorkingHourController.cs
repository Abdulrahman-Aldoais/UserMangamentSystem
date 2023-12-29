using Application.Features.WorkingHours.Queries.GitList;
using Microsoft.AspNetCore.Mvc;
using UserMangamentAPI.Base;

namespace UserMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingHourController : AppControllerBase
    {


        [HttpGet("GetWorkingHourList")]
        public async Task<IActionResult> GetWorkingHourList()
        {
            return NewResult(await Mediator.Send(new GetListWorkingHourQuery()));
        }
    }
}
