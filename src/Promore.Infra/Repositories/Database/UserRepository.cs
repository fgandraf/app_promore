using Microsoft.EntityFrameworkCore;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses.Users;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories.Database;

public class UserRepository : IUserHandler
{
    private PromoreDataContext _context;

    public UserRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<User> GetUserByLogin(GetUserByLoginRequest model)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        return user;
    }
    
    public async Task<List<GetUsersResponse>> GetAllAsync()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new GetUsersResponse
            (
                user.Id,
                user.Active,
                user.Email,
                user.Name,
                user.Cpf,
                user.Profession, 
                user.Roles.Select(x => x.Id).ToList(),
                user.Regions.Select(x=> x.Id).ToList(),
                user.Lots.Select(x=> x.Id).ToList()
            ))
            .ToListAsync();
        
        return users;
    }
    
    public async Task<long> InsertAsync(User user)
    {
        _context.Users.Add(user);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0 ? user.Id : 0;
    }
    
    public async Task<GetUserByIdResponse> GetByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new GetUserByIdResponse
            (
                user.Id,
                user.Active,
                user.Email,
                user.Name,
                user.Cpf,
                user.Profession, 
                user.Roles.Select(x => x.Id).ToList(),
                user.Regions.Select(x=> x.Id).ToList(),
                user.Lots.Select(x=> x.Id).ToList()
            ))
            .FirstOrDefaultAsync();
        
        return user;
    }
    
    public async Task<GetUserByEmailResponse> GetByEmailAsync(string address)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Where(x => x.Email == address)
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new GetUserByEmailResponse
            (
                user.Id,
                user.Active,
                user.Email,
                user.Name,
                user.Cpf,
                user.Profession, 
                user.Roles.Select(x => x.Id).ToList(),
                user.Regions.Select(x=> x.Id).ToList(),
                user.Lots.Select(x=> x.Id).ToList()
            ))
            .FirstOrDefaultAsync();
        
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
    
    public async Task<User> GetUserByIdAsync(int id)
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