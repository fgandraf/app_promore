using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Requests.Users;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/users")]
public class UserController(UserService service, TokenService tokenService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody]GetUserByLoginRequest model)
    {
        var result = service.GetUserByLoginAsync(model).Result;
        return result.Success ? Ok(tokenService.GenerateToken(result.Value)) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = service.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post([FromBody]CreateUserRequest model)
    {
        var result = service.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("settings")]
    public IActionResult UpdateSettings([FromBody]UpdateUserSettingsRequest model)
    {
        var result = service.UpdateSettingsAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [HttpGet("id/{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = service.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("email/{address}")]
    public IActionResult GetByEmail(string address)
    {
        var result = service.GetByEmailAsync(address).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPut]
    [HttpPut("info")]
    public IActionResult UpdateInfo([FromBody]UpdateUserInfoRequest model)
    {
        var result = service.UpdateInfoAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [HttpDelete("lotfromuser/{userId:int},{lotId}")]
    public IActionResult RemoveLotFromUser(int userId, string lotId)
    {
        var result = service.RemoveLotFromUserAsync(userId, lotId).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
}