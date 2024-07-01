using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Handlers;
using Promore.Core.Requests.Lots;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController(ILotHandler handler) : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatusByRegion(GetLotsStatusByRegionIdRequest request)
    {
        var result = handler.GetStatusByRegionAsync(request).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpGet("id")]
    public IActionResult GetById(GetLotByIdRequest request)
    {
        var result = handler.GetByIdAsync(request).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [HttpPost]
    public IActionResult Post(CreateLotRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        //return result.Success ? Ok(result.Value) : BadRequest(result.Message);
        return Ok(); // Apagar
    }   
   
    [HttpPut]
    public IActionResult Update(UpdateLotRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        //return result.Success ? Ok() : BadRequest(result.Message);
        return Ok(); // Apagar
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete]
    public IActionResult Delete(DeleteLotRequest request)
    {
        var result = handler.DeleteAsync(request).Result;
        //return result.Success ? Ok() : BadRequest(result.Message);
        return Ok(); // Apagar
    }
    
}