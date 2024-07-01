using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Handlers;
using Promore.Core.Requests.Clients;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/clients")]
public class ClientController(IClientHandler handler) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var request = new GetAllClientsRequest();
        var result = handler.GetAllAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpGet("lot/{lotId}")]
    public IActionResult GetAllByLotId(int lotId)
    {
        var request = new GetAllClientsByLotIdRequest { LotId = lotId };
        var result = handler.GetAllByLotIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    
    [HttpGet("id/{id}")]
    public IActionResult GetById(int id)
    {
        var request = new GetClientByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post(CreateClientRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }   
   
    [HttpPut]
    public IActionResult Update(UpdateClientRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var request = new DeleteClientRequest { Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
}