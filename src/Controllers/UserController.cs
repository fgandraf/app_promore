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
    
    // public UserController()
    // {
    //     using (var context = new PromoreDataContext(ConnectionString))
    //     {
    //         var user = new User { Role = 1, Active = true, Email = "fgandraf@gmail.com", PasswordHash = "12345senha" };
    //         var user2 = new User { Role = 1, Active = false, Email = "fernanda@email.com", PasswordHash = "12345senha" };
    //         var user3 = new User { Role = 1, Active = true, Email = "promore@seesp.com.br", PasswordHash = "12345senha" };
    //         context.Users.Add(user);
    //         context.Users.Add(user2);
    //         context.Users.Add(user3);
    //         context.SaveChanges();
    //     }
    // }
    
    
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
        var id = context.SaveChanges();
        
        return Ok(id);
    }
    
    
    [HttpPut]
    public IActionResult Update([FromBody]User model)
    {
        using var context = new PromoreDataContext(ConnectionString);
        var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
        
        if (user is null)
            return NotFound();

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
            return NotFound();
        
        context.Remove(user);
        context.SaveChanges();

        return Ok();
    }

    
}