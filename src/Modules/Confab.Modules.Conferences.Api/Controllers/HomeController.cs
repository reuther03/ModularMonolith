using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[ApiController]
[Route("conferences-module")]
internal class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("Confab API - Conferences module");
}