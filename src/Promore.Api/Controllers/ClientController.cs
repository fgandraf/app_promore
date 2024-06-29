using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Requests.Clients;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/clients")]
public class ClientController(ClientService service) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = service.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("lot/{lotId}")]
    public IActionResult GetAllByLotId(string lotId)
    {
        var result = service.GetAllByLotIdAsync(lotId).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = service.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateClientRequest model)
    {
        var result = service.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateClientRequest model)
    {
        var result = service.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = service.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
}