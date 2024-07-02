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
    [HttpGet("status-by-region/{regionId}")]
    public IActionResult GetStatusByRegion(int regionId)
    {
        var request = new GetLotsStatusByRegionIdRequest{PageNumber = 0, PageSize = 25, RegionId = regionId };
        var result = handler.GetAllStatusByRegionIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpGet("id/{id}")]
    public IActionResult GetById(int id)
    {
        var request = new GetLotByIdRequest{ Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post(CreateLotRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }   
   
    [HttpPut]
    public IActionResult Update(UpdateLotRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var request = new DeleteLotRequest{ Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
}