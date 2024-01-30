using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/professionals")]
public class ProfessionalController : ControllerBase
{
    private IProfessionalRepository _repository;

    public ProfessionalController(IProfessionalRepository repository)
        => _repository = repository;
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var professionals = _repository.GetAll().Result;
        return professionals.IsNullOrEmpty() ? NotFound() : Ok(professionals);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var professional = _repository.GetByIdAsync(id).Result;
        return professional is null ? NotFound($"Profissional '{id}' não encontrado!") : Ok(professional);
    }
}