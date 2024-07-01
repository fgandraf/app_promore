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
        
        return Ok(tokenService.GenerateToken(result.Data!));
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var request = new GetAllUsersRequest();
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
    
    [HttpGet("id/{id}")]
    public IActionResult GetById(int id)
    {
        var request = new GetUserByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpGet("email/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var request = new GetUserByEmailRequest { Email = email };
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
    
    [HttpPut("remove-lot")]
    public IActionResult RemoveLotFromUser(RemoveLotFromUserRequest request)
    {
        var result = handler.RemoveLotFromUserAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
}