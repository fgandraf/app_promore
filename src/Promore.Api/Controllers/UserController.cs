using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Requests.Users;
using Promore.Core.Responses;
using Promore.Core.Responses.Regions;
using Promore.Core.Responses.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/users")]
public class UserController(IUserHandler handler, TokenService tokenService) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPost]
    [SwaggerOperation(Summary = "Insere um novo usuário")]
    [ProducesResponseType(typeof(Response<GetUserResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Post(CreateUserRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("settings")]
    [SwaggerOperation(Summary = "Altera os dados de configuração de um usuário")]
    [ProducesResponseType(typeof(Response<UpdateUserSettingsResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult UpdateSettings(UpdateUserSettingsRequest request)
    {
        var result = handler.UpdateSettingsAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpPut]
    [HttpPut("info")]
    [SwaggerOperation(Summary = "Altera os dados de cadastro de um usuário")]
    [ProducesResponseType(typeof(Response<UpdateUserInfoResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult UpdateInfo(UpdateUserInfoRequest request)
    {
        var result = handler.UpdateInfoAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpPut("remove-lot")]
    [SwaggerOperation(Summary = "Remove um lote do usuário")]
    [ProducesResponseType(typeof(Response<RemoveLotFromUserResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult RemoveLotFromUser(RemoveLotFromUserRequest request)
    {
        var result = handler.RemoveLotFromUserAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("id/{id}")]
    [SwaggerOperation(Summary = "Obtém o usuário pelo identificador")]
    [ProducesResponseType(typeof(Response<GetUserResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetById(int id)
    {
        var request = new GetUserByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("email/{email}")]
    [SwaggerOperation(Summary = "Obtém o usuário pelo endereço de email")]
    [ProducesResponseType(typeof(Response<GetUserResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetByEmail(string email)
    {
        var request = new GetUserByEmailRequest { Email = email };
        var result = handler.GetByEmailAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Realiza o login de um usuário")]
    [ProducesResponseType(typeof(UserTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Login(GetUserByLoginRequest request)
    {
        var result = handler.GetUserByLoginAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(new UserTokenResponse(tokenService.GenerateToken(result.Data!)))
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todos os usuários")]
    [ProducesResponseType(typeof(PagedResponse<List<GetUserResponse>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetAll([FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllUsersRequest { PageNumber = pageNumber, PageSize = pageSize };
        var result = handler.GetAllAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }

}