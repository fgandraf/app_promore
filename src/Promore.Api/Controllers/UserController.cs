using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Contexts.UserContext;
using Promore.Core.Contexts.UserContext.UseCases.Create;
using Promore.Core.Contexts.UserContext.UseCases.GetByLogin;
using Promore.Core.Contexts.UserContext.UseCases.UpdateInfo;
using Promore.Core.Contexts.UserContext.UseCases.UpdateSettings;

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
    public IActionResult Login([FromBody]LoginRequest model)
    {
        var result = _handler.GetUserByLoginAsync(model).Result;
        return result.Success ? Ok(_tokenService.GenerateToken(result.Value)) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _handler.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post([FromBody]CreateUserRequest model)
    {
        var result = _handler.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("settings")]
    public IActionResult UpdateSettings([FromBody]UpdateUserSettingsRequest model)
    {
        var result = _handler.UpdateSettingsAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [HttpGet("id/{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _handler.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("email/{address}")]
    public IActionResult GetByEmail(string address)
    {
        var result = _handler.GetByEmailAsync(address).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPut]
    [HttpPut("info")]
    public IActionResult UpdateInfo([FromBody]UpdateUserInfoRequest model)
    {
        var result = _handler.UpdateInfoAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [HttpDelete("lotfromuser/{userId:int},{lotId}")]
    public IActionResult DeleteLotFromUser(int userId, string lotId)
    {
        var result = _handler.DeleteLotFromUserAsync(userId, lotId).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
}