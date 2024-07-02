using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Requests.Lots;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController(ILotHandler handler) : ControllerBase
{
    [HttpGet("status-by-region/{regionId}")]
    public IResult GetStatusByRegion(int regionId, [FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetLotsStatusByRegionIdRequest{RegionId = regionId, PageNumber = pageNumber, PageSize = pageSize};
        var result = handler.GetAllStatusByRegionIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpGet("id/{id}")]
    public IResult GetById(int id)
    {
        var request = new GetLotByIdRequest{ Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpPost]
    public IResult Post(CreateLotRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }   
   
    [HttpPut]
    public IResult Update(UpdateLotRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        var request = new DeleteLotRequest{ Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
}