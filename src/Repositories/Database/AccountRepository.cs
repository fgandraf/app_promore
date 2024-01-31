using Microsoft.EntityFrameworkCore;
using PromoreApi.Data;
using PromoreApi.Models.InputModels;
using PromoreApi.Repositories.Contracts;

namespace PromoreApi.Repositories.Database;

public class AccountRepository : IAccountRepository
{
    private PromoreDataContext _context;

    public AccountRepository(PromoreDataContext context)
        => _context = context;
    

    public async Task<bool> LoginAsync(LoginInput model)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user is null || !user.Active)
            return false;
       
        //TO DO: Check password
        return true;
    }
}