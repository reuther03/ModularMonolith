using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.Api.Controllers
{
    [ApiController]
    [Route(TicketsModule.BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}