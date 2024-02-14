using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Contexts.ClientContext;
using Promore.Core.Contexts.ClientContext.UseCases.Update;
using Promore.Core.Contexts.ClientContext.UseCases.Create;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/clients")]
public class ClientController : ControllerBase
{
    private readonly ClientHandler _handler;

    public ClientController(ClientHandler handler)
        => _handler = handler;
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _handler.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("lot/{lotId}")]
    public IActionResult GetAllByLotId(string lotId)
    {
        var result = _handler.GetAllByLotIdAsync(lotId).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _handler.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateClientRequest model)
    {
        var result = _handler.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateClientRequest model)
    {
        var result = _handler.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _handler.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
}