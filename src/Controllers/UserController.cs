using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromoreApi.Data;
using PromoreApi.Models;
using PromoreApi.ViewModels;

namespace PromoreApi.Controllers;

[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private const string ConnectionString = "Server=localhost,1433;Database=Promore;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";
    
    [HttpGet]
    public IActionResult GetAll()
    {
        using var context = new PromoreDataContext(ConnectionString);
        var users = context
            .Users
            .AsNoTracking()
            .ToList();

        return users.IsNullOrEmpty() ? NotFound() : Ok(users);
    }
    
    
    [HttpGet("email/{email}")]
    public IActionResult GetByEmailAddress(string email)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var users = context
            .Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Email == email);
        
        return users == null ? NotFound($"Usuário '{email}' não encontrado!") : Ok(users);
    }
    
    
    [HttpPost("login")]
    public IActionResult Login([FromBody]LoginViewModel model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var users = context
            .Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Email == model.Email);

        //TO DO: Check password
        
        return users == null || users.Active == false? NotFound($"Usuário '{model.Email}' não encontrado ou não está ativo!") : Ok(users);
    }
    
    
    [HttpPost]
    public IActionResult Post([FromBody]User model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        context.Users.Add(model);
        context.SaveChanges();
        
        return Ok();
    }
    
    
    [HttpPut]
    public IActionResult Update([FromBody]User model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
        
        if (user is null)
            return NotFound("Usuário não encontrado!");

        user.Role = model.Role;
        user.Active = model.Active;
        user.Email = model.Email;
        user.PasswordHash = model.PasswordHash;
        
        context.Update(user);
        context.SaveChanges();

        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var user = context.Users.FirstOrDefault(x => x.Id == id);
        if (user is null)
            return NotFound("Usuário não encontrado!");
        
        context.Remove(user);
        context.SaveChanges();

        return Ok();
    }

    
}