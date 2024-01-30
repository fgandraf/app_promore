using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Repositories.Contracts;
using PromoreApi.ViewModels;

namespace PromoreApi.Repositories.Database;

public class UserRepository : IUserRepository
{
    private PromoreDataContext _context;

    public UserRepository(PromoreDataContext context)
        => _context = context;
    

    public async Task<List<UserGetVO>> GetAll()
    {
        var users = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new UserGetVO
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
    
    public async Task<UserGetVO> GetByIdAsync(int id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new UserGetVO
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

    public async Task<UserGetVO> GetByEmailAddress(string address)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Select(user => new UserGetVO
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
}