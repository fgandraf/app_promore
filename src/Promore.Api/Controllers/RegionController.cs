using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Contexts.Region;
using Requests = Promore.Core.Contexts.Region.Models.Requests;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/regions")]
public class RegionController : ControllerBase
{
    
    public readonly RegionHandler _handler;
    
    public RegionController(RegionHandler handler)
        => _handler = handler;
    
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _handler.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _handler.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post([FromBody]Requests.CreateRegion model)
    {
        var result = _handler.InsertAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }   
   
    [Authorize(Roles = "admin")]
    [Authorize(Roles = "manager")]
    [HttpPut]
    public IActionResult Update([FromBody]Requests.UpdateRegion model)
    {
        var result = _handler.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _handler.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
}