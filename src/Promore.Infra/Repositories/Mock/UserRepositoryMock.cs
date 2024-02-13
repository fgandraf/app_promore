using Promore.Core.Contexts.UserContext.Contracts;
using Promore.Core.Contexts.UserContext.Entities;
using UseCases = Promore.Core.Contexts.UserContext.UseCases;

namespace Promore.Infra.Repositories.Mock;

public class UserRepositoryMock : IUserRepository
{
    private MockContext _context;

    public UserRepositoryMock(MockContext context)
        => _context = context;
    
    public Task<User> GetUserByLogin(UseCases.GetByLogin.LoginRequest model)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Email == model.Email);
        
        return Task.FromResult(user);
    }
    
    public Task<List<UseCases.GetAll.Response>> GetAllAsync()
    {
        var users = _context.Users
            .Select(user => new UseCases.GetAll.Response
            (
                Id:user.Id,
                Active:user.Active,
                Email:user.Email,
                Name:user.Name,
                Cpf:user.Cpf,
                Profession:user.Profession, 
                Roles:user.Roles.Select(x => x.Id).ToList(),
                Regions: user.Regions.Select(x=> x.Id).ToList(),
                Lots: user.Lots?.Select(x => x.Id).ToList() ?? new List<string>()
            ))
            .ToList();
        
        return Task.FromResult(users);
    }
    
    public Task<long> InsertAsync(User user)
    {
        user.Id = _context.Users.Max(x => x.Id) + 1;
        _context.Users.Add(user);
        return Task.FromResult(Convert.ToInt64(user.Id));
    }
    
    public Task<UseCases.GetById.Response> GetByIdAsync(int id)
    {
        var user = _context.Users
            .Select(user => new UseCases.GetById.Response
            (
                Id:user.Id,
                Active:user.Active,
                Email:user.Email,
                Name:user.Name,
                Cpf:user.Cpf,
                Profession:user.Profession, 
                Roles:user.Roles.Select(x => x.Id).ToList(),
                Regions: user.Regions.Select(x=> x.Id).ToList(),
                Lots: user.Lots?.Select(x => x.Id).ToList() ?? new List<string>()
            ))
            .FirstOrDefault(x => x.Id == id);
        
        return Task.FromResult(user);
    }
    
    public Task<UseCases.GetByEmail.Response> GetByEmailAsync(string address)
    {
        var user = _context.Users
            .Select(user => new UseCases.GetByEmail.Response
            (
                Id:user.Id,
                Active:user.Active,
                Email:user.Email,
                Name:user.Name,
                Cpf:user.Cpf,
                Profession:user.Profession, 
                Roles:user.Roles.Select(x => x.Id).ToList(),
                Regions: user.Regions.Select(x=> x.Id).ToList(),
                Lots: user.Lots?.Select(x => x.Id).ToList() ?? new List<string>()
            ))
            .FirstOrDefault(x => x.Email == address);
        
        return Task.FromResult(user);
    }
    
    public Task<int> UpdateAsync(User user)
    {
        var userSaved = _context.Users.FirstOrDefault(x => x.Id == user.Id);
        _context.Users.Remove(userSaved);
        _context.Users.Add(user);
        return Task.FromResult(1);
    }

    public Task<List<User>> GetEntitiesByIdsAsync(List<int> ids)
    {
        var users = _context.Users
            .Where(user => ids.Contains(user.Id))
            .ToList();
        
        foreach (var user in users)
            user.Lots = _context.Lots.Where(x => x.UserId == user.Id).ToList();

        return Task.FromResult(users);
    }
    
    public Task<User> GetEntityByIdAsync(int id)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Id == id);

        if (user is null)
            return null;
        
        user.Lots = _context.Lots.Where(x => x.UserId == id).ToList();
        
        return Task.FromResult(user);
    }
}