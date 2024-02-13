using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.UserContext.Contracts;
using Promore.Core.Contexts.UserContext.Entities;
using Promore.Infra.Data;
using UseCases = Promore.Core.Contexts.UserContext.UseCases;

namespace Promore.Infra.Repositories.Database;

public class UserRepository : IUserRepository
{
    private PromoreDataContext _context;

    public UserRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<User> GetUserByLogin(UseCases.GetByLogin.LoginRequest model)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        return user;
    }
    
    public async Task<List<UseCases.GetAll.Response>> GetAllAsync()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new UseCases.GetAll.Response
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
    
    public async Task<UseCases.GetById.Response> GetByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new UseCases.GetById.Response
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
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user;
    }
    
    public async Task<UseCases.GetByEmail.Response> GetByEmailAsync(string address)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .Select(user => new UseCases.GetByEmail.Response
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