using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Contexts.Lot;
using Requests = Promore.Core.Contexts.Lot.Models.Requests;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController : ControllerBase
{
    private readonly LotHandler _handler;

    public LotController(LotHandler handler)
        => _handler = handler;
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _handler.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("status/{regionId:int}")]
    public IActionResult GetStatusByRegion(int regionId)
    {
        var result = _handler.GetStatusByRegionAsync(regionId).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var result = _handler.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody]Requests.CreateLot model)
    {
        var result = _handler.InsertAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]Requests.UpdateLot model)
    {
        var result = _handler.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var result = _handler.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
}