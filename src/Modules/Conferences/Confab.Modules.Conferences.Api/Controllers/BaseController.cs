using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[ApiController]
[Route(ConferencesModule.BasePath + "/[controller]")]
public class BaseController : ControllerBase
{

}