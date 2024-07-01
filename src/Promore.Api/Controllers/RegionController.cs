using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core.Handlers;
using Promore.Core.Requests.Regions;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/regions")]
public class RegionController(IRegionHandler handler) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll(GetAllRegionsRequest request)
    {
        var result = handler.GetAllAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post(CreateRegionRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }  
    
    [Authorize(Roles = "admin")]
    [HttpDelete]
    public IActionResult Delete(DeleteRegionRequest request)
    {
        var result = handler.DeleteAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "admin, manager")]
    [HttpPut]
    public IActionResult Update(UpdateRegionRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    
    [HttpGet("id")]
    public IActionResult GetById(GetRegionByIdRequest request)
    {
        var result = handler.GetByIdAsync(request).Result;
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }

}