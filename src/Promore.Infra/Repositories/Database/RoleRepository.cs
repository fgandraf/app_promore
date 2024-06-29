using Microsoft.EntityFrameworkCore;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories.Database;

public class RoleRepository : IRoleHandler
{
    private PromoreDataContext _context;

    public RoleRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<Role>> GetRolesByIdListAsync(List<int> rolesId)
    {
        var roles = await _context
            .Roles
            .Where(role => rolesId.Contains(role.Id))
            .ToListAsync();

        return roles;
    }
}