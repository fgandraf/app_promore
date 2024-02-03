using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core.Entities;
using Promore.Core.Models.InputModels;
using Promore.Core.Models.ViewModels;
using Promore.Core.Repositories.Contracts;
using SecureIdentity.Password;

namespace Promore.Api.Repositories.Database;

public class UserRepository : IUserRepository
{
    private PromoreDataContext _context;

    public UserRepository(PromoreDataContext context)
        => _context = context;
    

    public async Task<List<UserView>> GetAll()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new UserView
            {
                Id = user.Id,
                Active = user.Active,
                Email = user.Email,
                Name = user.Name,
                Cpf = user.Cpf,
                Profession = user.Profession, 
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList()
            })
            .ToListAsync();
        
        return users;
    }
    
    public async Task<UserView> GetByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new UserView
            {
                Id = user.Id,
                Active = user.Active,
                Email = user.Email,
                Name = user.Name,
                Cpf = user.Cpf,
                Profession = user.Profession, 
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user;
    }

    public async Task<UserView> GetByEmailAddress(string address)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new UserView
            {
                Id = user.Id,
                Active = user.Active,
                Email = user.Email,
                Name = user.Name,
                Cpf = user.Cpf,
                Profession = user.Profession, 
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Email == address);
        
        return user;
    }

    public async Task<long> InsertAsync(CreateUserInput model)
    {
        var user = new User
        {
            Active = model.Active,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            Name = model.Name,
            Cpf = model.Cpf,
            Profession = model.Profession, 
            Roles = model.Roles.Select(role => _context.Roles.FirstOrDefault(x => x.Id == role)).ToList(),
            Regions = model.Regions.Select(region => _context.Regions.FirstOrDefault(x => x.Id == region)).ToList()
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> UpdateInfoAsync(UpdateUserInfoInput model)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (user is null)
            return false;
        
        user.Email = model.Email;
        user.PasswordHash = PasswordHasher.Hash(model.Password);
        user.Name = model.Name;
        user.Cpf = model.Cpf;
        user.Profession = model.Profession;
        
        _context.Update(user);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> UpdateSettingsAsync(UpdateUserSettingsInput model)
    {
        var user = await _context
            .Users
            .Include(x => x.Regions)
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (user is null)
            return false;
        
        user.Active = model.Active;
        user.Roles = model.Roles.Select(role => _context.Roles.FirstOrDefault(x => x.Id == role)).ToList();
        user.Regions = model.Regions.Select(region => _context.Regions.FirstOrDefault(x => x.Id == region)).ToList();
        
        _context.Update(user);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return false;

        _context.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}