using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Api.Services;
using Promore.Core.Requests.Regions;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/regions")]
public class RegionController(RegionService service) : ControllerBase
{
    public readonly RegionService Service = service;

    [Authorize(Roles = "admin")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = Service.GetAllAsync().Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Post([FromBody]CreateRegionRequest model)
    {
        var result = Service.CreateAsync(model).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }  
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = Service.DeleteAsync(id).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    [Authorize(Roles = "admin, manager")]
    [HttpPut]
    public IActionResult Update([FromBody]UpdateRegionRequest model)
    {
        var result = Service.UpdateAsync(model).Result;
        return result.Success ? Ok() : BadRequest(result.Message);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = Service.GetByIdAsync(id).Result;
        return result.Success ? Ok(result.Value) : BadRequest(result.Message);
    }

}