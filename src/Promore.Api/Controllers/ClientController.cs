using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Requests.Clients;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/clients")]
public class ClientController(IClientHandler handler) : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpGet]
    public IResult GetAll([FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllClientsRequest { PageNumber = pageNumber, PageSize = pageSize };
        var result = handler.GetAllAsync(request).Result;

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpGet("lot/{lotId}")]
    public IResult GetAllByLotId(int lotId)
    {
        var request = new GetAllClientsByLotIdRequest { LotId = lotId };
        var result = handler.GetAllByLotIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    
    [HttpGet("id/{id}")]
    public IResult GetById(int id)
    {
        var request = new GetClientByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpPost]
    public IResult Post(CreateClientRequest request)
    {
        var result = handler.CreateAsync(request).Result;

        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }   
   
    [HttpPut]
    public IResult Update(UpdateClientRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
    [HttpDelete("{id}")]
    public IResult Delete(int id)
    {
        var request = new DeleteClientRequest { Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
}