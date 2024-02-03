using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Entities;
using PromoreApi.Models.InputModels;
using PromoreApi.Repositories.Contracts;
using SecureIdentity.Password;

namespace PromoreApi.Repositories.Database;

public class AccountRepository : IAccountRepository
{
    private PromoreDataContext _context;

    public AccountRepository(PromoreDataContext context)
        => _context = context;
    

    public async Task<User> LoginAsync(LoginInput model)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        return user;
    }
}