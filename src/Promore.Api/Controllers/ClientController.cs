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
    public IActionResult GetAll(GetAllClientsRequest request)
    {
        var result = handler.GetAllAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpGet("lot")]
    public IActionResult GetAllByLotId(GetAllClientsByLotIdRequest request)
    {
        var result = handler.GetAllByLotIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    
    [HttpGet("id")]
    public IActionResult GetById(GetClientByIdRequest request)
    {
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
    
    [HttpDelete]
    public IActionResult Delete(DeleteClientRequest request)
    {
        var result = handler.DeleteAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
}