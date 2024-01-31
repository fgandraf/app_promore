using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Entities;
using PromoreApi.Models.InputModels;
using PromoreApi.Models.ViewModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

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
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Email == address);
        
        return user;
    }

    public async Task<long> InsertAsync(CreateUserInput model)
    {
        var roles = model.Roles.Select(role => _context.Roles.FirstOrDefault(x => x.Id == role)).ToList();
        var regions = model.Regions.Select(region => _context.Regions.FirstOrDefault(x => x.Id == region)).ToList();
        
        var user = new User
        {
            Active = model.Active,
            Email = model.Email,
            PasswordHash = model.Password,
            Roles = roles,
            Regions = regions
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> UpdateAsync(UpdateUserInput model)
    {
        var user = await _context
            .Users
            .Include(x => x.Regions)
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        
        if (user is null)
            return false;
        
        var roles = model.Roles.Select(role => _context.Roles.FirstOrDefault(x => x.Id == role)).ToList();
        var regions = model.Regions.Select(region => _context.Regions.FirstOrDefault(x => x.Id == region)).ToList();
        
        user.Active = model.Active;
        user.Email = model.Email;
        user.PasswordHash = model.Password;
        user.Roles = roles;
        user.Regions = regions;
        
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