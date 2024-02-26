using Microsoft.AspNetCore.Mvc;

namespace Gravity.Express.API.Controllers;

[ApiController]
[Route("api/{tenant}/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
}

