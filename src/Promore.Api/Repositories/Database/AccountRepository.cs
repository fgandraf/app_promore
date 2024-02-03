using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Api.Entities;
using Promore.Api.Models.InputModels;
using Promore.Api.Repositories.Contracts;
using SecureIdentity.Password;

namespace Promore.Api.Repositories.Database;

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