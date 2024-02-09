using Microsoft.EntityFrameworkCore;
using Promore.Core.Contexts.RoleContext.Contracts;
using Promore.Core.Contexts.RoleContext.Entities;
using Promore.Infra.Data;

namespace Promore.Infra.Repositories;

public class RoleRepository : IRoleRepository
{
    private PromoreDataContext _context;

    public RoleRepository(PromoreDataContext context)
        => _context = context;
    
    public async Task<List<Role>> GetRolesByIdListAsync(List<int> rolesId)
    {
        var roles = await _context
            .Roles
            .AsNoTracking()
            .Where(role => rolesId.Contains(role.Id))
            .ToListAsync();

        return roles;
    }
}