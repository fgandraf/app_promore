using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core;
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
    public IResult Login(GetUserByLoginRequest request)
    {
        var result = handler.GetUserByLoginAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(tokenService.GenerateToken(result.Data!))
            : TypedResults.BadRequest(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IResult GetAll([FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllUsersRequest { PageNumber = pageNumber, PageSize = pageSize };
        var result = handler.GetAllAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IResult Post(CreateUserRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("settings")]
    public IResult UpdateSettings(UpdateUserSettingsRequest request)
    {
        var result = handler.UpdateSettingsAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpGet("id/{id}")]
    public IResult GetById(int id)
    {
        var request = new GetUserByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpGet("email/{email}")]
    public IResult GetByEmail(string email)
    {
        var request = new GetUserByEmailRequest { Email = email };
        var result = handler.GetByEmailAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpPut]
    [HttpPut("info")]
    public IResult UpdateInfo(UpdateUserInfoRequest request)
    {
        var result = handler.UpdateInfoAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpPut("remove-lot")]
    public IResult RemoveLotFromUser(RemoveLotFromUserRequest request)
    {
        var result = handler.RemoveLotFromUserAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
}