using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

[ApiController]
[Route(AgendasModule.BasePath + "/[controller]")]
public class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("Confab API - Conferences module");
}