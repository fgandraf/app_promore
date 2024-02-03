using Microsoft.EntityFrameworkCore;
using Promore.Infra.Data;
using Promore.Core.Entities;
using Promore.Core.Models.InputModels;
using Promore.Core.Repositories.Contracts;

namespace Promore.Infra.Repositories.Database;

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