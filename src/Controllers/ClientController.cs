using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Models.InputModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Controllers;

[Authorize]
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
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateClientInput model)
    {
        var id = _repository.InsertAsync(model).Result;
        return Ok(id);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateClientInput model)
    {
        var updated = _repository.UpdateAsync(model).Result;
        
        if (!updated)
            return NotFound("Cliente não alterado ou não encontrado!");
        
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.DeleteAsync(id).Result;
        
        if (!deleted)
            return NotFound("Cliente não encontrado!");
        
        return Ok();
    }
    

}