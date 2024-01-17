using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Data;
using PromoreApi.Models;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/clients")]
public class ClientController : ControllerBase
{
    private const string ConnectionString = "Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";
    
    [HttpGet]
    public IActionResult GetAll()
    {
        using var context = new PromoreDataContext(ConnectionString);
        var clients = context
            .Clients
            .AsNoTracking()
            .ToList();

        return clients.IsNullOrEmpty() ? NotFound() : Ok(clients);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var client = context
            .Clients
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        
        return client == null ? NotFound($"Cliente '{id}' não encontrado!") : Ok(client);
    }
    
    
    [HttpPost]
    public IActionResult Post([FromBody]Client model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        context.Clients.Add(model);
        context.SaveChanges();
        
        return Ok();
    }
    
    
    [HttpPut]
    public IActionResult Update([FromBody]Client model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var client = context.Clients.FirstOrDefault(x => x.Id == model.Id);
        
        if (client is null)
            return NotFound("Cliente não encontrado!");

        if (context.Lots.FirstOrDefault(x => x.Id == model.LotId) is null)
            return NotFound("Lote não encontrado!");
        
        client.Name = model.Name;
        client.Cpf = model.Cpf;
        client.Phone = model.Phone;
        client.MothersName = model.MothersName;
        client.BirthdayDate = model.BirthdayDate;
        client.LotId = model.LotId;
        
        context.Update(client);
        context.SaveChanges();

        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var client = context.Clients.FirstOrDefault(x => x.Id == id);
        if (client is null)
            return NotFound("Cliente não encontrado!");
        
        context.Remove(client);
        context.SaveChanges();

        return Ok();
    }

    
}