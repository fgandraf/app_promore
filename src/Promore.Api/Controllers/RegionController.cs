using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;
using Promore.Core.Responses.Regions;
using Swashbuckle.AspNetCore.Annotations;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/regions")]
public class RegionController(IRegionHandler handler) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPost]
    [SwaggerOperation(Summary = "Insere uma nova região")]
    [ProducesResponseType(typeof(Response<Region?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Post(CreateRegionRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result.Data)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }  
    
    [Authorize(Roles = "admin, manager")]
    [HttpPut]
    [SwaggerOperation(Summary = "Altera os dados de uma região")]
    [ProducesResponseType(typeof(Response<Region?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Update(UpdateRegionRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Exclui uma região pelo identificador")]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Delete(int id)
    {
        var request = new DeleteRegionRequest { Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(new NullResponse(result.Message!))
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("id/{id}")]
    [SwaggerOperation(Summary = "Obtém a região pelo identificador")]
    [ProducesResponseType(typeof(Response<Region?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetById(int id)
    {
        var request = new GetRegionByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todas as regiões")]
    [ProducesResponseType(typeof(PagedResponse<List<GetRegionsResponse>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetAll([FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllRegionsRequest{PageNumber = pageNumber, PageSize = pageSize};
        var result = handler.GetAllAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }

}