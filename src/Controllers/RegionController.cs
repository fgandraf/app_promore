using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
}