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
        //return result.Success ? Ok(result.Value) : BadRequest(result.Message);
        return Ok(); // Apagar
    }
    
    [HttpGet("lot")]
    public IActionResult GetAllByLotId(GetAllClientsByLotIdRequest request)
    {
        var result = handler.GetAllByLotIdAsync(request).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    
    [HttpGet("id")]
    public IActionResult GetById(GetClientByIdRequest request)
    {
        var result = handler.GetClientByIdAsync(request).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Post(CreateClientRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        //return result.Success ? Ok(result.Value) : BadRequest(result.Message);
        return Ok(); // Apagar
    }   
   
    [HttpPut]
    public IActionResult Update(UpdateClientRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        //return result.Success ? Ok() : BadRequest(result.Message);
        return Ok(); //apagar
    }
    
    [HttpDelete]
    public IActionResult Delete(DeleteClientRequest request)
    {
        var result = handler.DeleteAsync(request).Result;
        //return result.Success ? Ok() : BadRequest(result.Message);
        return Ok(); //apagar
    }
    
}