using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Lots;
using Promore.Core.Responses;
using Promore.Core.Responses.Lots;
using Swashbuckle.AspNetCore.Annotations;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController(ILotHandler handler) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Insere um novo lote")]
    [ProducesResponseType(typeof(Response<Lot?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Post(CreateLotRequest request)
    {
        var result = handler.CreateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }   
   
    [HttpPut]
    [SwaggerOperation(Summary = "Altera os dados de um lote")]
    [ProducesResponseType(typeof(Response<Lot?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Update(UpdateLotRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Exclui um lote pelo identificador")]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Delete(int id)
    {
        var request = new DeleteLotRequest{ Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(new NullResponse(result.Message!))
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("id/{id}")]
    [SwaggerOperation(Summary = "Obtém o lote pelo identificador")]
    [ProducesResponseType(typeof(Response<Lot?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetById(int id)
    {
        var request = new GetLotByIdRequest{ Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("status-by-region/{regionId}")]
    [SwaggerOperation(Summary = "Obtém o status de todos os lotes de uma região")]
    [ProducesResponseType(typeof(PagedResponse<List<LotsStatusResponse>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetStatusByRegion(int regionId, [FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetLotsStatusByRegionIdRequest{RegionId = regionId, PageNumber = pageNumber, PageSize = pageSize};
        var result = handler.GetAllStatusByRegionIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
}