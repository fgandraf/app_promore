using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/clients")]
public class ClientController : ControllerBase
{
    private IClientRepository _repository;

    public ClientController(IClientRepository repository)
        => _repository = repository;
    
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var clients = _repository.GetAll().Result;
        return clients.IsNullOrEmpty() ? NotFound() : Ok(clients);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var client = _repository.GetByIdAsync(id).Result;
        return client is null ? NotFound($"Cliente '{id}' não encontrado!") : Ok(client);
    }
    

}