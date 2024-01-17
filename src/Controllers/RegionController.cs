using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Data;
using PromoreApi.Models;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/regions")]
public class RegionController : ControllerBase
{
    private const string ConnectionString = "Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";
    
    [HttpGet]
    public IActionResult GetAll()
    {
        using var context = new PromoreDataContext(ConnectionString);
        var regions = context
            .Regions
            .AsNoTracking()
            .ToList();

        return regions.IsNullOrEmpty() ? NotFound() : Ok(regions);
    }
    
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var region = context
            .Regions
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        
        return region == null ? NotFound($"Região '{id}' não encontrado!") : Ok(region);
    }
    
    
    [HttpPost]
    public IActionResult Post([FromBody]Region model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        context.Regions.Add(model);
        context.SaveChanges();
        
        return Ok();
    }
    
    
    [HttpPut]
    public IActionResult Update([FromBody]Region model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var region = context.Regions.FirstOrDefault(x => x.Id == model.Id);
        
        if (region is null)
            return NotFound("Região não encontrada!");

        region.Name = model.Name;
        region.EstablishedDate = model.EstablishedDate;
        region.StartDate = model.StartDate;
        region.EndDate = model.EndDate;
        
        context.Update(region);
        context.SaveChanges();

        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var region = context.Regions.FirstOrDefault(x => x.Id == id);
        if (region is null)
            return NotFound("Região não encontrada!");
        
        context.Remove(region);
        context.SaveChanges();

        return Ok();
    }

    
}