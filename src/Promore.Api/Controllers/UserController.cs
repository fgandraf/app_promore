using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Contexts.User;
using Requests = Promore.Core.Contexts.User.Models.Requests;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private readonly UserHandler _handler;
    private readonly TokenService _tokenService;
    
    public UserController(UserHandler handler, TokenService tokenService)
    {
        _handler = handler;
        _tokenService = tokenService;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody]Requests.Login model)
    {
        var result = _handler.GetUserByLoginAsync(model).Result;
        return result.Success ? Ok(_tokenService.GenerateToken(result.Value)) : BadRequest(result.Message);
    }
    
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _handler.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("id/{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _handler.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("email/{email}")]
    public IActionResult GetByEmailAddress(string email)
    {
        var result = _handler.GetByEmailAddressAsync(email).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPut]
    public IActionResult UpdateInfo([FromBody]Requests.UpdateInfoUser model)
    {
        var result = _handler.UpdateInfoAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post([FromBody]Requests.CreateUser model)
    {
        var result = _handler.InsertAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("settings")]
    public IActionResult UpdateSettings([FromBody]Requests.UpdateSettingsUser model)
    {
        var result = _handler.UpdateSettingsAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _handler.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
}