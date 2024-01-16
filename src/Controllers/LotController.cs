using Microsoft.AspNetCore.Mvc;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/lots")]
public class LotController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
}