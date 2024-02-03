using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Promore.Api.Models.InputModels;
using Promore.Api.Repositories.Contracts;

namespace Promore.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/lots")]
public class LotController : ControllerBase
{
    private ILotRepository _repository;

    public LotController(ILotRepository repository)
        => _repository = repository;
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var lots = _repository.GetAll().Result;
        return lots.IsNullOrEmpty() ? NotFound() : Ok(lots);
    }
    
    [HttpGet("status/{regionId:int}")]
    public IActionResult GetStatusByRegion(int regionId)
    {
        var lots = _repository.GetStatusByRegion(regionId).Result;
        return lots.IsNullOrEmpty() ? NotFound() : Ok(lots);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var lot = _repository.GetByIdAsync(id).Result;
        return lot is null ? NotFound($"Lote '{id}' não encontrado!") : Ok(lot);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateLotInput model)
    {
        var id = _repository.InsertAsync(model).Result;
        return Ok(id);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateLotInput model)
    {
        var updated = _repository.UpdateAsync(model).Result;
        
        if (!updated)
            return NotFound("Lote não alterado ou não encontrado!");
        
        return Ok();
    }
    
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var deleted = _repository.DeleteAsync(id).Result;
        
        if (!deleted)
            return NotFound("Lote não encontrado!");
        
        return Ok();
    }
    
}