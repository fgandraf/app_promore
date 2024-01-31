using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Models.InputModels;
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
    
    [HttpPost]
    public IActionResult Post([FromBody]CreateProfessionalInput model)
    {
        var id = _repository.InsertAsync(model).Result;
        return Ok(id);
    }


    [HttpPut]
    public IActionResult Update([FromBody]UpdateProfessionalInput model)
    {
        var updated = _repository.UpdateAsync(model).Result;
        
        if (!updated)
            return NotFound("Profissional não alterado ou não encontrado!");
        
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.DeleteAsync(id).Result;
        
        if (!deleted)
            return NotFound("Profissional não encontrado!");
        
        return Ok($"Profisisonal '{id}' removido!");
    }
}