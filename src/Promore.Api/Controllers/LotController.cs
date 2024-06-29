using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Requests.Lots;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController(LotService service) : ControllerBase
{
    [HttpGet("status/{regionId:int}")]
    public IActionResult GetStatusByRegion(int regionId)
    {
        var result = service.GetStatusByRegionAsync(regionId).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var result = service.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateLotRequest model)
    {
        var result = service.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateLotRequest model)
    {
        var result = service.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var result = service.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
}