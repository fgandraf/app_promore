using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private IUserRepository _repository;
    public UserController(IUserRepository repository)
        => _repository = repository;
    
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _repository.GetAll().Result;
        return users.IsNullOrEmpty() ? NotFound() : Ok(users);
    }
    
    [HttpGet("id/{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = _repository.GetByIdAsync(id).Result;
        return user is null ? NotFound($"Usuário '{id}' não encontrado!") : Ok(user);
    }
    
    [HttpGet("email/{email}")]
    public IActionResult GetByEmailAddress(string email)
    {
        var user = _repository.GetByEmailAddress(email).Result;
        return user is null ? NotFound($"Usuário '{email}' não encontrado!") : Ok(user);
    }
}