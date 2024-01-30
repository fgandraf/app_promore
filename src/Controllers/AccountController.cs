
using Microsoft.AspNetCore.Mvc;
using PromoreApi.Repositories.Contracts;
using PromoreApi.ViewModels;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/account")]
public class AccountController : ControllerBase
{
    private IAccountRepository _repository;

    public AccountController(IAccountRepository repository)
        => _repository = repository;
    
    
    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginVO model)
    {
        var logged = _repository.LoginAsync(model).Result;
        return logged ? Ok("Usuário logado!") : NotFound($"Usuário '{model.Email}' não encontrado ou não está ativo!");
    }

}
