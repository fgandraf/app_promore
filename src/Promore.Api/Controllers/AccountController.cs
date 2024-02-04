using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Models.InputModels;
using Promore.Api.Services;
using Promore.Core.Contracts;
using SecureIdentity.Password;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/account")]
public class AccountController : ControllerBase
{
    private IAccountRepository _repository;
    private readonly TokenService _tokenService;

    public AccountController(TokenService tokenService, IAccountRepository repository)
    {
        _tokenService = tokenService;
        _repository = repository;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginInput model)
    {
        var user = _repository.LoginAsync(model).Result;
        
        if (user is null || !user.Active)
            return NotFound($"Usuário '{model.Email}' não encontrado ou não está ativo!");
        
        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return BadRequest("Usuário ou senha inválida!");
        
        var token = _tokenService.GenerateToken(user);
        return Ok(token);
    }

}
