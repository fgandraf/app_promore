using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Models.InputModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/regions")]
public class RegionController : ControllerBase
{
    private IRegionRepository _repository;

    public RegionController(IRegionRepository repository)
        => _repository = repository;
    
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = _repository.GetAll().Result;
        return regions.IsNullOrEmpty() ? NotFound() : Ok(regions);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var region = _repository.GetByIdAsync(id).Result;
        return region is null ? NotFound($"Região '{id}' não encontrado!") : Ok(region);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateRegionInput model)
    {
        var id = _repository.InsertAsync(model).Result;
        return Ok(id);
    }   
   
    [HttpPut]
    public IActionResult Update([FromBody]UpdateRegionInput model)
    {
        var updated = _repository.UpdateAsync(model).Result;
        
        if (!updated)
            return NotFound("Região não alterada ou não encontrada!");
        
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.DeleteAsync(id).Result;
        
        if (!deleted)
            return NotFound("Região não encontrada!");
        
        return Ok();
    }
}