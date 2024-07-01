using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Handlers;
using Promore.Core.Requests.Users;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/users")]
public class UserController(IUserHandler handler, TokenService tokenService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(GetUserByLoginRequest request)
    {
        var result = handler.GetUserByLoginAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll(GetAllUsersRequest request)
    {
        var result = handler.GetAllAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post(CreateUserRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("settings")]
    public IActionResult UpdateSettings(UpdateUserSettingsRequest request)
    {
        var result = handler.UpdateSettingsAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpGet("id")]
    public IActionResult GetById(GetUserByIdRequest request)
    {
        var result = handler.GetByIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpGet("email")]
    public IActionResult GetByEmail(GetUserByEmailRequest request)
    {
        var result = handler.GetByEmailAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpPut]
    [HttpPut("info")]
    public IActionResult UpdateInfo(UpdateUserInfoRequest request)
    {
        var result = handler.UpdateInfoAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpPut("lot-from-user")]
    public IActionResult RemoveLotFromUser(RemoveLotFromUserRequest request)
    {
        var result = handler.RemoveLotFromUserAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
}