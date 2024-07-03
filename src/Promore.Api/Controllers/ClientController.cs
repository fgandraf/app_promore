using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Clients;
using Promore.Core.Responses;
using Promore.Core.Responses.Clients;
using Swashbuckle.AspNetCore.Annotations;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/clients")]
public class ClientController(IClientHandler handler) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Insere um novo cliente")]
    [ProducesResponseType(typeof(Response<Client?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Post(CreateClientRequest request)
    {
        var result = handler.CreateAsync(request).Result;

        return result.IsSuccess
            ? TypedResults.Created($"/id/{result.Data?.Id}", result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }   
   
    [HttpPut]
    [SwaggerOperation(Summary = "Altera os dados de um cliente")]
    [ProducesResponseType(typeof(Response<Client?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Update(UpdateClientRequest request)
    {
        var result = handler.UpdateAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Exclui um cliente pelo identificador")]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult Delete(int id)
    {
        var request = new DeleteClientRequest { Id = id };
        var result = handler.DeleteAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(new NullResponse(result.Message!))
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("id/{id}")]
    [SwaggerOperation(Summary = "Obtém o cliente pelo identificador")]
    [ProducesResponseType(typeof(Response<Client?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetById(int id)
    {
        var request = new GetClientByIdRequest { Id = id };
        var result = handler.GetByIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }
    
    [HttpGet("lot/{lotId}")]
    [SwaggerOperation(Summary = "Obtém todos os clientes do lote")]
    [ProducesResponseType(typeof(Response<List<ClientResponse>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetAllByLotId(int lotId)
    {
        var request = new GetAllClientsByLotIdRequest { LotId = lotId };
        var result = handler.GetAllByLotIdAsync(request).Result;
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todos os clientes")]
    [ProducesResponseType(typeof(PagedResponse<List<Client>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NullResponse), StatusCodes.Status400BadRequest)]
    public IResult GetAll([FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllClientsRequest { PageNumber = pageNumber, PageSize = pageSize };
        var result = handler.GetAllAsync(request).Result;

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(new NullResponse(result.Message!));
    }

}