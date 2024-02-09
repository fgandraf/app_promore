
using Microsoft.EntityFrameworkCore;
using Promore.Core.Contracts;
using Promore.Core.Entities;
using Promore.Infra.Data;
using LoginRequest = Promore.Core.ViewModels.Requests.LoginRequest;
using UserResponse = Promore.Core.ViewModels.Responses.UserResponse;

namespace Promore.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private PromoreDataContext _context;

    public UserRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<User> GetUserByLogin(LoginRequest model)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        return user;
    }
    
    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Active = user.Active,
                Email = user.Email,
                Name = user.Name,
                Cpf = user.Cpf,
                Profession = user.Profession, 
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList(),
                Lots = user.Lots.Select(x=> x.Id).ToList()
            })
            .ToListAsync();
        
        return users;
    }
    
    public async Task<long> InsertAsync(User user)
    {
        _context.Users.Add(user);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0 ? user.Id : 0;
    }
    
    public async Task<UserResponse> GetByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Active = user.Active,
                Email = user.Email,
                Name = user.Name,
                Cpf = user.Cpf,
                Profession = user.Profession, 
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList(),
                Lots = user.Lots.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user;
    }
    
    public async Task<UserResponse> GetByEmailAsync(string address)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Active = user.Active,
                Email = user.Email,
                Name = user.Name,
                Cpf = user.Cpf,
                Profession = user.Profession, 
                Roles = user.Roles.Select(x => x.Id).ToList(),
                Regions = user.Regions.Select(x=> x.Id).ToList(),
                Lots = user.Lots.Select(x=> x.Id).ToList()
            })
            .FirstOrDefaultAsync(x => x.Email == address);
        
        return user;
    }
    
    public async Task<int> UpdateAsync(User user)
    {
        _context.Update(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetEntitiesByIdsAsync(List<int> ids)
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .Include(x => x.Regions)
            .Include(x => x.Lots)
            .Where(user => ids.Contains(user.Id))
            .ToListAsync();

        return users;
    }
    
    public async Task<User> GetEntityByIdAsync(int id)
    {
        var user = await _context
            .Users
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user;
    }
    
}