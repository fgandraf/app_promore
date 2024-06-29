using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Users;
using Promore.Core.Responses.Users;

namespace Promore.Infra.Repositories.Mock;

public class UserHandlerMock : IUserHandler
{
    private MockContext _context;

    public UserHandlerMock(MockContext context)
        => _context = context;
    
    public Task<User> GetUserByLogin(GetUserByLoginRequest model)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Email == model.Email);
        
        return Task.FromResult(user);
    }
    
    public Task<List<GetUsersResponse>> GetAllAsync()
    {
        var users = _context.Users
            .Select(user => new GetUsersResponse
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
    
    public Task<GetUserByIdResponse> GetByIdAsync(int id)
    {
        var user = _context.Users
            .Select(user => new GetUserByIdResponse
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
    
    public Task<GetUserByEmailResponse> GetByEmailAsync(string address)
    {
        var user = _context.Users
            .Select(user => new GetUserByEmailResponse
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
    
    public Task<User> GetUserByIdAsync(int id)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Id == id);

        if (user is null)
            return null;
        
        user.Lots = _context.Lots.Where(x => x.UserId == id).ToList();
        
        return Task.FromResult(user);
    }
}