using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Contexts.LotContext;
using Promore.Core.Contexts.LotContext.UseCases.Create;
using Promore.Core.Contexts.LotContext.UseCases.Update;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController : ControllerBase
{
    private readonly LotHandler _handler;

    public LotController(LotHandler handler)
        => _handler = handler;
    
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
    public IActionResult Post([FromBody]CreateLotRequest model)
    {
        var result = _handler.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateLotRequest model)
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