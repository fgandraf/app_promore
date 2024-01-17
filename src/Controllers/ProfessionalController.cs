using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Data;
using PromoreApi.Models;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/professionals")]
public class ProfessionalController : ControllerBase
{
    private const string ConnectionString = "Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";
    
    [HttpGet]
    public IActionResult GetAll()
    {
        using var context = new PromoreDataContext(ConnectionString);
        var professionals = context
            .Professionals
            .AsNoTracking()
            .ToList();

        return professionals.IsNullOrEmpty() ? NotFound() : Ok(professionals);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var professional = context
            .Professionals
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        
        return professional == null ? NotFound($"Profissional '{id}' não encontrado!") : Ok(professional);
    }
    
    
    [HttpPost]
    public IActionResult Post([FromBody]Professional model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        context.Professionals.Add(model);
        context.SaveChanges();
        
        return Ok();
    }
    
    
    [HttpPut]
    public IActionResult Update([FromBody]Professional model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var professional = context.Professionals.FirstOrDefault(x => x.Id == model.Id);
        
        if (professional is null)
            return NotFound("Profissional não encontrado!");

        professional.Name = model.Name;
        professional.Cpf = model.Cpf;
        professional.Profession = model.Profession;
        
        context.Update(professional);
        context.SaveChanges();

        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var professional = context.Professionals.FirstOrDefault(x => x.Id == id);
        if (professional is null)
            return NotFound("Profissional não encontrado!");
        
        context.Remove(professional);
        context.SaveChanges();

        return Ok();
    }

    
}