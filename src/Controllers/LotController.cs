using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Controllers;

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
    
    
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var lot = _repository.GetByIdAsync(id).Result;
        return lot is null ? NotFound($"Lote '{id}' não encontrado!") : Ok(lot);
    }
    
}