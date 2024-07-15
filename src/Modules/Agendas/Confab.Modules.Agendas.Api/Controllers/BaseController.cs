using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

[ApiController]
[Route(AgendasModule.BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{

}