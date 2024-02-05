using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.User.Contracts;
using Promore.Core.Contexts.User.Entity;
using Promore.Infra.Data;
using Responses = Promore.Core.Contexts.User.Models.Responses;
using Requests = Promore.Core.Contexts.User.Models.Requests;

namespace Promore.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private PromoreDataContext _context;

    public UserRepository(PromoreDataContext context)
        => _context = context;
    

    public async Task<List<Responses.ReadUser>> GetAll()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new Responses.ReadUser
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
    
    public async Task<List<User>> GetUsersByIdListAsync(List<int> usersId)
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Where(user => usersId.Contains(user.Id))
            .ToListAsync();

        return users;
    }
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user;
    }
    
    public async Task<Responses.ReadUser> GetByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new Responses.ReadUser
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

    public async Task<Responses.ReadUser> GetByEmailAddress(string address)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new Responses.ReadUser
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

    public async Task<long> InsertAsync(User user)
    {
        _context.Users.Add(user);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0 ? user.Id : 0;
    }

    public async Task<int> UpdateInfoAsync(User user)
    {
        _context.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateSettingsAsync(User user)
    {
        _context.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return 0;

        _context.Remove(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByLogin(Requests.Login model)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        return user;
    }
    
    
}