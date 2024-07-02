using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core;
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
    public IResult GetAll([FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllRegionsRequest{PageNumber = pageNumber, PageSize = pageSize};
        var result = handler.GetAllAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpGet("id/{id}")]
    public IResult GetById(int id)
    {
        var request = new GetRegionByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IResult Post(CreateRegionRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(result);
    }  
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        var request = new DeleteRegionRequest { Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [Authorize(Roles = "admin, manager")]
    [HttpPut]
    public IResult Update(UpdateRegionRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    
    

}