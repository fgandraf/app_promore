using Microsoft.AspNetCore.Mvc;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
    
}