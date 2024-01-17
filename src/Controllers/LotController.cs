using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Data;
using PromoreApi.Models;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/lots")]
public class LotController : ControllerBase
{
    private const string ConnectionString = "Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";
    
    [HttpGet]
    public IActionResult GetAll()
    {
        using var context = new PromoreDataContext(ConnectionString);
        var lots = context
            .Lots
            .AsNoTracking()
            .ToList();

        return lots.IsNullOrEmpty() ? NotFound() : Ok(lots);
    }
    
    
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var lot = context
            .Lots
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        
        return lot == null ? NotFound($"Lote '{id}' não encontrado!") : Ok(lot);
    }
    
    
    [HttpPost]
    public IActionResult Post([FromBody]Lot model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        context.Lots.Add(model);
        context.SaveChanges();
        
        return Ok();
    }
    
    
    [HttpPut]
    public IActionResult Update([FromBody]Lot model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var lot = context.Lots.FirstOrDefault(x => x.Id == model.Id);
        
        if (lot is null)
            return NotFound("Lote não encontrado!");

        if (context.Professionals.FirstOrDefault(x => x.Id == model.ProfessionalId) is null)
            return NotFound("Profissional não encontrado!");
        
        if (context.Regions.FirstOrDefault(x => x.Id == model.RegionId) is null)
            return NotFound("Região não encontrado!");
        
        lot.Block = model.Block;
        lot.Number = model.Number;
        lot.SurveyDate = model.SurveyDate;
        lot.LastModifiedDate = model.LastModifiedDate;
        lot.Status = model.Status;
        lot.Comments = model.Comments;
        lot.ProfessionalId = model.ProfessionalId;
        lot.RegionId = model.RegionId;
        
        context.Update(lot);
        context.SaveChanges();

        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var lot = context.Lots.FirstOrDefault(x => x.Id == id);
        if (lot is null)
            return NotFound("Lote não encontrado!");
        
        context.Remove(lot);
        context.SaveChanges();

        return Ok();
    }

    
}