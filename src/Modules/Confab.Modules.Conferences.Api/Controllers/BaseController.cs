using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[ApiController]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    private const string BasePath = "conferences-module";
}